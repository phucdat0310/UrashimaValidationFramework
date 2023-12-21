using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation
{
    public class ValidationFactory<T>
    {
        public static AbstractValidation<T> GetCreditCard(string validationType)
        {
            switch (validationType.ToLower())
            {
                case "attribute":
                    return new Validation<T>();
                case "customAttribute":
                    return new Titanium();
                default:
                    throw new ArgumentException("Invalid credit card type");
            }
        }
    }
}
