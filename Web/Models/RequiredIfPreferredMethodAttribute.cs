using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public sealed class RequiredIfPreferredMethodAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var form = (ContactForm)context.ObjectInstance;
            return value == null && context.MemberName == form.PreferredMethod.ToString()
                ? new ValidationResult(context.DisplayName + " is required")
                : null;
        }
    }
}