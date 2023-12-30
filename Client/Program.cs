// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using UrashimaValidation;

var customer = new Customer();

var validation = ValidationFactory<Customer>.CreateValidation(item => item.Name, (customer) => customer.Name.Length > 6, "Name length must be greater than 6");

#region DECORATOR
var emailCombineValidation = new NotEmptyValidationFactory<Customer>().Create(
    item => item.Name,
    baseValidation: validation);
#endregion

List<IValidation<Customer>> list = new List<IValidation<Customer>>
{
    // pass a base validation to the "Not Empty" validation
    new EmailValidationFactory<Customer>().Create(item => item.Email, baseValidation: emailCombineValidation),
    ValidationFactory<Customer>.CreateRegexValidation(item => item.Email, pattern: @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$", errorMessage: "Password is WEAK!")
};

Validator<Customer> validator = Validator<Customer>.GetInstance()!
    .AddAttributeValidation()
    .Add(validations: list);

var response = validator.ValidateSingleValueComposite(customer);

foreach (var item in response)
{
    Console.WriteLine(item.Message);
}

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