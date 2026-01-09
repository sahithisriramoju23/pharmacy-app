using BuildingBlocks.CrossCutting.CQRS;
using Medicine.Application.Data;

namespace Medicine.Application.Handlers.Commands.CreateMedicine;

public class CreateMedicineHandler(ICosmosDbContext dbContext) : ICommandHandler<CreateMedicineCommand, CreateMedicineResult>
{
    public async Task<CreateMedicineResult> Handle(CreateMedicineCommand command, CancellationToken cancellationToken = default)
    {
       var medicine = Domain.Entities.Medicine.Create(command.Name, command.ExpiryDate, command.Quantity, command.Price, command.Brand);
       dbContext.Medicines.Add(medicine);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateMedicineResult(medicine.Id.Value);
    }
}
