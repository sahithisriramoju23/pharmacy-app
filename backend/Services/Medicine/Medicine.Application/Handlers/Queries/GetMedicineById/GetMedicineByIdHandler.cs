using BuildingBlocks.CrossCutting.CQRS;
using BuildingBlocks.CrossCutting.Exceptions;
using Medicine.Application.Data;
using Medicine.Domain.ValueObjects;

namespace Medicine.Application.Handlers.Queries.GetMedicineById;

public class GetMedicineByIdHandler(ICosmosDbContext dbContext) : IQueryHandler<GetMedicineByIdQuery, GetMedicineByIdResult>
{
    public async Task<GetMedicineByIdResult> Handle(GetMedicineByIdQuery query, CancellationToken cancellationToken = default)
    {
        var medicine = await dbContext.Medicines.FindAsync(MedicineId.FromGuid(query.Id));
        
        if(medicine == null)
            throw new NotFoundException($"Medicine with id {query.Id} not found");

        return new GetMedicineByIdResult(medicine.Id.Value, medicine.Name, medicine.ExpiryDate, medicine.Quantity, medicine.Price, medicine.Brand, medicine.Notes);
    }
}
