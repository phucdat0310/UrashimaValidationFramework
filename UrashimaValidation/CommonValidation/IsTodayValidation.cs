using UrashimaValidation.Functions;

namespace UrashimaValidation.CommonValidation
{
    public class IsTodayValidation<T> : Validation<T>
    {
        private IValidation<T>? _baseValidation;

        public IsTodayValidation(Func<T, object> originalValue, IValidation<T>? baseValidation = null)
            : base("DateTime value is not Today.",
                  "Is Today validation",
                  originalValue,
                  obj => ((DateTime)originalValue(obj)).IsToday())
        {
            _baseValidation = baseValidation;
        }

        public IsTodayValidation(string messageOnError, string name, Func<T, object> originalValue, IValidation<T>? baseValidation = null)
            : base(messageOnError, name, originalValue, obj => ((DateTime)originalValue(obj)).IsToday())
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