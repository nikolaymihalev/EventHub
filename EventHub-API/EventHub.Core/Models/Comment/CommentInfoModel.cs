namespace EventHub.Core.Models.Comment
{
    /// <summary>
    /// Model for Comment information
    /// </summary>
    public class CommentInfoModel
    {
        /// <summary>
        /// Unique identifier for the comment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Content of the comment.
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Date and time the comment was created.
        /// </summary>
        public string CreatedAt { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the user who wrote the comment.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the event the comment is associated with.
        /// </summary>
        public int EventId { get; set; }
    }
}
