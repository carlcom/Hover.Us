using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ContactForm
    {
        [Display(Name = "Your Name"), Required]
        public string Name { get; set; }

        [Display(Name = "Your Company / Organization"), Required]
        public string Company { get; set; }

        [Display(Name = "Preferred Response Method"), Required]
        public ContactMethod? PreferredMethod { get; set; }

        [Display(Name = "Email Address"), RequiredIfPreferredMethod]
        public string Email { get; set; }

        [Display(Name = "Phone Number"), RequiredIfPreferredMethod]
        public string Phone { get; set; }

        [Display(Name = "Details"), Required]
        public string Message { get; set; }

        
    }

    public enum ContactMethod
    {
        Phone,
        Email
    }

    public class RequiredIfPreferredMethodAttribute : ValidationAttribute
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