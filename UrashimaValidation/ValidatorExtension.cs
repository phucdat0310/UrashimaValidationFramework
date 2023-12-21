using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation
{
    public static class ValidatorExtension
    {
        /// <summary>
        /// Only Errors are returning if validating is called />
        /// </summary>
        /// <typeparam name="T">The Type to validate</typeparam>
        /// <param name="validator">The validator</param>
        /// <returns>Returns the Instance of <see cref="Validator{T}"/> with <see cref="Validator{T}.ReturnOnlyErrors"/> set on true</returns>
        /// <exception cref="InvalidOperationException">Occurs if the instance of <see cref="Validator{T}"/> is null</exception>
        public static Validator<T> EnableReturnOnlyErrors<T>(this Validator<T> validator)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.ReturnOnlyErrors = true;

            return validator;
        }

        /// <summary>
        /// All items are returning if validating is called />
        /// </summary>
        /// <typeparam name="T">The Type to validate</typeparam>
        /// <param name="validator">The validator</param>
        /// <returns>Returns the Instance of <see cref="Validator{T}"/> with <see cref="Validator{T}.ReturnOnlyErrors"/> set on false</returns>
        /// <exception cref="InvalidOperationException">Occurs if the instance of <see cref="Validator{T}"/> is null</exception>
        public static Validator<T> DisableReturnOnlyErrors<T>(this Validator<T> validator)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.ReturnOnlyErrors = false;

            return validator;
        }

        /// <summary>
        /// Adds a new <see cref="AbstractValidation{T}"/> to the <see cref="Validator{T}" />
        /// </summary>
        /// <typeparam name="T">The Type to validate</typeparam>
        /// <param name="validator">The validator</param>
        /// <param name="validation">The validation to add</param>
        /// <returns>Returns the Instance of <see cref="Validator{T}"/></returns>
        /// <exception cref="InvalidOperationException">Occurs if the instance of <see cref="Validator{T}"/> is null</exception>
        public static Validator<T> Add<T>(
            this Validator<T> validator,
            AbstractValidation<T> validation)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.AddValidation(validation: validation);

            return validator;
        }

        /// <summary>
        /// Adds a <see cref="IEnumerable{AbstractValidation}"/> to the <see cref="Validator{T}" />
        /// </summary>
        /// <typeparam name="T">The Type to validate</typeparam>
        /// <param name="validator">The validator</param>
        /// <param name="validations">The validations to add</param>
        /// <returns>Returns the Instance of <see cref="Validator{T}"/></returns>
        /// <exception cref="InvalidOperationException">Occurs if the instance of <see cref="Validator{T}"/> is null</exception>
        public static Validator<T> Add<T>(
            this Validator<T> validator,
            IEnumerable<AbstractValidation<T>> validations)
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
                        messageOnSuccess: "",
                        name: validator.GetAttributeValidationMessage(property.Name + ", Attribute name: " + attribute.GetType().Name),
                        originalValue: obj => property.GetValue(obj),
                        validationFunction: obj => attribute.IsValid(property.GetValue(obj)))
                    );
                }
            }

            return validator;
        }
    }
}
