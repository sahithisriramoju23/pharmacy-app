using BuildingBlocks.CrossCutting.CQRS;
using BuildingBlocks.CrossCutting.Exceptions;
using Medicine.Application.Data;
using Medicine.Domain.ValueObjects;

namespace Medicine.Application.Handlers.Commands.DeleteMedicineById;

public class DeleteMedicineByIdHandler(ICosmosDbContext dbContext) : ICommandHandler<DeleteMedicineByIdCommand, DeleteMedicineByIdResult>
{
    public async Task<DeleteMedicineByIdResult> Handle(DeleteMedicineByIdCommand command, CancellationToken cancellationToken = default)
    {
        var medicine =  await dbContext.Medicines.FindAsync(MedicineId.FromGuid(command.Id));
        if(medicine is null)
            throw new NotFoundException($"Medicine with id {command.Id} not found");

        dbContext.Medicines.Remove(medicine);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteMedicineByIdResult(command.Id,true);
    }
}
