namespace UrashimaValidation
{
    public interface IValidator<T>
    {
        IValidation<T> ValidationComposite { get; }
        IReadOnlyCollection<IValidation<T>> Validations { get; }
        void AddValidation(IValidation<T> validation);
        void AddValidation(IEnumerable<IValidation<T>> validations);
        void ClearValidations();
        IEnumerable<ValidationResponse> ValidateList(IEnumerable<T> values);
        IEnumerable<ValidationResponse> ValidateSingleValue(T value);
        IEnumerable<ValidationResponse> ValidateWithFilter(T value, Func<IValidation<T>, bool> wherePredicate);
        bool ReturnOnlyErrors { get; set; }
    }
}