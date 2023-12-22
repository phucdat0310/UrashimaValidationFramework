// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using UrashimaValidation;

var customer = new Customer();

var validation = ValidationFactory<Customer>.CreateValidation("Validation", item => item.Name, (customer) => customer.Name.Length > 6, "Name length must be greater than 6");
var emailCombineValidation = ValidationFactory<Customer>.CreateValidation(
        "NotEmptyValidation",
        item => item.Name,
        baseValidation: validation);
List<IValidation<Customer>> list = new List<IValidation<Customer>>
{
    #region DECORATOR
    // pass a base validation to the "Not Empty" validation
    
    #endregion
    ValidationFactory<Customer>.CreateValidation("EmailValidation", item => item.Email, baseValidation: emailCombineValidation)
};

Validator<Customer> validator = Validator<Customer>.GetInstance()!
    .AddAttributeValidation()
    .EnableReturnOnlyErrors()
    .Add(validations: list);

var response = validator.ValidateSingleValue(customer);

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