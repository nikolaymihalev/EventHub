using EventHub.Core.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.Comment;
using EventHub.Infrastructure.Common;
using EventHub.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository repository;

        public CommentService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task AddAsync(CommentFormModel model)
        {
            try
            {
                var comment = new Comment()
                {
                    Content = model.Content,
                    CreatedAt = DateTime.Now,
                    EventId = model.EventId,
                    UserId = model.UserId,
                };

                await repository.AddAsync(comment);
                await repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException(ErrorMessages.OperationFailedErrorMessage);
            }
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var comment = await repository.GetByIdAsync<Comment>(id);

            if (comment == null || comment.UserId != userId)
                throw new ArgumentException(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters"));           

            await repository.DeleteAsync<Comment>(id);
            await repository.SaveChangesAsync();
        }

        public async Task EditAsync(CommentFormModel model, string userId)
        {
            var comment = await repository.GetByIdAsync<Comment>(model.Id);

            if(comment == null || comment.UserId != userId)
                throw new ArgumentException(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters"));

            comment.Content = model.Content;

            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentInfoModel>> GetEventCommentsAsync(int eventId)
        {
            return await repository.AllReadonly<Comment>()
                .Where(x => x.EventId == eventId)
                .Select(x => new CommentInfoModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    EventId = x.EventId,
                    Content = x.Content,
                    CreatedAt = x.CreatedAt.ToString(VariablesConstants.DataFormat)
                }).ToListAsync();
        }
    }
}
