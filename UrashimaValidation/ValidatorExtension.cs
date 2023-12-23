using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace UrashimaValidation
{
    public static class ValidatorExtension
    {
        /// <summary>
        /// Adds a new <see cref="IValidation{T}"/> to the <see cref="Validator{T}" />
        /// </summary>
        /// <typeparam name="T">The Type to validate</typeparam>
        /// <param name="validator">The validator</param>
        /// <param name="validation">The validation to add</param>
        /// <returns>Returns the Instance of <see cref="Validator{T}"/></returns>
        /// <exception cref="InvalidOperationException">Occurs if the instance of <see cref="Validator{T}"/> is null</exception>
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

        /// <summary>
        /// Adds a <see cref="IEnumerable{IValidation}"/> to the <see cref="Validator{T}" />
        /// </summary>
        /// <typeparam name="T">The Type to validate</typeparam>
        /// <param name="validator">The validator</param>
        /// <param name="validations">The validations to add</param>
        /// <returns>Returns the Instance of <see cref="Validator{T}"/></returns>
        /// <exception cref="InvalidOperationException">Occurs if the instance of <see cref="Validator{T}"/> is null</exception>
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

        /// <summary>
        /// Returns true if all validations are successful
        /// </summary>
        /// <param name="validationResponse">The list of the response of the validations</param>
        /// <returns>True if all responses are successful</returns>
        public static bool IsAllValid(this IEnumerable<ValidationResponse> validationResponse)
        {
            return validationResponse.All((val) => val.Type != ValidationResponseType.Error);
        }

        /// <summary>
        /// Convert attributes to validations
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator"></param>
        /// <returns></returns>
        public static Validator<T> AddAttributeValidation<T>(this Validator<T> validator)
        {
            Type type = typeof(T);

            // Loop through all properties of the class
            foreach (PropertyInfo property in type.GetProperties())
            {
                // Get the custom attributes for the property
                object[] attributes = property.GetCustomAttributes(true);

                // Loop through the attributes
                foreach (ValidationAttribute attribute in attributes)
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