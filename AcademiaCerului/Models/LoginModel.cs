using System.ComponentModel.DataAnnotations;

namespace AcademiaCerului.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Numele utilizatorului este obligatoriu")]
        [Display(Name = "Utilizator")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie")]
        [Display(Name = "Parolă")]
        public string Password { get; set; }
    }
}