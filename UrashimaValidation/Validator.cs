using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace UrashimaValidation
{
    public class Validator<T> : IValidator<T>
    {
        #region COMPOSITE
        private readonly List<IValidation<T>> _validations = new List<IValidation<T>>();
        private readonly IValidation<T> _validationComposite;
        #endregion

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
        public IReadOnlyCollection<IValidation<T>> Validations => _validations.AsReadOnly();

        public IValidation<T> ValidationComposite
        {
            get
            {
                if (_validationComposite == null)
                {
                    return new ValidationComposite<T>(_validations);
                }

                return _validationComposite;
            }
        }

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
            Func<IValidation<T>, bool>? wherePredicate = null;

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
            Func<IValidation<T>, bool>? wherePredicate = null)
        {
            wherePredicate = wherePredicate ?? new Func<IValidation<T>, bool>((i) => true);

            return ValidateWithCachingDisabled(value: value, wherePredicate: wherePredicate);
        }

        private IEnumerable<ValidationResponse> ValidateWithCachingDisabled(
            T value,
            Func<IValidation<T>, bool> wherePredicate)
        {
            foreach (IValidation<T> validation in Validations.Where(predicate: wherePredicate))
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

        public bool IsAllValid(T value)
        => ValidationComposite.IsValid(value);

        public IEnumerable<ValidationResponse> ValidateSingleValueComposite(T value)
        => ValidationComposite.Validate(value);

        /// <inheritdoc />
        public void AddValidation(IValidation<T> validation)
        {
            if (!Validations.Any(a => a.Name == validation.Name))
            {
                _validations.Add(item: validation);
            }
        }

        /// <inheritdoc />
        public void AddValidation(IEnumerable<IValidation<T>> validations)
        {
            foreach (IValidation<T> validation in validations)
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
            IValidation<T> validation,
            T value)
        {
            return new ValidationResponse(
                type: valid ? ValidationResponseType.Success : ValidationResponseType.Error,
                name: validation.Name,
                message: validation.MessageOnError,
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