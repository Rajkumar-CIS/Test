using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;


    public class FutureDateAttribute : ValidationAttribute
    {

        public FutureDateAttribute()
        {
            ErrorMessage = "The due date must be greater than the current date.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dueDate = (DateTime)value;
                if (dueDate <= DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }