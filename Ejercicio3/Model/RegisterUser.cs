using System.ComponentModel.DataAnnotations;

namespace Ejercicio3.Models
{
    public class RegisterUser
    {
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public RegisterUser(string userName, string email,string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
         }
    }
}
