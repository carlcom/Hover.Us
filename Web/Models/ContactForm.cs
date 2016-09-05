using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public sealed class ContactForm
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
}