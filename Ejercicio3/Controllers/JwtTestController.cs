using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ejercicio3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JwtTestController : ControllerBase
    {
      

        // GET api/<JwtTestController>/5
        [HttpGet]
        public string Get(int id)
        {
            return "JuanCarlos";
        }

        // POST api/<JwtTestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<JwtTestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<JwtTestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
