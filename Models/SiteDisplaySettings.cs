namespace Night.Models;

public class SiteDisplaySettings
{
    public int Id { get; set; }

    public bool ShowFullMembers { get; set; } = true;

    public bool ShowSubscribedMembers { get; set; } = true;

    public bool ShowUnsubscribedMembers { get; set; } = true;

    public bool ShowCollectiveGames { get; set; } = true;

    public bool ShowExternalGames { get; set; } = true;
}
