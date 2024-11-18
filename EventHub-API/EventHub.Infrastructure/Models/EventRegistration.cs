using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Infrastructure.Models
{
    /// <summary>
    /// Represents a user's registration for an event.
    /// </summary>
    [Comment("Represents a user's registration for an event")]
    public class EventRegistration
    {
        /// <summary>
        /// Unique identifier for the event registration.
        /// </summary>
        [Comment("Registration's identifier")]
        public int Id { get; set; }

        /// <summary>
        /// Foreign key referencing the registered user.
        /// </summary>
        [Required]
        [Comment("User's identifier")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the registered event.
        /// </summary>
        [Required]
        [Comment("Event's identifier")]
        public int EventId { get; set; }

        /// <summary>
        /// Date and time of the registration.
        /// </summary>
        [Required]
        [Comment("Registration date")]
        public DateTime RegisteredAt { get; set; }

        /// <summary>
        /// User who registered for the event.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        /// <summary>
        /// Event for which the user has registered.
        /// </summary>
        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!;
    }
}
