using UrashimaValidation.Functions;

namespace UrashimaValidation.CommonValidation
{
    public class EmailValidation<T> : Validation<T>
    {
        private IValidation<T>? _baseValidation;

        public EmailValidation(Func<T, object> originalValue, IValidation<T>? baseValidation = null)
            : base("Invalid email address",
                  "Email validation",
                  originalValue,
                  obj => originalValue(obj).ToString()!.IsEMail())
        {
            _baseValidation = baseValidation;
        }

        public EmailValidation(string messageOnError, string name, Func<T, object> originalValue, IValidation<T>? baseValidation = null)
            : base(messageOnError, name, originalValue, obj => originalValue(obj).ToString()!.IsEMail())
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