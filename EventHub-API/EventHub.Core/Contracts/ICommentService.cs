using EventHub.Core.Models.Comment;

namespace EventHub.Core.Contracts
{
    /// <summary>
    /// Defines the operations related to managing comments for events.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Retrieves all comments associated with a specific event asynchronously.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event.</param>
        /// <returns>A task that represents the asynchronous operation. 
        ///     The task result contains a collection of 
        ///     <see cref="CommentInfoModel"/> for the specified event.</returns>
        Task<IEnumerable<CommentInfoModel>> GetEventCommentsAsync(int eventId);

        /// <summary>
        /// Adds a new comment asynchronously.
        /// </summary>
        /// <param name="model">The <see cref="CommentFormModel"/> containing the data for the new comment.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(CommentFormModel model);

        /// <summary>
        /// Edits an existing comment asynchronously.
        /// </summary>
        /// <param name="model">The <see cref="CommentFormModel"/> containing the updated data for the comment.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task EditAsync(CommentFormModel model, string userId);

        /// <summary>
        /// Deletes a comment by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the comment to delete.</param>
        /// <param name="userId">The identifier of the user attempting to delete the comment. 
        ///     Used for authorization checks.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(int id, string userId);
    }
}
