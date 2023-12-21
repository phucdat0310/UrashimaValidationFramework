using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation.CommonValidation
{
    public class NotEmptyValidation<T> : Validation<T>
    {
        public NotEmptyValidation(string messageOnError, Func<T, object> originalValue)
            : base(string.IsNullOrEmpty(messageOnError) ? "Field must not be empty" : messageOnError, 
                  "Field not empty", "Not Empty Validation", 
                  originalValue, 
                  obj => !string.IsNullOrEmpty(originalValue(obj).ToString()))
        {

        }

        public NotEmptyValidation(string messageOnError, string messageOnSuccess, string name, Func<T, object> originalValue, Func<T, bool> validationFunction)
            : base(messageOnError, messageOnSuccess, name, originalValue, validationFunction)
        {

        }
    }
}
