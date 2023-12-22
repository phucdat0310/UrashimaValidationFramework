namespace UrashimaValidation
{
    public interface IValidation<T>
    {
        public string Name { get; }

        public Func<T, bool> ValidationFunction { get; }

        public string MessageOnError { get; }

        public Func<T, object> OriginalValue { get; }

        public virtual bool IsValid(T value) => ValidationFunction(arg: value);
    }
}
