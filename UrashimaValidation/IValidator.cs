namespace UrashimaValidation
{
    public interface IValidator<T>
    {
        IReadOnlyCollection<IValidation<T>> Validations { get; }
        void AddValidation(IValidation<T> validation);
        void AddValidation(IEnumerable<IValidation<T>> validations);
        void ClearValidations();
        IEnumerable<ValidationResponse> ValidateList(IEnumerable<T> values);
        IEnumerable<ValidationResponse> ValidateSingleValue(T value);
        IEnumerable<ValidationResponse> ValidateWithFilter(T value, Func<IValidation<T>, bool> wherePredicate);
        IEnumerable<ValidationResponse> ValidateWithNameFilter(T value, string nameFilter);
        bool ReturnOnlyErrors { get; set; }
    }
}