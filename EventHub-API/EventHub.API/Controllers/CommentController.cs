using EventHub.API.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.Comment;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService _commentService)
        {
            commentService = _commentService;
        }

        [HttpGet("get-all/{eventId}")]
        public async Task<IActionResult> GetComments(int eventId) 
        {
            var model = await commentService.GetEventCommentsAsync(eventId);

            return Ok(model);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody] CommentFormModel model)
        {
            try
            {
                await commentService.AddAsync(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { Message = string.Format(SuccessfullMessages.Created, "Comment") });
        }

        [HttpDelete("delete/{id}/user/{userId}")]
        public async Task<IActionResult> Delete(int id, string userId)
        {
            try
            {
                await commentService.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { Message = string.Format(SuccessfullMessages.Deleted, "Comment") });
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> Update(string userId, [FromBody] CommentFormModel model)
        {
            try
            {
                await commentService.EditAsync(model, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { Message = string.Format(SuccessfullMessages.Updated, "Comment") });
        }
    }
}
