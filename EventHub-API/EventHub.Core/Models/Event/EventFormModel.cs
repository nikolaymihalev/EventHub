using EventHub.Core.Constants;
using EventHub.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace EventHub.Core.Models.Event
{
    /// <summary>
    /// Model for adding or editing Event
    /// </summary>
    public class EventFormModel
    {
        /// <summary>
        /// Unique identifier for the event.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the event.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        [StringLength(ValidationConstants.EventTitleMaxLength,
            MinimumLength = ValidationConstants.EventTitleMinLength,
            ErrorMessage = ErrorMessages.StringLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Description of the event.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        [StringLength(ValidationConstants.EventDescriptionMaxLength,
            MinimumLength = ValidationConstants.EventDescriptionMinLength,
            ErrorMessage = ErrorMessages.StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Date and time of the event.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Location of the event.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Category identifier
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        public int CategoryId { get; set; }

        /// <summary>
        /// Foreign key referencing the user who created the event.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        public string CreatorId { get; set; } = string.Empty;
    }
}
