using System.ComponentModel.DataAnnotations;

namespace EventHub.Core.Models.User
{
    /// <summary>
    /// Model for user information
    /// </summary>
    public class UserInfoModel
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        [Key]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Username chosen by the user.
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Email address of the user.
        /// </summary>
        [Required]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// First name of the user.
        /// </summary>
        [Required]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last name of the user.
        /// </summary>
        [Required]
        public string LastName { get; set; } = string.Empty;
    }
}
