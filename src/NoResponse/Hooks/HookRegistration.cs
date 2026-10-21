namespace NoResponse.Hooks
{
    /// <summary>
    /// Hand-written. Hooks are registered per SDK instance and last as long as it does.
    /// </summary>
    public static class HookRegistration
    {
        public static void InitHooks(IHooks hooks)
        {
            hooks.RegisterBeforeRequestHook(new IdempotencyKeyHook());
        }
    }
}
