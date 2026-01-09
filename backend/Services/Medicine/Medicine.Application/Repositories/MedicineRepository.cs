
using Medicine.Application.Data;
using Medicine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Application.Repositories;

public class MedicineRepository(ICosmosDbContext _dbContext) : IMedicineRepository
{
    public async Task AddMedicine(Domain.Entities.Medicine medicine, CancellationToken cancellationToken)
    {
        _dbContext.Medicines.Add(medicine);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteMedicine(Guid id, CancellationToken cancellationToken)
    {
        var medicine = await _dbContext.Medicines
            .FindAsync(MedicineId.FromGuid(id));
        if (medicine == null)
            throw new ArgumentException("Medicine not found");
        _dbContext.Medicines.Remove(medicine);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Domain.Entities.Medicine>> GetAllMedicines(CancellationToken cancellationToken)
    {
        return await _dbContext.Medicines.ToListAsync(cancellationToken);
    }

    public async Task<Domain.Entities.Medicine> GetMedicine(Guid id, CancellationToken cancellationToken)
    {
        var medicine = await _dbContext.Medicines
            .FindAsync(MedicineId.FromGuid(id));

        if (medicine == null)
            throw new ArgumentException("Medicine not found");
        return medicine;
    }

    public async Task<IEnumerable<Domain.Entities.Medicine>> GetAllMedicinesByBrand(string brand, CancellationToken cancellationToken)
    {
        return await _dbContext.Medicines
            .Where(m => m.Brand == brand)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateMedicine(Domain.Entities.Medicine medicine, CancellationToken cancellationToken)
    {
        _dbContext.Medicines.Update(medicine);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
