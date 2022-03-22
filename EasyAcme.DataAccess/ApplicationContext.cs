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

        var order = builder.Entity<AcmeOrder>();
        order.HasOne(a => a.AcmeAccount)
            .WithMany(e => e.AcmeOrders)
            .HasForeignKey(e => e.AcmeAccountId);
        order.Property(a => a.AuthorizationChallengeType)
            .HasConversion<string>();
        order.HasOne(a => a.Country)
            .WithMany()
            .HasForeignKey(a => a.CountryCode);

        builder.Entity<Country>()
            .HasKey(c => c.Code);

        base.OnModelCreating(builder);
    }

    public DbSet<AcmeAccount> AcmeAccounts { get; set; } = null!;
    public DbSet<AcmeAccountEmail> AcmeAccountEmails { get; set; } = null!;
    public DbSet<AcmeOrder> AcmeOrders { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
}