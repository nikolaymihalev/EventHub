using EventHub.Core.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        public UserController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            return Ok();
        }
    }
}
