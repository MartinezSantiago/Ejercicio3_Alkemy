using Ejercicio3.Model;
using Ejercicio3.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ejercicio3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserController(Microsoft.AspNetCore.Identity.UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //Register
        [HttpPost]

        [Route("api/[controller]/Register")]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            var aux = await _userManager.FindByNameAsync(registerUser.UserName);
            if (aux != null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new
                {
                    Status = "Error",
                    Message = $"El usuario { registerUser.UserName} ya existe"
                });
            }
            var user = new User()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = "Error",
                    Message = $"Error del servidor"
                });
            }
            return Ok(new{Status="Success"});
        }

        [HttpPost]
        [Route("api/[controller]/Login")]
        public async Task<IActionResult> Login(RequestLoginUser user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName,user.Password,false,false);
            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(user.UserName);
                if (currentUser.IsActive)
                {
                    var token = await GetToken(currentUser);

                    return Ok(token); 

                }
                
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new
            {
                Status = "Error",
                Message = $"El usuario { user.UserName} no esta autorizado"
            }) ;
            


        }
        [HttpGet]
        [Route("api/[controller]/GetToken")]
        private async Task<RequestToken> GetToken(User currentUser)
        {
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,currentUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };
            AuthClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));
            var AuthSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JuanRomanRiquelme"));
            var token = new JwtSecurityToken(
               issuer: "https://localhost:5001",
      audience: "https://localhost:5001",
      expires: DateTime.Now.AddHours(1),
      claims: AuthClaims,
      signingCredentials: new SigningCredentials(AuthSignInKey, SecurityAlgorithms.HmacSha256)
       );

            return new RequestToken
            {
                
         TokenCode = new JwtSecurityTokenHandler().WriteToken(token),
         ValidTo = token.ValidTo
};

               
            
        }
    }
}
 