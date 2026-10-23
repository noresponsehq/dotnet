#nullable enable
namespace NoResponse
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using NoResponse.Models.Shared;
    using NoResponse.Models.Operations;

    /// <summary>
    /// The assessment was still pending when the wait ran out. Last is the pending assessment
    /// from the final poll; the webhook delivers the result when it settles.
    /// </summary>
    public sealed class WaitTimeoutException : TimeoutException
    {
        public WaitTimeoutException(string assessmentId, Assessment last) : base($"Assessment {assessmentId} is still pending")
        {
            AssessmentId = assessmentId;
            Last = last;
        }

        public string AssessmentId { get; }

        public Assessment Last { get; }
    }

    /// <summary>Wait for an assessment to complete. Hand-written beside the generated client.</summary>
    public static class Wait
    {
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Poll GetAsync until the assessment's status is complete, sleeping for the
        /// Retry-After the API sends (at least 1 s) between polls. When the timeout
        /// (default 30 s) passes, a partial answer is returned as it stands, its
        /// status still <c>Status.Partial</c>.
        /// </summary>
        /// <exception cref="WaitTimeoutException">The timeout passed while the assessment was still pending.</exception>
        public static async Task<Assessment> WaitAsync(
            this IAssessments assessments,
            string assessmentId,
            TimeSpan? timeout = null,
            CancellationToken cancellationToken = default,
            Func<TimeSpan, CancellationToken, Task>? delay = null,
            Func<TimeSpan>? clock = null)
        {
            delay ??= Task.Delay;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            clock ??= () => watch.Elapsed;
            var deadline = clock() + (timeout ?? DefaultTimeout);
            Assessment? last = null;
            while (true)
            {
                var remaining = deadline - clock();
                if (remaining <= TimeSpan.Zero)
                {
                    if (last is not null)
                    {
                        return TimedOut(assessmentId, last);
                    }
                    throw new TimeoutException($"Timed out waiting for assessment {assessmentId}");
                }

                using var requestCancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                requestCancellation.CancelAfter(remaining);
                GetAssessmentResponse response;
                try
                {
                    response = await assessments.GetAsync(assessmentId, cancellationToken: requestCancellation.Token).ConfigureAwait(false);
                }
                catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested
                    && requestCancellation.IsCancellationRequested && last is not null)
                {
                    return TimedOut(assessmentId, last);
                }
                if (Status.Complete.Equals(response.Result.Status))
                {
                    return response.Result;
                }
                last = response.Result;
                var wait = RetryAfter(response.Headers);
                if (clock() + wait > deadline)
                {
                    return TimedOut(assessmentId, last);
                }
                await delay(wait, cancellationToken).ConfigureAwait(false);
            }
        }

        // The wait ran out: a partial answer stands, and a pending one throws.
        private static Assessment TimedOut(string assessmentId, Assessment last)
        {
            if (Status.Partial.Equals(last.Status))
            {
                return last;
            }
            throw new WaitTimeoutException(assessmentId, last);
        }

        private static TimeSpan RetryAfter(Dictionary<string, List<string>> headers)
        {
            foreach (var header in headers)
            {
                if (string.Equals(header.Key, "Retry-After", StringComparison.OrdinalIgnoreCase)
                    && header.Value.Count > 0
                    && double.TryParse(header.Value[0], NumberStyles.Float, CultureInfo.InvariantCulture, out var seconds))
                {
                    return TimeSpan.FromSeconds(Math.Max(1.0, seconds));
                }
                if (string.Equals(header.Key, "Retry-After", StringComparison.OrdinalIgnoreCase)
                    && header.Value.Count > 0
                    && DateTimeOffset.TryParse(header.Value[0], CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var retryAt))
                {
                    return retryAt - DateTimeOffset.UtcNow < TimeSpan.FromSeconds(1)
                        ? TimeSpan.FromSeconds(1)
                        : retryAt - DateTimeOffset.UtcNow;
                }
            }
            return TimeSpan.FromSeconds(1);
        }
    }
}
