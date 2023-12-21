using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation
{
    public class Validation<T> : AbstractValidation<T>
    {
        private string _messageOnError;
        private readonly string _messageOnSuccess;
        private readonly string _name;
        private readonly Func<T, object> _originalValue;
        private readonly Func<T, bool> _validationFunction;

        public Validation(
            string messageOnError,
            string messageOnSuccess,
            string name,
            Func<T, object> originalValue,
            Func<T, bool> validationFunction)
        {
            if (string.IsNullOrEmpty(messageOnError))
            {
                throw new ArgumentException(
                    message: $"Parameter {nameof(messageOnError)} can not be empty or null",
                    paramName: nameof(messageOnError));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(
                    message: $"Parameter {nameof(name)} can not be empty or null",
                    paramName: nameof(name));
            }

            _messageOnError = messageOnError;
            _messageOnSuccess = messageOnSuccess;
            _name = name;
            _originalValue = originalValue;
            _validationFunction = validationFunction ?? throw new ArgumentNullException(
                message: $"The parameter {nameof(validationFunction)} can not be null",
                paramName: nameof(validationFunction));
        }

        /// <inheritdoc />
        public override string MessageOnError  {
            get
            {
                return _messageOnError;
            }
            protected set
            {
                _messageOnError = value;
            }
        }

        /// <inheritdoc />
        public override string MessageOnSuccess => _messageOnSuccess;

        /// <inheritdoc />
        public override string Name => _name;

        /// <inheritdoc />
        public override Func<T, object> OriginalValue => _originalValue;

        /// <inheritdoc />
        public override Func<T, bool> ValidationFunction => _validationFunction;
    }
}
