using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation
{
    public class Validator<T> : IValidator<T>
    {

        private readonly List<AbstractValidation<T>> _validations = new List<AbstractValidation<T>>();


        #region SINGLETON
        private Validator()
        {
        }

        private static Validator<T>? instance = null;
        public static Validator<T> GetInstance()
        {
            if (instance == null)
            {
                instance = new Validator<T>();
            }
            return instance;
        }
        #endregion

        /// <inheritdoc />
        public IReadOnlyCollection<AbstractValidation<T>> Validations => _validations.AsReadOnly();

        /// <inheritdoc />
        public bool ReturnOnlyErrors { get; set; } = false;

        /// <summary>
        /// The internal cache
        /// </summary>
        protected Dictionary<string, bool> Cache { get; set; } = new Dictionary<string, bool>();

        /// <inheritdoc />
        public IEnumerable<ValidationResponse> ValidateSingleValue(T value)
        {
            return ValidateWithFilter(
                value: value,
                wherePredicate: null);
        }

        /// <inheritdoc />
        public IEnumerable<ValidationResponse> ValidateWithNameFilter(
            T value,
            string? nameFilter = null)
        {
            Func<AbstractValidation<T>, bool>? wherePredicate = null;

            if (!string.IsNullOrEmpty(nameFilter))
            {
                wherePredicate = (i) => i.Name == nameFilter;
            }

            return ValidateWithFilter(
                value: value,
                wherePredicate: wherePredicate);
        }

        /// <inheritdoc />
        public IEnumerable<ValidationResponse> ValidateList(IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                IEnumerable<ValidationResponse> responses = ValidateSingleValue(value: value);

                foreach (ValidationResponse response in responses)
                {
                    yield return response;
                }
            }
        }

        /// <inheritdoc />
        public IEnumerable<ValidationResponse> ValidateWithFilter(
            T value,
            Func<AbstractValidation<T>, bool>? wherePredicate = null)
        {
            wherePredicate = wherePredicate ?? new Func<AbstractValidation<T>, bool>((i) => true);

            return ValidateWithCachingDisabled(value: value, wherePredicate: wherePredicate);
        }

        private IEnumerable<ValidationResponse> ValidateWithCachingDisabled(
            T value,
            Func<AbstractValidation<T>, bool> wherePredicate)
        {
            foreach (AbstractValidation<T> validation in Validations.Where(predicate: wherePredicate))
            {
                bool valid = validation.IsValid(value);

                if ((!valid && ReturnOnlyErrors) || !ReturnOnlyErrors)
                {
                    yield return CreateValidationResponse(
                        valid: valid,
                        validation: validation,
                        value: value);
                }
            }
        }

        /// <inheritdoc />
        public void AddValidation(AbstractValidation<T> validation)
        {
            if (!Validations.Any(a => a.Name == validation.Name))
            {
                _validations.Add(item: validation);
            }
        }

        /// <inheritdoc />
        public void AddValidation(IEnumerable<AbstractValidation<T>> validations)
        {
            foreach (AbstractValidation<T> validation in validations)
            {
                AddValidation(validation: validation);
            }
        }

        /// <inheritdoc />
        public void ClearValidations()
        {
            _validations.Clear();
        }

        private ValidationResponse CreateValidationResponse(
            bool valid,
            AbstractValidation<T> validation,
            T value)
        {
            return new ValidationResponse(
                type: valid ? ValidationResponseType.Success : ValidationResponseType.Error,
                name: validation.Name,
                message: valid ? validation.MessageOnSuccess : validation.MessageOnError,
                originalValue: validation.OriginalValue(arg: value));
        }

        public string GetAttributeValidationMessage(string errorMessage)
        {
            var builder = new StringBuilder();
            builder.Append("Attribute Validation: ");
            builder.Append(errorMessage);
            return builder.ToString();
        }
    }
}
