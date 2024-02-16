#region

using LicenseServer.Database.Entities;

#endregion

namespace LicenseServer.Core.Services;

public class EventService
{
    private readonly EntityRepository<Event> _repository;

    public EventService(EntityRepository<Event> repository)
    {
        _repository = repository;
    }

    public async Task Create(Event @event)
    {
        await _repository.AddAsync(@event);
        await _repository.SaveChangesAsync();
    }
}
