namespace UrashimaValidation
{
    public interface IValidation<T>
    {
        public string Name { get; }

        public Func<T, bool> ValidationFunction { get; }

        public string MessageOnError { get; }

        public Func<T, object> OriginalValue { get; }

        public bool IsValid(T value) => ValidationFunction(arg: value);

        public IEnumerable<ValidationResponse> Validate(T value);
    }
}
