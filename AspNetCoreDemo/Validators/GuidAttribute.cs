using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemo.Validators
{
    public class GuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            Guid guid;
            if (Guid.TryParse(value.ToString(), out guid) == false)
                return new ValidationResult($"The value '{value.ToString()}' is invalid, it must be a Guid");

            return ValidationResult.Success;

        }
    }
}
