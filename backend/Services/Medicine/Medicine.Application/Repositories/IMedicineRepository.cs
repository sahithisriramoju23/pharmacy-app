using MedicineDomain = Medicine.Domain.Entities.Medicine;
namespace Medicine.Application.Repositories;

public interface IMedicineRepository
{
    Task AddMedicine(MedicineDomain medicine, CancellationToken cancellationToken);
    Task<MedicineDomain> GetMedicine(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MedicineDomain>> GetAllMedicines(CancellationToken cancellationToken);
    Task UpdateMedicine(MedicineDomain medicine, CancellationToken cancellationToken);
    Task DeleteMedicine(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MedicineDomain>> GetAllMedicinesByBrand(string brand, CancellationToken cancellationToken);
}
