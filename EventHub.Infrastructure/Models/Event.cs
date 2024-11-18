using EventHub.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Infrastructure.Models
{
    /// <summary>
    /// Represents an event that can be created, viewed, or joined by users.
    /// </summary>
    [Comment("Represents an event")]
    public class Event
    {
        /// <summary>
        /// Unique identifier for the event.
        /// </summary>
        [Comment("Event's identifier")]
        public int Id { get; set; }

        /// <summary>
        /// Title of the event.
        /// </summary>
        [Required]
        [Comment("Event's title")]
        [MaxLength(ValidationConstants.EventTitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Description of the event.
        /// </summary>
        [Required]
        [Comment("Event's description")]
        [MaxLength(ValidationConstants.EventDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Date and time of the event.
        /// </summary>
        [Required]
        [Comment("Event's date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Location of the event.
        /// </summary>
        [Required]
        [Comment("Event's location")]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Category identifier
        /// </summary>
        [Required]
        [Comment("Category's identifier")]
        public int CategoryId { get; set; }

        /// <summary>
        /// URL to an image representing the event.
        /// </summary>
        [Required]
        [Comment("Event's image link")]
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the user who created the event.
        /// </summary>
        [Required]
        [Comment("Creator's identifier")]
        public string CreatorId { get; set; } = string.Empty;

        /// <summary>
        /// User who created the event.
        /// </summary>
        [ForeignKey(nameof(CreatorId))]
        public User Creator { get; set; } = null!;

        /// <summary>
        /// Category of the event
        /// </summary>
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        /// <summary>
        /// Users registered for the event.
        /// </summary>
        public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();

        /// <summary>
        /// Comments left on the event.
        /// </summary>
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
