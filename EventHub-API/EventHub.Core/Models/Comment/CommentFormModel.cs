using EventHub.Core.Constants;
using EventHub.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace EventHub.Core.Models.Comment
{
    /// <summary>
    /// Model for adding or editing Comment
    /// </summary>
    public class CommentFormModel
    {
        /// <summary>
        /// Unique identifier for the comment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Content of the comment.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        [StringLength(ValidationConstants.CommentContentMaxLength,
            MinimumLength = ValidationConstants.CommentContentMinLength,
            ErrorMessage = ErrorMessages.StringLengthErrorMessage)]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the user who wrote the comment.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the event the comment is associated with.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        public int EventId { get; set; }
    }
}
