using Microsoft.AspNetCore.Mvc;
using Night.Services;
using Night.ViewModels;

namespace Night.Controllers;

public class EventsController(IEventService eventService, IImageService imageService) : Controller
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
        // Custom validation: at least one image source required (file or URL)
        bool hasFile = viewModel.ImageFile is not null && viewModel.ImageFile.Length > 0;
        bool hasUrl = !string.IsNullOrWhiteSpace(viewModel.ImageUrl?.Trim());

        if (!hasFile && !hasUrl)
        {
            ModelState.AddModelError("", "Please provide an image by uploading a file or entering a URL.");
        }

        if (!ModelState.IsValid)
        {
            // Log validation errors for debugging
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine($"Validation Error: {error.ErrorMessage}");
            }
            return View(viewModel);
        }

        try
        {
            await eventService.CreateEventAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating event: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            ModelState.AddModelError("", $"An error occurred while creating the event: {ex.Message}");
            return View(viewModel);
        }
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

        // Custom validation: at least one image source required (file or URL)
        bool hasFile = viewModel.ImageFile is not null && viewModel.ImageFile.Length > 0;
        bool hasUrl = !string.IsNullOrWhiteSpace(viewModel.ImageUrl?.Trim());

        if (!hasFile && !hasUrl)
        {
            ModelState.AddModelError("", "Please provide an image by uploading a file or entering a URL.");
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            await eventService.UpdateEventAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating event: {ex.Message}");
            ModelState.AddModelError("", $"An error occurred while updating the event: {ex.Message}");
            return View(viewModel);
        }
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
