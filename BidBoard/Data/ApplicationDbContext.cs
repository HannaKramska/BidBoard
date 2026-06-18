using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BidBoard.Models;

namespace BidBoard.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<Lot> Lots { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // обязательно — настраивает таблицы Identity

        // --- Lot ---
        builder.Entity<Lot>(e =>
        {
            // Индекс по статусу — часто фильтруем Active лоты
            e.HasIndex(l => l.Status);

            // Индекс по дате окончания — нужен AuctionCloseService
            // чтобы быстро найти лоты, которые пора закрыть
            e.HasIndex(l => l.EndsAt);

            e.HasOne(l => l.Seller)
             .WithMany(u => u.Lots)
             .HasForeignKey(l => l.SellerId)
             .OnDelete(DeleteBehavior.Restrict); // не удалять лоты при удалении юзера

            e.HasOne(l => l.Category)
             .WithMany(c => c.Lots)
             .HasForeignKey(l => l.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // --- Bid ---
        builder.Entity<Bid>(e =>
        {
            // Составной индекс: часто запрашиваем ставки конкретного лота,
            // отсортированные по сумме
            e.HasIndex(b => new { b.LotId, b.Amount });

            e.HasOne(b => b.Lot)
             .WithMany(l => l.Bids)
             .HasForeignKey(b => b.LotId)
             .OnDelete(DeleteBehavior.Cascade); // лот удалён — ставки тоже

            e.HasOne(b => b.Bidder)
             .WithMany(u => u.Bids)
             .HasForeignKey(b => b.BidderId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // --- Seed: базовые категории ---
        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Антиквариат", Slug = "antiques" },
            new Category { Id = 2, Name = "Искусство", Slug = "art" },
            new Category { Id = 3, Name = "Украшения", Slug = "jewelry" },
            new Category { Id = 4, Name = "Монеты и марки", Slug = "coins-stamps" },
            new Category { Id = 5, Name = "Разное", Slug = "other" }
        );
    } 
}
