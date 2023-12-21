namespace UrashimaValidation
{
    public interface IValidator<T>
    {
        IReadOnlyCollection<AbstractValidation<T>> Validations { get; }
        void AddValidation(AbstractValidation<T> validation);
        void AddValidation(IEnumerable<AbstractValidation<T>> validations);
        void ClearValidations();
        IEnumerable<ValidationResponse> ValidateList(IEnumerable<T> values);
        IEnumerable<ValidationResponse> ValidateSingleValue(T value);
        IEnumerable<ValidationResponse> ValidateWithFilter(T value, Func<AbstractValidation<T>, bool> wherePredicate);
        IEnumerable<ValidationResponse> ValidateWithNameFilter(T value, string nameFilter);
        bool ReturnOnlyErrors { get; set; }
    }
}