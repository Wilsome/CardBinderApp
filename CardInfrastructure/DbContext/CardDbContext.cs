    using Microsoft.EntityFrameworkCore;
using CardLibrary.Models;
using CardInfrastructure.Models;

public class CardDbContext(DbContextOptions<CardDbContext> options) : DbContext(options)
{

    // Define database tables (DbSets)
    public DbSet<Card> Cards { get; set; }
    public DbSet<CardBinder> Binders { get; set; }
    public DbSet<CardGrading> Gradings { get; set; }
    public DbSet<CardImage> Images { get; set; }
    public DbSet<CardPriceHistory> PriceHistories { get; set; }
    public DbSet<CardValueTracker> ValueTrackers { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<MagicTheGathering> MagicCards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardBase>().UseTptMappingStrategy(); // ✅ Enables TPT

        modelBuilder.Entity<MagicTheGathering>().ToTable("MagicTheGatheringCards"); // ✅ Creates a separate table

        modelBuilder.Entity<CardBase>()
        .Property(c => c.Value)
        .HasPrecision(18, 2); // ✅ Set precision and scale

        modelBuilder.Entity<CardBinder>()
            .Property(c => c.EstimatedValue)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CardPriceHistory>()
            .Property(c => c.Value)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CardValueTracker>()
            .Property(c => c.MaxPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CardValueTracker>()
            .Property(c => c.MinPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CardValueTracker>()
            .Property(c => c.PercentageChange)
            .HasPrecision(18, 4); // ✅ Allow more precision for percentage

        modelBuilder.Entity<Collection>()
            .Property(c => c.EstimatedValue)
            .HasPrecision(18, 2);
    }
}