using EventHub.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventHub.Infrastructure.Models
{
    /// <summary>
    /// Represents a user in the EventHub application.
    /// </summary>
    [Comment("Represents a user")]
    public class User
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        [Key]
        [Comment("User's identifier")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Username chosen by the user.
        /// </summary>
        [Required]
        [Comment("User's username")]
        [MaxLength(ValidationConstants.UserUsernameMaxLength)]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Email address of the user.
        /// </summary>
        [Required]
        [Comment("User's email")]
        [MaxLength(ValidationConstants.UserEmailMaxLength)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Hashed password for secure authentication.
        /// </summary>
        [Required]
        [Comment("User's password")]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// First name of the user.
        /// </summary>
        [Required]
        [Comment("User's first name")]
        [MaxLength(ValidationConstants.UserNameMaxLength)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last name of the user.
        /// </summary>
        [Required]
        [Comment("User's last name")]
        [MaxLength(ValidationConstants.UserNameMaxLength)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Date and time the user was created.
        /// </summary>
        [Required]
        [Comment("User's creation date")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Events created by the user.
        /// </summary>
        public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();

        /// <summary>
        /// Comments created by the user.
        /// </summary>
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        /// <summary>
        /// Events the user has registered for.
        /// </summary>
        public ICollection<EventRegistration> Registrations { get; set; } = new List<EventRegistration>();
    }
}
