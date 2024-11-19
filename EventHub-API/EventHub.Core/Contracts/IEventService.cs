using EventHub.Core.Models.Event;

namespace EventHub.Core.Contracts
{
    public interface IEventService
    {
        Task<EventPageModel> GetEventsForPageAsync(int currentPage = 1);
        Task<EventInfoModel> GetEventByIdAsync(int id);
        Task AddAsync(EventFormModel model);
        Task EditAsync(EventFormModel model, string creatorId);
        Task DeleteAsync(int id, string creatorId);
    }
}
