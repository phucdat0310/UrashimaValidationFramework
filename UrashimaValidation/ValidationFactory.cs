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
        public static IValidation<T> CreateValidation(Func<T, object> value, Func<T, bool> validationFunc, string? errorMessage = null)
        {
            InstanceNumber++;
            return new Validation<T>(
                messageOnError: string.IsNullOrEmpty(errorMessage) ? "Validation #" + InstanceNumber + ": This field do not match the validation" : errorMessage,
                name: "Validation from factory: " + InstanceNumber,
                originalValue: value,
                validationFunction: validationFunc
            );
        }

        public static IValidation<T> CreateEmailValidation(Func<T, object> value, string? errorMessage = null, IValidation<T>? baseValidation = null)
        {
            InstanceNumber++;
            return new EmailValidation<T>(
                messageOnError: string.IsNullOrEmpty(errorMessage) ? "Email validation #" + InstanceNumber + ": This field is not an email" : errorMessage,
                name: "Email validation from factory: " + InstanceNumber,
                originalValue: value,
                baseValidation
            );
        }

        public static IValidation<T> CreateNotEmptyValidation(Func<T, object> value, string? errorMessage = null, IValidation<T>? baseValidation = null)
        {
            InstanceNumber++;
            return new NotEmptyValidation<T>(
                messageOnError: string.IsNullOrEmpty(errorMessage) ? "Not empty validation #" + InstanceNumber + ": This field cannot be empty" : errorMessage,
                name: "Not empty validation from factory: " + InstanceNumber,
                originalValue: value,
                baseValidation
            );
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

        #region RangeValidate
        public static IValidation<T> CreateRangeValidation(Func<T, object> value, double min, double max, string? errorMessage = null, IValidation<T>? baseValidation = null)
        {
            InstanceNumber++;
            return new RangeValidation<T>(
                messageOnError: string.IsNullOrEmpty(errorMessage) ? "Range validation #" + InstanceNumber + ": The value is not in range" : errorMessage,
                name: "Range validation from factory: " + InstanceNumber,
                originalValue: value,
                min: min,
                max: max,
                baseValidation
            );
        }

        public static IValidation<T> CreateRangeValidation(Func<T, object> value, float min, float max, string? errorMessage = null, IValidation<T>? baseValidation = null)
        {
            InstanceNumber++;
            return new RangeValidation<T>(
                messageOnError: string.IsNullOrEmpty(errorMessage) ? "Regex validation #" + InstanceNumber + ": The value is not in range" : errorMessage,
                name: "Regex validation from factory: " + InstanceNumber,
                originalValue: value,
                min: min,
                max: max,
                baseValidation
            );
        }

        public static IValidation<T> CreateRangeValidation(Func<T, object> value, int min, int max, string? errorMessage = null, IValidation<T>? baseValidation = null)
        {
            InstanceNumber++;
            return new RangeValidation<T>(
                messageOnError: string.IsNullOrEmpty(errorMessage) ? "Regex validation #" + InstanceNumber + ": The value is not in range" : errorMessage,
                name: "Regex validation from factory: " + InstanceNumber,
                originalValue: value,
                min: min,
                max: max,
                baseValidation
            );
        }

        public static IValidation<T> CreateRangeValidation(Func<T, object> value, DateTime min, DateTime max, string? errorMessage = null, IValidation<T>? baseValidation = null)
        {
            InstanceNumber++;
            return new RangeValidation<T>(
                messageOnError: string.IsNullOrEmpty(errorMessage) ? "Regex validation #" + InstanceNumber + ": The value is not in range" : errorMessage,
                name: "Regex validation from factory: " + InstanceNumber,
                originalValue: value,
                min: min,
                max: max,
                baseValidation
            );
        }
        #endregion

        public static IValidation<T> CreateIPv4Validation(Func<T, object> value, string? errorMessage = null, IValidation<T>? baseValidation = null)
        {
            InstanceNumber++;
            return new IPv4Validation<T>(
                messageOnError: string.IsNullOrEmpty(errorMessage) ? "IPv4 validation #" + InstanceNumber + ": This field is not an IPv4 Address" : errorMessage,
                name: "IPv4 validation from factory: " + InstanceNumber,
                originalValue: value,
                baseValidation
            );
        }

        public static IValidation<T> CreateIPv6Validation(Func<T, object> value, string? errorMessage = null, IValidation<T>? baseValidation = null)
        {
            InstanceNumber++;
            return new IPv6Validation<T>(
                messageOnError: string.IsNullOrEmpty(errorMessage) ? "IPv6 validation #" + InstanceNumber + ": This field is not an IPv6 Address" : errorMessage,
                name: "IPv6 validation from factory: " + InstanceNumber,
                originalValue: value,
                baseValidation
            );
        }

        public static IValidation<T> CreateIsTodayValidation(Func<T, object> value, string? errorMessage = null, IValidation<T>? baseValidation = null)
        {
            InstanceNumber++;
            return new IsTodayValidation<T>(
                messageOnError: string.IsNullOrEmpty(errorMessage) ? "IsToday validation #" + InstanceNumber + ": This field date is not today" : errorMessage,
                name: "IsToday validation from factory: " + InstanceNumber,
                originalValue: value,
                baseValidation
            );
        }

        #endregion
    }
}