using BuildingBlocks.CrossCutting.CQRS;
using BuildingBlocks.CrossCutting.Exceptions;
using Medicine.Application.Data;
using Medicine.Application.Extensions;
using Medicine.Domain.ValueObjects;

namespace Medicine.Application.Handlers.Commands.UpdateMedicine;

public class UpdateMedicineHandler(ICosmosDbContext dbContext) : ICommandHandler<UpdateMedicineCommand, UpdateMedicineResult>
{
    public async Task<UpdateMedicineResult> Handle(UpdateMedicineCommand command, CancellationToken cancellationToken = default)
    {
        var medicineExits = await dbContext.Medicines.FindAsync([MedicineId.FromGuid(command.Id)], cancellationToken);
        if(medicineExits is null)
            throw new NotFoundException($"Medicine with id {command.Id} not found");
        
        var medicine = Domain.Entities.Medicine.Update(MedicineId.FromGuid(command.Id), command.Name, command.ExpiryDate, command.Quantity, command.Price, command.Brand, command.Notes);    
        dbContext.Medicines.Update(medicine);
        await dbContext.SaveChangesAsync(cancellationToken);
        var medicineDto = MedicineExtensions.ToMedicineDto(medicine.Id.Value, medicine.Name, medicine.ExpiryDate, medicine.Brand, medicine.Price, medicine.Quantity, medicine.Notes);

        return new UpdateMedicineResult(medicine.Id.Value, medicine.Name, medicine.ExpiryDate, medicine.Quantity, medicine.Price, medicine.Brand, medicine.Notes);
    }
}
