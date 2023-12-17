using Microsoft.EntityFrameworkCore;
using Ecocell.Domain.Entities.UserApp;

namespace Ecocell.Infrastructure.Context;

public class EcocellContext : DbContext
{
    public EcocellContext(DbContextOptions<EcocellContext> options): base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<LegalPerson> LegalPerson { get; set; }
    public DbSet<NaturalPerson> NaturalPerson { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcocellContext).Assembly);

        modelBuilder.Entity<LegalPerson>()
            .ToTable("LegalPersons")
            .HasBaseType<User>();

        modelBuilder.Entity<NaturalPerson>()
            .ToTable("NaturalPersons")
            .HasBaseType<User>();    

    }
}