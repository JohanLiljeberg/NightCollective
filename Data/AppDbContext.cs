using Microsoft.EntityFrameworkCore;
using Night.Models;

namespace Night.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CollectiveProject> CollectiveProjects => Set<CollectiveProject>();

    public DbSet<CollectiveEvent> CollectiveEvents => Set<CollectiveEvent>();

    public DbSet<Developer> Developers => Set<Developer>();

    public DbSet<CollectiveMember> CollectiveMembers => Set<CollectiveMember>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CollectiveProject>(entity =>
        {
            entity.Property(project => project.Title).HasMaxLength(120).IsRequired();
            entity.Property(project => project.Creator).HasMaxLength(120).IsRequired();
            entity.Property(project => project.Description).HasMaxLength(600).IsRequired();
            entity.Property(project => project.Medium).HasMaxLength(80).IsRequired();

            entity.HasData(
                new CollectiveProject
                {
                    Id = 1,
                    Title = "Dream Cartographers",
                    Creator = "Mira Sol",
                    Medium = "Interactive poem",
                    Description = "A tiny exploration game about mapping memories, procedural stars, and unfinished conversations."
                },
                new CollectiveProject
                {
                    Id = 2,
                    Title = "Arcade Reliquary",
                    Creator = "APT Studio",
                    Medium = "Playable installation",
                    Description = "A cabinet-scale exhibition that treats high scores, rituals, and glitches as community folklore."
                },
                new CollectiveProject
                {
                    Id = 3,
                    Title = "Soft Boss Rush",
                    Creator = "Jun Vale",
                    Medium = "Experimental action game",
                    Description = "A non-violent boss rush where every encounter is resolved through rhythm, dialogue, and care."
                });
        });

        modelBuilder.Entity<CollectiveEvent>(entity =>
        {
            entity.Property(collectiveEvent => collectiveEvent.Title).HasMaxLength(120).IsRequired();
            entity.Property(collectiveEvent => collectiveEvent.DateLabel).HasMaxLength(80).IsRequired();
            entity.Property(collectiveEvent => collectiveEvent.Description).HasMaxLength(600).IsRequired();
            entity.Property(collectiveEvent => collectiveEvent.Location).HasMaxLength(160).IsRequired();

            entity.HasData(
                new CollectiveEvent
                {
                    Id = 1,
                    Title = "Monthly Gamejam",
                    DateLabel = "Friday",
                    Location = "Online + local pop-up",
                    Description = "A monthly gamejam that anyone can join. New promt everytime!"
                },
                new CollectiveEvent
                {
                    Id = 2,
                    Title = "Games as Art Showcase",
                    DateLabel = "Summer 2026",
                    Location = "Community gallery",
                    Description = "A curated evening celebrating independent game creation, installations, talks, and live demos."
                });
        });

        modelBuilder.Entity<Developer>(entity =>
        {
            entity.Property(developer => developer.Name).HasMaxLength(120).IsRequired();
            entity.Property(developer => developer.Role).HasMaxLength(160).IsRequired();
            entity.Property(developer => developer.Bio).HasMaxLength(600).IsRequired();

            entity.HasData(new Developer
            {
                Id = 1,
                Name = "Night Collective",
                Role = " Creator",
                Bio = "Games are art."
            });
        });

        modelBuilder.Entity<CollectiveMember>(entity =>
        {
            entity.Property(member => member.Name).HasMaxLength(120).IsRequired();
            entity.Property(member => member.Image).HasMaxLength(240).IsRequired();
            entity.Property(member => member.Position).HasMaxLength(160).IsRequired();
            entity.Property(member => member.Quote).HasMaxLength(600).IsRequired();

            entity.HasMany(member => member.Games).WithOne();

            entity.HasData(new CollectiveMember
            {
                Id = 1,
                Name = "Night Collective",
                Image = "/images/collective-members/night-collective.jpg",
                Position = "Curator",
                Quote = "We champion small teams, expressive play, accessible tools, and games that belong in galleries as much as living rooms."
            });
        });
    }
}
