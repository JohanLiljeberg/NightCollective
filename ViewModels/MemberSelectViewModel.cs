using Night.Models;

namespace Night.ViewModels;

public class MemberSelectViewModel
{
    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string? ImageUrl { get; init; }

    public MembershipType MembershipType { get; init; } = MembershipType.Full;
}
