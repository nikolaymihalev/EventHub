using EventHub.Core.Constants;
using EventHub.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace EventHub.Core.Models.User
{
    /// <summary>
    /// Model for user registration.
    /// Contains the user's username, email, first and last name, password, and a confirmation password field 
    /// required during the account registration process.
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// The username that the user will register with
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        [StringLength(ValidationConstants.UserUsernameMaxLength,
            MinimumLength = ValidationConstants.UserUsernameMinLength,
            ErrorMessage = ErrorMessages.StringLengthErrorMessage)]
        public string Username { get; set; } = null!;

        /// <summary>
        /// The user's email address used for account registration
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        [EmailAddress]
        [StringLength(ValidationConstants.UserEmailMaxLength,
            MinimumLength = ValidationConstants.UserEmailMinLength,
            ErrorMessage = ErrorMessages.StringLengthErrorMessage)]
        public string Email { get; set; } = null!;

        /// <summary>
        /// The user's first name used for account registration
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        [StringLength(ValidationConstants.UserNameMaxLength,
            MinimumLength = ValidationConstants.UserNameMinLength,
            ErrorMessage = ErrorMessages.StringLengthErrorMessage)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// The user's last name used for account registration
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        [StringLength(ValidationConstants.UserNameMaxLength,
            MinimumLength = ValidationConstants.UserNameMinLength,
            ErrorMessage = ErrorMessages.StringLengthErrorMessage)]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// The user's password used for account registration and future authentication
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.RequireErrorMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// A field to confirm the user's password, ensuring the password is entered
        /// </summary>
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
