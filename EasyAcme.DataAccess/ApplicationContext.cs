using EasyAcme.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyAcme.DataAccess;

public class ApplicationContext : IdentityDbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AcmeAccountEmail>()
            .HasOne(a => a.AcmeAccount)
            .WithMany(e => e.AccountEmails)
            .HasForeignKey(e => e.AcmeAccountId);

        builder.Entity<AcmeAccount>()
            .Property(a => a.EabKeyAlgorithm)
            .HasConversion<string?>();

        builder.Entity<AcmeOrder>()
            .HasOne(a => a.AcmeAccount)
            .WithMany(e => e.AcmeOrders)
            .HasForeignKey(e => e.AcmeAccountId);
        builder.Entity<AcmeOrder>()
            .Property(a => a.AuthorizationChallengeType)
            .HasConversion<string>();

        base.OnModelCreating(builder);
    }

    public DbSet<AcmeAccount> AcmeAccounts { get; set; } = null!;
    public DbSet<AcmeAccountEmail> AcmeAccountEmails { get; set; } = null!;
    public DbSet<AcmeOrder> AcmeOrders { get; set; } = null!;
}