using Microsoft.AspNetCore.Mvc;
using Night.Services;

namespace Night.Controllers;

public class EventsController(IEventService eventService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var events = await eventService.GetUpcomingEventsAsync();

        return View(events);
    }

    public async Task<IActionResult> Details(int id)
    {
        var collectiveEvent = await eventService.GetEventAsync(id);

        return collectiveEvent is null ? NotFound() : View(collectiveEvent);
    }
}
