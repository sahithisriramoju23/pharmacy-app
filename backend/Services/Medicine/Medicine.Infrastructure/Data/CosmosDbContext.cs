using Medicine.Application.Data;
using Medicine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Infrastructure.Data;

public class CosmosDbContext: DbContext, ICosmosDbContext
{
    public DbSet<Domain.Entities.Medicine> Medicines => Set<Domain.Entities.Medicine>();
    public CosmosDbContext(DbContextOptions<CosmosDbContext> dbContextOptions): base(dbContextOptions)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAutoscaleThroughput(4000);
        modelBuilder.HasDefaultContainer("Medicines");
        modelBuilder.Entity<Domain.Entities.Medicine>()
            .HasNoDiscriminator()
            .ToContainer("Medicines")
            .HasPartitionKey(x => x.Brand)
            .HasKey(x=>x.Id);

        modelBuilder.Entity<Domain.Entities.Medicine>().
            Property(x => x.Id)
            .HasConversion(x => x.Value, dbValue => MedicineId.FromGuid(dbValue));

        modelBuilder.Entity<Domain.Entities.Medicine>()
            .Property(x => x.Price).IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}
