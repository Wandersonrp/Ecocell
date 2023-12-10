using Microsoft.EntityFrameworkCore;
using Ecocell.Domain.Entities.UserApp;

namespace Ecocell.Infrastructure.Context;

public class EcocellContext : DbContext
{
    public EcocellContext(DbContextOptions<EcocellContext> options): base(options) {}

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcocellContext).Assembly);
    }
}