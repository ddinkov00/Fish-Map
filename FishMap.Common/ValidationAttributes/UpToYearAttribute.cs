namespace FishMap.Common.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class UpToYearAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;
            if (DateTime.UtcNow.CompareTo(value) <= 0 && DateTime.UtcNow.AddYears(+1).CompareTo(value) >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Датата трябва да до една година напред!");
            }
        }
    }
}
