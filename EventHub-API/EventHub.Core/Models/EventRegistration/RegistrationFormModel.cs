using EventHub.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace EventHub.Core.Models.EventRegistration
{
    /// <summary>
    /// Model for adding and editing Registration
    /// </summary>
    public class RegistrationFormModel
    {
        /// <summary>
        /// Unique identifier for the event registration.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key referencing the registered user.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the registered event.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        public int EventId { get; set; }

        /// <summary>
        /// Date and time of the registration.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        public DateTime RegisteredAt { get; set; }
    }
}
