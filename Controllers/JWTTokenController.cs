using DemoJWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DemoJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTTokenController : ControllerBase
    {
        public IConfiguration _configuration;
        

        public JWTTokenController(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostUser([FromForm] UserRequest userRequest) 
        {
            //if (user != null && user.UserName != null && user.Password != null)
            //{
                var userData = GetUser(userRequest);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

                if (userData != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }  
            
            return BadRequest("Invalid Credentials");
            
        }

        //[NonAction]
        private  User GetUser(UserRequest userRequest)
        {
             var res = DemoJWT.Models.User.users.FirstOrDefault(x=> x.UserName == userRequest.UserName && x.Password == userRequest.Password);

            return res;
        }

    }
}
