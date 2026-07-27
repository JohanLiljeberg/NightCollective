using Night.Models;

namespace Night.Extensions;

public static class WorkAreaExtensions
{
    private static readonly Dictionary<WorkArea, string> WorkAreaIcons = new()
    {
        { WorkArea.GameDesign, "🎮" },
        { WorkArea.LevelDesign, "🧩" },
        { WorkArea.SystemsDesign, "📈" },
        { WorkArea.UXDesign, "🎯" },
        { WorkArea.UIDesign, "🖥" },
        { WorkArea.Art3D, "🎨" },
        { WorkArea.Texturing, "🖌" },
        { WorkArea.Animation, "🎞" },
        { WorkArea.Programming, "💻" },
        { WorkArea.AI, "🤖" },
        { WorkArea.VFX, "✨" },
        { WorkArea.TechnicalArt, "🛠" }
    };

    private static readonly Dictionary<WorkArea, string> WorkAreaNames = new()
    {
        { WorkArea.GameDesign, "Game Design" },
        { WorkArea.LevelDesign, "Level Design" },
        { WorkArea.SystemsDesign, "Systems Design" },
        { WorkArea.UXDesign, "UX Design" },
        { WorkArea.UIDesign, "UI Design" },
        { WorkArea.Art3D, "3D Art" },
        { WorkArea.Texturing, "Texturing" },
        { WorkArea.Animation, "Animation" },
        { WorkArea.Programming, "Programming" },
        { WorkArea.AI, "AI" },
        { WorkArea.VFX, "VFX" },
        { WorkArea.TechnicalArt, "Technical Art" }
    };

    public static string GetIcon(this WorkArea workArea)
    {
        return WorkAreaIcons.TryGetValue(workArea, out var icon) ? icon : "";
    }

    public static string GetName(this WorkArea workArea)
    {
        return WorkAreaNames.TryGetValue(workArea, out var name) ? name : workArea.ToString();
    }

    public static string GetIconWithName(this WorkArea workArea)
    {
        return $"{workArea.GetIcon()} {workArea.GetName()}";
    }

    public static IEnumerable<WorkArea> GetSelectedWorkAreas(this List<WorkArea> workAreas)
    {
        return workAreas.Where(wa => wa != WorkArea.None);
    }

    public static List<WorkArea> GetAllWorkAreas()
    {
        return Enum.GetValues<WorkArea>()
            .Where(wa => wa != WorkArea.None)
            .ToList();
    }
}
