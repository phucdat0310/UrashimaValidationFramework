using UrashimaValidation.Functions;

namespace UrashimaValidation.CommonValidation
{
    public class RegexValidation<T> : Validation<T>
    {
        private IValidation<T>? _baseValidation;

        public RegexValidation(Func<T, object> originalValue, string pattern, IValidation<T>? baseValidation = null)
            : base("Regex validation failed.",
                  "Regex validation",
                  originalValue,
                  obj => originalValue(obj).ToString()!.IsMatch(pattern))
        {
            _baseValidation = baseValidation;
        }

        public RegexValidation(string messageOnError, string name, Func<T, object> originalValue, string pattern, IValidation<T>? baseValidation = null)
            : base(messageOnError, name, originalValue, obj => originalValue(obj).ToString()!.IsMatch(pattern))
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