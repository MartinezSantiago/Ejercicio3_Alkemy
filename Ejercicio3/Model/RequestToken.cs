using System.ComponentModel.DataAnnotations;

namespace Ejercicio3.Model
{
    public class RequestToken
    {
       
        [Required]
        public string ?TokenCode { get; set; }

        [Required]
        public DateTime ValidTo { get; set; }

       
    }
}
