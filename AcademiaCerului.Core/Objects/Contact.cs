using System.ComponentModel.DataAnnotations;

namespace AcademiaCerului.Core.Objects
{
    public class Contact
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
