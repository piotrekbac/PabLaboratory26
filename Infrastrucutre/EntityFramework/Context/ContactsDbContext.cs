using AppCore.Models;
using AppCore.Models.Enums;
using Infrastructure.EntityFramework.Entities;
using Infrastructure.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// Piotr Bacior - WSEI Kraków

namespace Infrastructure.EntityFramework.Context;

public class ContactsDbContext : IdentityDbContext<CrmUser, CrmRole, string>
{
    // Zestaw danych reprezentujących tabele w bazie
    public DbSet<Person> People { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // --- Konfiguracja dziedziczenia TPH ---
        builder.Entity<Contact>()
            .HasDiscriminator<string>("ContactType")
            .HasValue<Person>("Person")
            .HasValue<Company>("Company")
            .HasValue<Organization>("Organization");

        // --- Konfiguracja bazowej encji Contact ---
        builder.Entity<Contact>(entity =>
        {
            entity.Property(p => p.Email).HasMaxLength(200);
            entity.Property(p => p.Phone).HasMaxLength(20);
            entity.OwnsOne(c => c.Address);
        });

        // --- Konfiguracja encji Person ---
        builder.Entity<Person>(entity =>
        {
            entity.Property(p => p.BirthDate).HasColumnType("date");
            // Rzutowanie enumów na string
            entity.Property(p => p.Gender).HasConversion<string>();
            entity.Property(p => p.Status).HasConversion<string>();
        });
        
        // --- Relacje ---
        builder.Entity<Person>()
            .HasOne(p => p.Employer)
            .WithMany(c => c.Employees)
            .HasForeignKey(p => p.EmployerId)
            .IsRequired(false);

        builder.Entity<Person>()
            .HasOne(p => p.Organization)
            .WithMany(o => o.Members)
            .HasForeignKey(p => p.OrganizationId)
            .IsRequired(false);
            
        builder.Entity<Tag>(entity => { entity.HasKey(t => t.Id); });
        
        // Konfiguracja RefreshTokena
        builder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.HasIndex(t => t.Token).IsUnique();
        });
        
        // --- Seedowanie danych początkowych ---
        // Używamy silnego typowania, aby uniknąć błędów niekompatybilności enumów
        builder.Entity<Company>().HasData(
            new 
            {
                Id = Guid.Parse("516A34D7-CCFB-4F20-85F3-62BD0F3AF271"),
                Name = "WSEI",
                Industry = "edukacja",
                Phone = "123567123",
                Email = "biuro@wsei.edu.pl",
                Website = "https://wsei.edu.pl",
                CreatedAt = new DateTime(2026, 01, 01),
                ContactType = "Company",
                Status = ContactStatus.Activate     // Używamy Enuma bezpośrednio
            }
        );
        
    }
}