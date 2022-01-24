using Ejercicio3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio3.Context
{
    public class UserContext:IdentityDbContext<User>
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        
    }
}
