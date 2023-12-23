namespace UrashimaValidation.CommonValidation
{
    public class NotEmptyValidation<T> : Validation<T>
    {
        #region DEPENDENCY INJECTION
        private IValidation<T>? _baseValidation;

        public NotEmptyValidation(Func<T, object> originalValue, IValidation<T>? baseValidation = null)
            : base("This field cannot be empty",
                  "Not Empty Validation",
                  originalValue,
                  obj => !string.IsNullOrEmpty(originalValue(obj).ToString()))
        {
            _baseValidation = baseValidation;
        }
        #endregion

        public NotEmptyValidation(string messageOnError, string name, Func<T, object> originalValue, IValidation<T>? baseValidation = null)
            : base(messageOnError, name, originalValue, obj => !string.IsNullOrEmpty(originalValue(obj).ToString()))
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