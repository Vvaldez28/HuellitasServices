using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Security.Cryptography;

namespace Huellitas.Services.Administration
{
    public partial class AdministrationContext(DbContextOptions<AdministrationContext> options) : DbContext(options)
    {
       public DbSet<User> Users { get; set; } = null!;
       public DbSet<TransactionType> TransactionTypes { get; set; } = null!;
       public DbSet<PaymentType> PaymentTypes { get; set; } = null!;
       public DbSet<MoneyTransaction> MoneyTransactions { get; set; } = null!;
       public DbSet<CredentialType> CredentialTypes { get; set; } = null!;
       public DbSet<Credential> Credentials { get; set; } = null!;
       public DbSet<Pet>Pets { get; set; } = null!;
       public DbSet<PetSize> PetSizes { get; set; } = null!;
       public DbSet<PetStatus> PetStatuses { get; set; } = null!;
       public DbSet<Rol>Roles { get; set; } = null!;
       public DbSet<UserRol> UserRoles { get; set; } = null!;
         public DbSet<Event>Events { get; set; } = null!;
        //public DbSet<EventRegistration> EventRegistrations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Event>()
       .ToTable("Events");

            modelBuilder.Entity<Rol>()
          .ToTable("Roles");
            modelBuilder.Entity<User>()
               .ToTable("Users");

            modelBuilder.Entity<TransactionType>()
              .ToTable("TransactionTypes");

            modelBuilder.Entity<PaymentType>()
             .ToTable("PaymentTypes");

            modelBuilder.Entity<MoneyTransaction>()
         .ToTable("MoneyTransactions");

            modelBuilder.Entity<CredentialType>()
        .ToTable("CredentialTypes");




            modelBuilder.Entity<Credential>()
                .ToTable("Credentials")
                 .HasKey(e => new { e.UserId, e.CredentialTypeId });

            modelBuilder.Entity<UserRol>()
               .ToTable("UserRoles")
                .HasKey(e => new { e.UserId, e.RolId });


            modelBuilder.Entity<Pet>()
                .ToTable("Pets")
              .HasKey(e => new { e.PetId });
        }


    }
}
