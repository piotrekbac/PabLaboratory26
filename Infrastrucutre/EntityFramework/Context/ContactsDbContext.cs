using AppCore.Models;
using AppCore.Models.Enums;
using Infrastructure.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// Piotr Bacior - WSEI Kraków

namespace Infrastructure.EntityFramework.Context
{
    public class ContactsDbContext : IdentityDbContext<CrmUser, CrmRole, string>
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // W docelowej wersji ścieżka powinna być pobierana z appsettings.json
            optionsBuilder.UseSqlite("Data Source=crm_database.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // --- Konfiguracja Identity ---
            builder.Entity<CrmUser>(entity =>
            {
                entity.Property(u => u.FirstName).HasMaxLength(100);
                entity.Property(u => u.LastName).HasMaxLength(100);
                entity.Property(u => u.Department).HasMaxLength(100);
                entity.HasIndex(u => u.Email).IsUnique();
            });
            
            builder.Entity<Tag>(entity =>
            {
                entity.HasKey(t => t.Id); // Jawne wskazanie klucza głównego
                entity.Property(t => t.Name).HasMaxLength(50);
            });

            builder.Entity<CrmRole>(entity =>
            {
                entity.Property(r => r.Name).HasMaxLength(20);
            });

            // --- Konfiguracja dziedziczenia TPH (One table) ---
            builder.Entity<Contact>()
                .HasDiscriminator<string>("ContactType")
                .HasValue<Person>("Person")
                .HasValue<Company>("Company")
                .HasValue<Organization>("Organization");

            builder.Entity<Contact>(entity =>
            {
                entity.Property(p => p.Email).HasMaxLength(200);
                entity.Property(p => p.Phone).HasMaxLength(20);
            });

            builder.Entity<Person>(entity =>
            {
                entity.Property(p => p.BirthDate).HasColumnType("date");
                entity.Property(p => p.Gender).HasConversion<string>();
                entity.Property(p => p.Status).HasConversion<string>();
            });

            // --- Definicje związków (Relacje) ---
            builder.Entity<Person>()
                .HasOne(p => p.Employer)
                .WithMany(e => e.Employees);

            builder.Entity<Organization>()
                .HasMany(o => o.Members)
                .WithOne(p => p.Organization);

            // --- Adres jako typ osadzony (Owned Type) ---
            builder.Entity<Contact>()
                .OwnsOne(c => c.Address);

            // --- Seedowanie danych (Przykładowe dane) ---
            builder.Entity<Company>().HasData(
                new Company()
                {
                    Id = Guid.Parse("516A34D7-CCFB-4F20-85F3-62BD0F3AF271"),
                    Name = "WSEI",
                    Industry = "edukacja",
                    Phone = "123567123",
                    Email = "biuro@wsei.edu.pl",
                    Website = "https://wsei.edu.pl"
                }
            );

            builder.Entity<Person>().HasData(
                new {
                    Id = Guid.Parse("3d54091d-abc8-49ec-9590-93ad3ed5458f"),
                    FirstName = "Adam",
                    LastName = "Nowak",
                    Gender = Gender.Male,
                    Status = ContactStatus.Activate,
                    Email = "adam@wsei.edu.pl",
                    Phone = "123456789",
                    BirthDate = DateTime.Parse("2001-01-11"),
                    Position = "Programista",
                    CreatedAt = new DateTime(2026, 01, 01, 10, 0, 0), 
                    UpdatedAt = new DateTime(2026, 01, 01, 10, 0, 0),
                    ContactType = "Person" // Wymagane przez TPH
                }
            );
        }
    }
}