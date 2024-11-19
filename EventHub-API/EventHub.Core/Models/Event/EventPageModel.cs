namespace EventHub.Core.Models.Event
{
    /// <summary>
    /// Model for page with Events
    /// </summary>
    public class EventPageModel
    {
        /// <summary>
        /// Collection of Events for page
        /// </summary>
        public IEnumerable<EventInfoModel> Events { get; set; } = new List<EventInfoModel>();

        /// <summary>
        /// Pages count
        /// </summary>
        public double PagesCount { get; set; }

        /// <summary>
        /// Current page
        /// </summary>
        public int CurrentPage { get; set; }
    }
}
