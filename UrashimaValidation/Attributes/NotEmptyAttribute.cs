using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrashimaValidation.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class NotEmptyAttribute : ValidationAttribute
    {
        public string Description { get; set; }

        public NotEmptyAttribute(string description)
        {
            Description = description;
        }

        
    }
}
