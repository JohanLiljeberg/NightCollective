using Night.ViewModels;

namespace Night.Services;

public interface IEventService
{
    Task<IReadOnlyCollection<EventViewModel>> GetUpcomingEventsAsync();

    Task<EventViewModel?> GetEventAsync(int id);

    Task<EventFormViewModel> GetCreateEventFormAsync();

    Task<EventFormViewModel?> GetEventForEditAsync(int id);

    Task CreateEventAsync(EventFormViewModel viewModel);

    Task UpdateEventAsync(EventFormViewModel viewModel);

    Task DeleteEventAsync(int id);
}
