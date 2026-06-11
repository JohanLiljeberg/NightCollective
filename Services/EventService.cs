using Night.Models;
using Night.Repositories;
using Night.ViewModels;

namespace Night.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    public async Task<IReadOnlyCollection<EventViewModel>> GetUpcomingEventsAsync()
    {
        return (await eventRepository.GetUpcomingAsync(DateTime.Today)).Select(MapEvent).ToList();
    }

    public async Task<EventViewModel?> GetEventAsync(int id)
    {
        var collectiveEvent = await eventRepository.GetByIdAsync(id);

        return collectiveEvent is null ? null : MapEvent(collectiveEvent);
    }

    public Task<EventFormViewModel> GetCreateEventFormAsync()
    {
        return Task.FromResult(new EventFormViewModel { Date = DateTime.Today });
    }

    public async Task<EventFormViewModel?> GetEventForEditAsync(int id)
    {
        var collectiveEvent = await eventRepository.GetByIdAsync(id);

        return collectiveEvent is null ? null : MapForm(collectiveEvent);
    }

    public async Task CreateEventAsync(EventFormViewModel viewModel)
    {
        await eventRepository.AddAsync(MapEntity(viewModel));
    }

    public async Task UpdateEventAsync(EventFormViewModel viewModel)
    {
        await eventRepository.UpdateAsync(MapEntity(viewModel));
    }

    public async Task DeleteEventAsync(int id)
    {
        await eventRepository.DeleteAsync(id);
    }

    private static EventViewModel MapEvent(CollectiveEvent collectiveEvent)
    {
        return new EventViewModel
        {
            Id = collectiveEvent.Id,
            Title = collectiveEvent.Title,
            Date = collectiveEvent.Date,
            Location = collectiveEvent.Location,
            Description = collectiveEvent.Description,
            ImageUrl = collectiveEvent.ImageUrl
        };
    }

    private static EventFormViewModel MapForm(CollectiveEvent collectiveEvent)
    {
        return new EventFormViewModel
        {
            Id = collectiveEvent.Id,
            Title = collectiveEvent.Title,
            Date = collectiveEvent.Date,
            Location = collectiveEvent.Location,
            Description = collectiveEvent.Description,
            ImageUrl = collectiveEvent.ImageUrl
        };
    }

    private static CollectiveEvent MapEntity(EventFormViewModel viewModel)
    {
        return new CollectiveEvent
        {
            Id = viewModel.Id,
            Title = viewModel.Title,
            Date = viewModel.Date,
            Location = viewModel.Location,
            Description = viewModel.Description,
            ImageUrl = viewModel.ImageUrl
        };
    }
}
