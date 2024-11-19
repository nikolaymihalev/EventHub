namespace EventHub.Core.Models.EventRegistration
{
    /// <summary>
    /// Model for Registration information
    /// </summary>
    public class RegistrationInfoModel
    {
        /// <summary>
        /// Unique identifier for the event registration.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Foreign key referencing the registered user.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Foreign key referencing the registered event.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Date and time of the registration.
        /// </summary>
        public string RegisteredAt { get; set; } = string.Empty;

        /// <summary>
        /// Title of the event.
        /// </summary>
        public string EventTitle { get; set; } = string.Empty;
    }
}
