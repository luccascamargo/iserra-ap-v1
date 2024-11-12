using iserra_api.Enums;
using iserra_api.Models;
using Microsoft.EntityFrameworkCore;

namespace iserra_api.Data;

public class ApplicatonDbContext : DbContext
{
    public DbSet<Advert> Adverts { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<Optional> Optionals { get; set; }

    public DbSet<Photo> Photos { get; set; }

    public ApplicatonDbContext(DbContextOptions<ApplicatonDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Mapeia nosso ENUM para que use o seu proprio nome como valor
        modelBuilder.Entity<Advert>().Property(a => a.Condicao)
            .HasConversion(v => v.ToString(), v => (Condition)Enum.Parse(typeof(Condition), v));

        modelBuilder.Entity<User>().Property(a => a.Plano)
            .HasConversion(v => v.ToString(), v => (Plan)Enum.Parse(typeof(Plan), v));

        // Mapeia nosso ENUM para que use o seu proprio nome como valor
        modelBuilder.Entity<User>().Property(a => a.Plano)
            .HasConversion(v => v.ToString(), v => (Plan)Enum.Parse(typeof(Plan), v));

        modelBuilder.Entity<Photo>().HasOne(p => p.Anuncio)
            .WithMany(a => a.Imagens)
            .HasForeignKey(p => p.AnuncioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}