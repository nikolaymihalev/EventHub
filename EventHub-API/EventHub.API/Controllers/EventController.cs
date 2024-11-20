using EventHub.API.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.Event;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService eventService;
        
        public EventController(IEventService _eventService)
        {
            eventService = _eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents(int currentPage = 1, string? userId = null)
        {
            var model = await eventService.GetEventsForPageAsync(currentPage, userId);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventFormModel model)
        {
            try
            {
                await eventService.AddAsync(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { Message = string.Format(SuccessfullMessages.Created, "Event")});
        }

        [HttpDelete("{id}/user/{userId}")]
        public async Task<IActionResult> Delete(int id, string userId)
        {
            try
            {
                await eventService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { Message = string.Format(SuccessfullMessages.Deleted, "Event") });
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(string userId, [FromBody] EventFormModel model)
        {
            try
            {
                await eventService.EditAsync(model, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { Message = string.Format(SuccessfullMessages.Updated, "Event") });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = new EventInfoModel();

            try
            {
                model = await eventService.GetEventByIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(model);
        }
    }
}
