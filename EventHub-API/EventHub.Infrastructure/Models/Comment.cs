using EventHub.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Infrastructure.Models
{
    /// <summary>
    /// Represents a comment made by a user on an event.
    /// </summary>
    [Comment("Represents a comment")]
    public class Comment
    {
        /// <summary>
        /// Unique identifier for the comment.
        /// </summary>
        [Comment("Comment's identifier")]
        public int Id { get; set; }

        /// <summary>
        /// Content of the comment.
        /// </summary>
        [Required]
        [Comment("Comment's content")]
        [MaxLength(ValidationConstants.CommentContentMaxLength)]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Date and time the comment was created.
        /// </summary>
        [Required]
        [Comment("Comment's creation date")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Foreign key referencing the user who wrote the comment.
        /// </summary>
        [Required]
        [Comment("User's identifier")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the event the comment is associated with.
        /// </summary>
        [Required]
        [Comment("Event's identifier")]
        public int EventId { get; set; }

        /// <summary>
        /// User who wrote the comment.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        /// <summary>
        /// Event the comment is associated with.
        /// </summary>
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;
    }
}
