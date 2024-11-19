using EventHub.Core.Constants;
using EventHub.Core.Contracts;
using EventHub.Core.Models.EventRegistration;
using EventHub.Infrastructure.Common;
using EventHub.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.Core.Services
{
    public class EventRegistrationService : IEventRegistrationService
    {
        private readonly IRepository repository;
        
        public EventRegistrationService(IRepository _repository)
        {
            repository = _repository;   
        }

        public async Task AddAsync(RegistrationFormModel model)
        {
            var registration = new EventRegistration()
            {
                EventId = model.EventId,
                UserId = model.UserId,
                RegisteredAt = DateTime.Now,
            };

            try
            {
                await repository.AddAsync(registration);
                await repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException(ErrorMessages.OperationFailedErrorMessage);
            }
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var registration = await repository.GetByIdAsync<EventRegistration>(id);

            if (registration == null || registration.UserId != userId)
                throw new ArgumentException(ErrorMessages.OperationFailedErrorMessage);

            await repository.DeleteAsync<EventRegistration>(id);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<RegistrationInfoModel>> GetUserEventRegistrationsAsync(string userId)
        {
            return await repository.AllReadonly<EventRegistration>()
                .Where(x=>x.UserId == userId)
                .Include(x=>x.Event)
                .Select(x=>new RegistrationInfoModel() 
                {
                    Id = x.Id,
                    EventId = x.EventId,
                    UserId = userId,
                    EventTitle = x.Event.Title
                }).ToListAsync();   
        }
    }
}
