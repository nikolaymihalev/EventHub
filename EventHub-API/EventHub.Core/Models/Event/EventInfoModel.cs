namespace EventHub.Core.Models.Event
{
    /// <summary>
    /// Model for Event information
    /// </summary>
    public class EventInfoModel
    {
        /// <summary>
        /// Unique identifier for the event.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the event.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Description of the event.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Date and time of the event.
        /// </summary>
        public string Date { get; set; } = string.Empty;

        /// <summary>
        /// Location of the event.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Category identifier
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Foreign key referencing the user who created the event.
        /// </summary>
        public string CreatorId { get; set; } = string.Empty;
    }
}
