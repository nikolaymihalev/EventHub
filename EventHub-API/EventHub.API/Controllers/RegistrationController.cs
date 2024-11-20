using EventHub.API.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.EventRegistration;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IEventRegistrationService eventRegistrationService;

        public RegistrationController(IEventRegistrationService _eventRegistrationService)
        {
            eventRegistrationService = _eventRegistrationService;
        }

        [HttpGet("all/{userId}")]
        public async Task<IActionResult> GetRegistrations(string userId)
        {
            var model = await eventRegistrationService.GetUserEventRegistrationsAsync(userId);

            return Ok(model);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody]RegistrationFormModel model)
        {
            try
            {
                await eventRegistrationService.AddAsync(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { Message = string.Format(SuccessfullMessages.Created, "Registration for event") });
        }

        [HttpDelete("delete/{id}/user/{userId}")]
        public async Task<IActionResult> Delete(int id, string userId)
        {
            try
            {
                await eventRegistrationService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { Message = string.Format(SuccessfullMessages.Deleted, "Registration for event") });
        }
    }
}
