using Microsoft.EntityFrameworkCore;


namespace Medicine.Application.Data;

public interface ICosmosDbContext 
{
    public DbSet<Domain.Entities.Medicine> Medicines { get;  }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
