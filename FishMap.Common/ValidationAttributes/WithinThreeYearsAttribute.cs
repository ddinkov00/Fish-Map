﻿namespace FishMap.Common.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WithinThreeYearsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;
            if (DateTime.UtcNow.AddYears(-3).CompareTo(value) <= 0 && DateTime.UtcNow.CompareTo(value) >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Датата трябва да е през последните 3 години!");
            }
        }
    }
}
