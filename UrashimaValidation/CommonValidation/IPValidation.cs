using UrashimaValidation.Functions;

namespace UrashimaValidation.CommonValidation
{
    public class IPv4Validation<T> : Validation<T>
    {
        private IValidation<T>? _baseValidation;

        public IPv4Validation(Func<T, object> originalValue, IValidation<T>? baseValidation = null)
            : base("IPv4 Address validation failed.",
                  "IPv4 validation",
                  originalValue,
                  obj => originalValue(obj).ToString()!.IsIPv4Address())
        {
            _baseValidation = baseValidation;
        }

        public IPv4Validation(string messageOnError, string name, Func<T, object> originalValue, IValidation<T>? baseValidation = null)
            : base(messageOnError, name, originalValue, obj => originalValue(obj).ToString()!.IsIPv4Address())
        {
            _baseValidation = baseValidation;
        }

        public override bool IsValid(T value)
        {
            if (!IsBaseSuccess(_baseValidation, value))
                return false;

            return ValidationFunction(value);
        }
    }

    public class IPv6Validation<T> : Validation<T>
    {
        private IValidation<T>? _baseValidation;

        public IPv6Validation(Func<T, object> originalValue, IValidation<T>? baseValidation = null)
            : base("IPv4 Address validation failed.",
                  "IPv4 validation",
                  originalValue,
                  obj => originalValue(obj).ToString()!.IsIPv6Address())
        {
            _baseValidation = baseValidation;
        }

        public IPv6Validation(string messageOnError, string name, Func<T, object> originalValue, IValidation<T>? baseValidation = null)
            : base(messageOnError, name, originalValue, obj => originalValue(obj).ToString()!.IsIPv6Address())
        {
            _baseValidation = baseValidation;
        }

        public override bool IsValid(T value)
        {
            if (!IsBaseSuccess(_baseValidation, value))
                return false;

            return ValidationFunction(value);
        }
    }
}