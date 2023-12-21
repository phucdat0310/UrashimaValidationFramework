// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using UrashimaValidation;
using UrashimaValidation.CommonValidation;

var customer = new Customer();

List<AbstractValidation<int>> list = new List<AbstractValidation<int>>();
List<AbstractValidation<Customer>> list2 = new List<AbstractValidation<Customer>>();

list.Add(new Validation<int>(
        messageOnError: "số phải nhỏ hơn 3",
        messageOnSuccess: "số lớn hơn 3",
        name: "MOD",
        originalValue: (i) => i,
        validationFunction: (i) => i < 1));

list2.Add(new Validation<Customer>(
        messageOnError: "Name must not be empty",
        messageOnSuccess: "Name not empty",
        name: "Check name",
        originalValue: (customer) => customer.Name,
        validationFunction: (customer) => !string.IsNullOrEmpty(customer.Name)));

list2.Add(new Validation<Customer>(
        messageOnError: "Name length must be greater than 6",
        messageOnSuccess: "Name length greater than 6",
        name: "Check name length",
        originalValue: (customer) => customer.Name,
        validationFunction: (customer) => customer.Name.Length > 6
));

list2.Add(new NotEmptyValidation<Customer>(
        "Email must not be empty",
        (customer) => customer.Email
));

Validator<Customer> validator = Validator<Customer>.GetInstance()!
    .AddAttributeValidation()
    .EnableReturnOnlyErrors()
    .Add(validations: list2);
Validator<int> validator2 = Validator<int>.GetInstance()!
    .AddAttributeValidation()
    .EnableReturnOnlyErrors()
    .Add(validations: list);

var response = validator.ValidateSingleValue(customer);
var response2 = validator2.ValidateSingleValue(2);
//var response2 = validator.ValidateWithNameFilter(value: 3, "MOD");

foreach (var item in response)
{
    Console.WriteLine(item.Message);
}

foreach (var item in response2)
{
    Console.WriteLine(item.Message);
}


//foreach (var item in response2)
//{
//    Console.WriteLine(item.Message);
//}

Console.ReadKey();

class Customer
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    [Range(1, 50, ErrorMessage = "Age must be between 1 and 50")]
    public int Age { get; set; } = 0;
}

// factory
public interface IValidation
{
    bool IsValid();
}

public class NotEmptyValidation<T> : IValidation
{
    public NotEmptyValidation(string messageOnError, Func<T, object> originalValue)
        : base(messageOnError, "", "Not Empty Validation", originalValue, obj => !string.IsNullOrEmpty(originalValue(obj).ToString()))
    {
    }

    public bool IsValid()
    {
        // Perform validation logic here
        return true;
    }
}

public class RangeValidation<T> : IValidation
{
    public RangeValidation(string messageOnError, Func<T, object> originalValue, int min, int max)
        : base(messageOnError, "", "Range Validation", originalValue, obj => (int)originalValue(obj) >= min && (int)originalValue(obj) <= max)
    {
    }

    public bool IsValid()
    {
        // Perform validation logic here
        return true;
    }
}

public class ValidationFactory
{
    public static IValidation GetValidation<T>(string validationType, string messageOnError, Func<T, object> originalValue, int min = 0, int max = 0)
    {
        switch (validationType.ToLower())
        {
            case "notempty":
                return new NotEmptyValidation<T>(messageOnError, originalValue);
            case "range":
                return new RangeValidation<T>(messageOnError, originalValue, min, max);
            default:
                throw new ArgumentExcep