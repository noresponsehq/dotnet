#nullable enable
namespace NoResponse.Models.Shared
{
    using Newtonsoft.Json;
    using NoResponse.Utils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// What sort of value a field holds, which decides how it is cleaned, compared and searched again. It comes from the value itself where its shape is certain (an email, an IP, a URL, an international phone number), else from the answer stored for the source's column, else text. A value of any kind is returned.
    /// </summary>
    [JsonConverter(typeof(OpenEnumConverter))]
    public class RecordKind : IEquatable<RecordKind>, IOpenEnum<string>
    {
        public static readonly RecordKind Email = new RecordKind("email");
        public static readonly RecordKind Phone = new RecordKind("phone");
        public static readonly RecordKind Username = new RecordKind("username");
        public static readonly RecordKind AccountId = new RecordKind("account_id");
        public static readonly RecordKind Url = new RecordKind("url");
        public static readonly RecordKind Domain = new RecordKind("domain");
        public static readonly RecordKind Image = new RecordKind("image");
        public static readonly RecordKind PersonName = new RecordKind("person_name");
        public static readonly RecordKind Gender = new RecordKind("gender");
        public static readonly RecordKind Age = new RecordKind("age");
        public static readonly RecordKind Language = new RecordKind("language");
        public static readonly RecordKind Organisation = new RecordKind("organisation");
        public static readonly RecordKind JobTitle = new RecordKind("job_title");
        public static readonly RecordKind Country = new RecordKind("country");
        public static readonly RecordKind Region = new RecordKind("region");
        public static readonly RecordKind City = new RecordKind("city");
        public static readonly RecordKind Address = new RecordKind("address");
        public static readonly RecordKind Postcode = new RecordKind("postcode");
        public static readonly RecordKind Coordinates = new RecordKind("coordinates");
        public static readonly RecordKind Timezone = new RecordKind("timezone");
        public static readonly RecordKind Date = new RecordKind("date");
        public static readonly RecordKind DateTime = new RecordKind("date_time");
        public static readonly RecordKind Duration = new RecordKind("duration");
        public static readonly RecordKind Ip = new RecordKind("ip");
        public static readonly RecordKind MacAddress = new RecordKind("mac_address");
        public static readonly RecordKind DeviceId = new RecordKind("device_id");
        public static readonly RecordKind UserAgent = new RecordKind("user_agent");
        public static readonly RecordKind Password = new RecordKind("password");
        public static readonly RecordKind Hash = new RecordKind("hash");
        public static readonly RecordKind Salt = new RecordKind("salt");
        public static readonly RecordKind Token = new RecordKind("token");
        public static readonly RecordKind SecurityAnswer = new RecordKind("security_answer");
        public static readonly RecordKind CardNumber = new RecordKind("card_number");
        public static readonly RecordKind BankAccount = new RecordKind("bank_account");
        public static readonly RecordKind CryptoAddress = new RecordKind("crypto_address");
        public static readonly RecordKind Amount = new RecordKind("amount");
        public static readonly RecordKind IdNumber = new RecordKind("id_number");
        public static readonly RecordKind Vehicle = new RecordKind("vehicle");
        public static readonly RecordKind Label = new RecordKind("label");
        public static readonly RecordKind LongText = new RecordKind("long_text");
        public static readonly RecordKind Count = new RecordKind("count");
        public static readonly RecordKind Score = new RecordKind("score");
        public static readonly RecordKind Flag = new RecordKind("flag");
        public static readonly RecordKind Structured = new RecordKind("structured");
        public static readonly RecordKind Text = new RecordKind("text");

        private static readonly Dictionary<string, RecordKind> _knownValues =
            new Dictionary<string, RecordKind>() {
                ["email"] = Email,
                ["phone"] = Phone,
                ["username"] = Username,
                ["account_id"] = AccountId,
                ["url"] = Url,
                ["domain"] = Domain,
                ["image"] = Image,
                ["person_name"] = PersonName,
                ["gender"] = Gender,
                ["age"] = Age,
                ["language"] = Language,
                ["organisation"] = Organisation,
                ["job_title"] = JobTitle,
                ["country"] = Country,
                ["region"] = Region,
                ["city"] = City,
                ["address"] = Address,
                ["postcode"] = Postcode,
                ["coordinates"] = Coordinates,
                ["timezone"] = Timezone,
                ["date"] = Date,
                ["date_time"] = DateTime,
                ["duration"] = Duration,
                ["ip"] = Ip,
                ["mac_address"] = MacAddress,
                ["device_id"] = DeviceId,
                ["user_agent"] = UserAgent,
                ["password"] = Password,
                ["hash"] = Hash,
                ["salt"] = Salt,
                ["token"] = Token,
                ["security_answer"] = SecurityAnswer,
                ["card_number"] = CardNumber,
                ["bank_account"] = BankAccount,
                ["crypto_address"] = CryptoAddress,
                ["amount"] = Amount,
                ["id_number"] = IdNumber,
                ["vehicle"] = Vehicle,
                ["label"] = Label,
                ["long_text"] = LongText,
                ["count"] = Count,
                ["score"] = Score,
                ["flag"] = Flag,
                ["structured"] = Structured,
                ["text"] = Text
            };

        private static readonly ConcurrentDictionary<string, RecordKind> _values =
            new ConcurrentDictionary<string, RecordKind>(_knownValues);

        private RecordKind(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public string Value { get; }

        public static RecordKind Of(string value)
        {
            return _values.GetOrAdd(value,
                                    _ => new RecordKind(value));
        }

        public static implicit operator RecordKind(string value) => Of(value);
        public static implicit operator string(RecordKind recordkind) => recordkind.Value;

        public static RecordKind[] Values()
        {
            return _values.Values.ToArray();
        }

        public override string ToString() => Value.ToString(System.Globalization.CultureInfo.InvariantCulture);

        public bool IsKnown()
        {
            return _knownValues.ContainsKey(Value);
        }

        public override bool Equals(object? obj) => Equals(obj as RecordKind);

        public bool Equals(RecordKind? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}