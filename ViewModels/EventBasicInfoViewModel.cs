namespace Night.ViewModels;

public class EventBasicInfoViewModel
{
    public int Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public DateTime Date { get; init; }

    public string DateDisplay => Date.ToString("MMM d");
}
