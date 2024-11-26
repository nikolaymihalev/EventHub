using EventHub.Core.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.Event;
using EventHub.Infrastructure.Common;
using EventHub.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository repository;

        public EventService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task AddAsync(EventFormModel model)
        {
            try
            {
                var eventModel = new Event()
                {
                    Title = model.Title,
                    Description = model.Description,
                    CreatorId = model.CreatorId,
                    Location = model.Location,
                    Date = model.Date,
                    CategoryId = model.CategoryId
                };

                await repository.AddAsync(eventModel);
                await repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException(ErrorMessages.OperationFailedErrorMessage);
            }
        }

        public async Task DeleteAsync(int id, string creatorId)
        {
            var eventM = await repository.GetByIdAsync<Event>(id);

            if (eventM == null || eventM.CreatorId != creatorId)
                throw new ArgumentException(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters"));

            await repository.DeleteAsync<Event>(id);
            await repository.SaveChangesAsync();
        }

        public async Task EditAsync(EventFormModel model, string creatorId)
        {
            var eventM = await repository.GetByIdAsync<Event>(model.Id);

            if (eventM == null || eventM.CreatorId != creatorId)
                throw new ArgumentException(string.Format(ErrorMessages.InvalidModelErrorMessage, "parameters"));

            eventM.Title = model.Title;
            eventM.Description = model.Description;
            eventM.Location = model.Location;
            eventM.Date = model.Date;
            eventM.CategoryId = model.CategoryId;

            await repository.SaveChangesAsync();
        }

        public async Task<EventInfoModel> GetEventByIdAsync(int id)
        {
            var eventM = await repository.GetByIdAsync<Event>(id);

            if (eventM == null)
                throw new ArgumentException(string.Format(ErrorMessages.DoesntExistErrorMessage, "event"));

            return new EventInfoModel()
            {
                Id = eventM.Id,
                Title = eventM.Title,
                Description = eventM.Description,
                CreatorId = eventM.CreatorId,
                Location = eventM.Location,
                Date = eventM.Date.ToString(VariablesConstants.DataFormat),
                CategoryId = eventM.CategoryId
            };
        }

        public async Task<EventPageModel> GetEventsForPageAsync(int currentPage = 1, string? userId = null)
        {
            var eventPageModel = new EventPageModel();
            int formula = (currentPage - 1) * VariablesConstants.MaxEventsPerPage;

            if (currentPage <= 1)
            {
                formula = 0;
            }

            eventPageModel.Events = await GetAllEventsAsync(userId);

            eventPageModel.PagesCount = Math.Ceiling(eventPageModel.Events.Count() / Convert.ToDouble(VariablesConstants.MaxEventsPerPage));

            eventPageModel.Events = eventPageModel.Events
               .Skip(formula)
               .Take(VariablesConstants.MaxEventsPerPage);

            eventPageModel.CurrentPage = currentPage;

            return eventPageModel;
        }

        public async Task<EventPageModel> GetSearchedEvents(int currentPage = 1, string searchTitle = "", int? categoryId = null)
        {
            var eventPageModel = new EventPageModel();
            int formula = (currentPage - 1) * VariablesConstants.MaxEventsPerPage;

            if (currentPage <= 1)
            {
                formula = 0;
            }

            eventPageModel.Events = await GetAllEventsAsync();

            eventPageModel.Events = eventPageModel.Events.Where(x => x.Title.ToLower().Contains(searchTitle.ToLower())).ToList();

            if(categoryId != null)
            {
                eventPageModel.Events = eventPageModel.Events.Where(x=>x.CategoryId==categoryId).ToList();
            }

            eventPageModel.PagesCount = Math.Ceiling(eventPageModel.Events.Count() / Convert.ToDouble(VariablesConstants.MaxEventsPerPage));

            eventPageModel.Events = eventPageModel.Events
               .Skip(formula)
               .Take(VariablesConstants.MaxEventsPerPage);

            eventPageModel.CurrentPage = currentPage;

            return eventPageModel;
        }

        private async Task<IEnumerable<EventInfoModel>> GetAllEventsAsync(string? userId = null) 
        {
            if (userId != null)             
                return await repository.AllReadonly<Event>()
                .Where(x => x.CreatorId == userId)
                .Select(x => new EventInfoModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreatorId = x.CreatorId,
                    Location = x.Location,
                    Date = x.Date.ToString(VariablesConstants.DataFormat),
                    CategoryId = x.CategoryId
                }).ToListAsync();
            

            return await repository.AllReadonly<Event>()
                .Select(x => new EventInfoModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreatorId = x.CreatorId,
                    Location = x.Location,
                    Date = x.Date.ToString(VariablesConstants.DataFormat),
                    CategoryId = x.CategoryId
                }).ToListAsync();
        }
    }
}
