// MODEL VALIDATION WITH VALIDATIONATTRIBUTE

// To make Shirt_EnsureCorrectSizingAttribute an attribute we derive it from ValidationAttribute class

using System.ComponentModel.DataAnnotations;

namespace DemoWebAPI.Models.Validations
{
    public class Shirt_EnsureCorrectSizingAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var shirt = validationContext.ObjectInstance as Shirt;
            if (shirt != null && !string.IsNullOrWhiteSpace(shirt.Gender))
            {
                if(shirt.Gender.Equals("men",StringComparison.OrdinalIgnoreCase) && shirt.Size < 8)
                {
                    return new ValidationResult("Men shirt's size must be equal to or greater than 8");
                }
                else if (shirt.Gender.Equals("women", StringComparison.OrdinalIgnoreCase) && shirt.Size < 6)
                {
                    return new ValidationResult("Women shirt's size must be equal to or greater than 6");
                }
            }
            return ValidationResult.Success;
        }
    }
}
