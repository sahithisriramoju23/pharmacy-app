using BuildingBlocks.CrossCutting.CQRS;
using FluentValidation;

namespace Medicine.Application.Handlers.Commands.DeleteMedicineById;

public record DeleteMedicineByIdCommand(Guid Id): ICommand<DeleteMedicineByIdResult>;
public record DeleteMedicineByIdResult(Guid Id, bool IsSuccess);

public class DeleteMedicineByIdCommandValidator : AbstractValidator<DeleteMedicineByIdCommand>
{
    public DeleteMedicineByIdCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Id is required.");
    }
}
