using EventHub.API.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            string result = "";
            try
            {
                result = await userService.RegisterAsync(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

            return Ok(new { Message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Unsuccessful login" });

            string result = await userService.LoginAsync(model);

            if(result == SuccessfullMessages.Logged)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, model.Email),
                    new Claim(ClaimTypes.Role, "User")
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var keyBytes = Encoding.UTF8.GetBytes(Variables.JwtKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(keyBytes),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { token = tokenString });
            }

            return BadRequest(new { Message = result });
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserInfoModel model)
        {
            string result = "";
            try
            {
                result = await userService.UpdateUserInfoAsync(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

            return Ok(new { Message = result });
        }        

        [HttpGet("getUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { Message = "No token provided" });
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var keyBytes = Encoding.UTF8.GetBytes(Variables.JwtKey); 

                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero 
                }, out var validatedToken);

                var email = principal.FindFirst(ClaimTypes.Name)?.Value;

                var userInfo = await userService.GetUserInfoAsync(email);

                if (userInfo is null)
                    throw new Exception();

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = "Invalid token"});
            }
        }

        [HttpGet("get-information")]
        public async Task<IActionResult> GetInformationAboutUser(string userId)
        {
            var model = await userService.GetInformationAboutUserAsync(userId);                
            
            if(model is null)
                return BadRequest(new { Message = "User doesnt't exist!" });

            return Ok(model);
        }
    }
}
