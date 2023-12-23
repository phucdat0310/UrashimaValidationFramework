namespace UrashimaValidation
{
    public class Validation<T> : IValidation<T>
    {
        private string _messageOnError;
        private readonly string _name;
        private readonly Func<T, object> _originalValue;
        private readonly Func<T, bool> _validationFunction;

        public Validation(
            string messageOnError,
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
            _name = name;
            _originalValue = originalValue;
            _validationFunction = validationFunction ?? throw new ArgumentNullException(
                message: $"The parameter {nameof(validationFunction)} can not be null",
                paramName: nameof(validationFunction));
        }

        /// <inheritdoc />
        public virtual string MessageOnError
        {
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
        public string Name => _name;

        /// <inheritdoc />
        public virtual Func<T, object> OriginalValue => _originalValue;

        /// <inheritdoc />
        public virtual Func<T, bool> ValidationFunction => _validationFunction;

        public virtual bool IsValid(T value) => ValidationFunction(value);

        public bool IsBaseSuccess(IValidation<T>? baseValidation, T value)
        {
            if (baseValidation != null && !baseValidation.IsValid(value))
            {
                MessageOnError += "\n" + baseValidation.MessageOnError;
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"Name: {Name}";
        }

        IEnumerable<ValidationResponse> IValidation<T>.Validate(T value)
        {
            return new List<ValidationResponse>()
            {
                new ValidationResponse(
                type: IsValid(value) ? ValidationResponseType.Success : ValidationResponseType.Error,
                name: Name,
                message: MessageOnError,
                originalValue: OriginalValue(arg: value))
            };
        }
    }
}