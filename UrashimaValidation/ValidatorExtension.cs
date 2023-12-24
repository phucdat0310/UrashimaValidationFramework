using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace UrashimaValidation
{
    public static class ValidatorExtension
    {
        public static Validator<T> Add<T>(
            this Validator<T> validator,
            IValidation<T> validation)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.AddValidation(validation: validation);

            return validator;
        }

        public static Validator<T> Add<T>(
            this Validator<T> validator,
            IEnumerable<IValidation<T>> validations)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.AddValidation(validations: validations);

            return validator;
        }

        public static bool IsAllValid(this IEnumerable<ValidationResponse> validationResponse)
        {
            return validationResponse.All((val) => val.Type != ValidationResponseType.Error);
        }

        public static Validator<T> AddAttributeValidation<T>(this Validator<T> validator)
        {
            Type type = typeof(T);

            // Loop through all properties of the class
            foreach (PropertyInfo property in type.GetProperties())
            {
                // Get the custom attributes for the property
                object[] attributes = property.GetCustomAttributes(true);

                // Loop through the attributes
                foreach (ValidationAttribute attribute in attributes.Cast<ValidationAttribute>())
                {
                    validator.AddValidation(new Validation<T>(
                        messageOnError: string.IsNullOrEmpty(attribute.ErrorMessage) ? validator.GetAttributeValidationMessage(attribute.GetType().Name + " failed") : validator.GetAttributeValidationMessage(attribute.ErrorMessage),
                        name: validator.GetAttributeValidationMessage(property.Name + ", Attribute name: " + attribute.GetType().Name),
                        originalValue: obj => property.GetValue(obj)!,
                        validationFunction: obj => attribute.IsValid(property.GetValue(obj)))
                    );
                }
            }

            return validator;
        }
    }
}