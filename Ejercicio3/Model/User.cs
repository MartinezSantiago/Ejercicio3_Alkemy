using Microsoft.AspNetCore.Identity;

namespace Ejercicio3.Models
{
    public class User: IdentityUser
    {
        public bool IsActive { get; set; }
    }
}
