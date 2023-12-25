using UrashimaValidation.Functions;

namespace UrashimaValidation.CommonValidation
{
    public class RangeValidation<T> : Validation<T>
    {
        private IValidation<T>? _baseValidation;

        public RangeValidation(Func<T, object> originalValue, double min, double max, IValidation<T>? baseValidation = null)
            : base("Value out of range",
                  "Range validation",
                  originalValue,
                  obj => ((double)originalValue(obj)).Between(start: min, end: max))
        {
            _baseValidation = baseValidation;
        }

        public RangeValidation(string messageOnError, string name, Func<T, object> originalValue, double min, double max, IValidation<T>? baseValidation = null)
            : base(messageOnError, name, originalValue, obj => ((double)originalValue(obj)).Between(start: min, end: max))
        {
            _baseValidation = baseValidation;
        }

        // ========================================

        public RangeValidation(Func<T, object> originalValue, float min, float max, IValidation<T>? baseValidation = null)
            : base("Value out of range",
                  "Range validation",
                  originalValue,
                  obj => ((float)originalValue(obj)).Between(start: min, end: max))
        {
            _baseValidation = baseValidation;
        }

        public RangeValidation(string messageOnError, string name, Func<T, object> originalValue, float min, float max, IValidation<T>? baseValidation = null)
            : base(messageOnError, name, originalValue, obj => ((float)originalValue(obj)).Between(start: min, end: max))
        {
            _baseValidation = baseValidation;
        }

        // ========================================

        public RangeValidation(Func<T, object> originalValue, int min, int max, IValidation<T>? baseValidation = null)
            : base("Value out of range",
                  "Range validation",
                  originalValue,
                  obj => ((int)originalValue(obj)).Between(start: min, end: max))
        {
            _baseValidation = baseValidation;
        }

        public RangeValidation(string messageOnError, string name, Func<T, object> originalValue, int min, int max, IValidation<T>? baseValidation = null)
            : base(messageOnError, name, originalValue, obj => ((int)originalValue(obj)).Between(start: min, end: max))
        {
            _baseValidation = baseValidation;
        }

        // ========================================

        public RangeValidation(Func<T, object> originalValue, DateTime min, DateTime max, IValidation<T>? baseValidation = null)
            : base("Value out of range",
                  "Range validation",
                  originalValue,
                  obj => ((DateTime)originalValue(obj)).Between(start: min, end: max))
        {
            _baseValidation = baseValidation;
        }

        public RangeValidation(string messageOnError, string name, Func<T, object> originalValue, DateTime min, DateTime max, IValidation<T>? baseValidation = null)
            : base(messageOnError, name, originalValue, obj => ((DateTime)originalValue(obj)).Between(start: min, end: max))
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