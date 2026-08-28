namespace Night.Models;

public enum MembershipType
{
    /// <summary>
    /// Former member - shows only name and icon
    /// </summary>
    Unsubscribed = 0,

    /// <summary>
    /// Subscribed member - shows name, icon, and one linked game
    /// </summary>
    Subscribed = 1,

    /// <summary>
    /// Full collective member - shows complete profile with all games
    /// </summary>
    Full = 2
}
