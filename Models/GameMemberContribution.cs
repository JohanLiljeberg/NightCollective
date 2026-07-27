namespace Night.Models;

public class GameMemberContribution
{
    public int GameId { get; set; }
    public Game Game { get; set; } = null!;

    public int CollectiveMemberId { get; set; }
    public CollectiveMember CollectiveMember { get; set; } = null!;

    public InvolvementLevel InvolvementLevel { get; set; } = InvolvementLevel.Supporting;

    public List<WorkArea> WorkAreas { get; set; } = new();
}

public enum InvolvementLevel
{
    Supporting,
    Major,
    Lead
}

[Flags]
public enum WorkArea
{
    None = 0,
    GameDesign = 1 << 0,
    LevelDesign = 1 << 1,
    SystemsDesign = 1 << 2,
    UXDesign = 1 << 3,
    UIDesign = 1 << 4,
    Art3D = 1 << 5,
    Texturing = 1 << 6,
    Animation = 1 << 7,
    Programming = 1 << 8,
    AI = 1 << 9,
    VFX = 1 << 10,
    TechnicalArt = 1 << 11
}
