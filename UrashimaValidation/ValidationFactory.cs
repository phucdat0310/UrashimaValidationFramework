using UrashimaValidation.CommonValidation;

namespace UrashimaValidation
{
    public class ValidationFactory<T>
    {
        private static int _instanceNumber = 0;

        public static int InstanceNumber
        {
            get
            {
                return _instanceNumber;
            }
            private set
            {
                _instanceNumber = value;
            }
        }

        #region FACTORY
        public static IValidation<T> CreateValidation(string validationType, Func<T, object> value, Func<T, bool>? validationFunc = null, string? errorMessage = null, IValidation<T>? baseValidation = null)
        {
            switch (validationType)
            {
                case "Validation":
                    InstanceNumber++;
                    return new Validation<T>(
                        messageOnError: string.IsNullOrEmpty(errorMessage) ? "Validation #" + InstanceNumber + ": This field do not match the validation" : errorMessage,
                        name: "Validation from factory: " + InstanceNumber,
                        originalValue: value,
                        validationFunction: validationFunc
                    );

                case "EmailValidation":
                    InstanceNumber++;
                    return new EmailValidation<T>(
                        messageOnError: string.IsNullOrEmpty(errorMessage) ? "Email validation #" + InstanceNumber + ": This field is not an email" : errorMessage,
                        name: "Email validation from factory: " + InstanceNumber,
                        originalValue: value,
                        baseValidation
                    );

                case "NotEmptyValidation":
                    InstanceNumber++;
                    return new NotEmptyValidation<T>(
                        messageOnError: string.IsNullOrEmpty(errorMessage) ? "Not empty validation #" + InstanceNumber + ": This field cannot be empty" : errorMessage,
                        name: "Not empty validation from factory: " + InstanceNumber,
                        originalValue: value,
                        baseValidation
                    );
                
                default:
                    throw new ArgumentException("Invalid validation type");
            }
        }

        public static IValidation<T> CreateRegexValidation(Func<T, object> value, string pattern, string? errorMessage = null, IValidation<T>? baseValidation = null)
        {
            InstanceNumber++;
            return new RegexValidation<T>(
                messageOnError: string.IsNullOrEmpty(errorMessage) ? "Regex validation #" + InstanceNumber + ": The value is not match for pattern provided" : errorMessage,
                name: "Regex validation from factory: " + InstanceNumber,
                originalValue: value,
                pattern: pattern,
                baseValidation
            );
        }

        #endregion
    }
}