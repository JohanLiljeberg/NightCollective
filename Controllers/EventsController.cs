using Microsoft.AspNetCore.Mvc;
using Night.Services;
using Night.ViewModels;

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

    public async Task<IActionResult> Create()
    {
        var viewModel = await eventService.GetCreateEventFormAsync();

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EventFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await eventService.CreateEventAsync(viewModel);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var viewModel = await eventService.GetEventForEditAsync(id);

        return viewModel is null ? NotFound() : View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EventFormViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await eventService.UpdateEventAsync(viewModel);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var collectiveEvent = await eventService.GetEventAsync(id);

        return collectiveEvent is null ? NotFound() : View(collectiveEvent);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await eventService.DeleteEventAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
