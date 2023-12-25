using UrashimaValidation.Functions;

namespace UrashimaValidation.CommonValidation
{
    public class ContainsValidation<T> : Validation<T>
    {
        private IValidation<T>? _baseValidation;

        public ContainsValidation(Func<T, object> originalValue, IEnumerable<T> values, IValidation<T>? baseValidation = null)
            : base("Object not exists in {values}",
                  "Contains validation",
                  originalValue,
                  obj => ((T)originalValue(obj)).In(values))
        {
            _baseValidation = baseValidation;
        }

        public ContainsValidation(string messageOnError, string name, Func<T, object> originalValue, IEnumerable<T> values, IValidation<T>? baseValidation = null)
            : base(messageOnError, name, originalValue, obj => ((T)originalValue(obj)).In(values))
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