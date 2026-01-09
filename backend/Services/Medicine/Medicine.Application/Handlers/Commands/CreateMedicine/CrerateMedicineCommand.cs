using BuildingBlocks.CrossCutting.CQRS;
using FluentValidation;

namespace Medicine.Application.Handlers.Commands.CreateMedicine;

public record CreateMedicineCommand(string Name, DateOnly ExpiryDate, int Quantity, decimal Price, string Brand, string? Notes) : ICommand<CreateMedicineResult>;

public record CreateMedicineResult(Guid Id);

public class CreateMedicineCommandValidator : AbstractValidator<CreateMedicineCommand>
{
    public CreateMedicineCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(command => command.ExpiryDate).GreaterThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Expiry date must be in the future.");
        RuleFor(command => command.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        RuleFor(command => command.Price).GreaterThan(0)
            .PrecisionScale(18, 2, true)
            .WithMessage("Price must be greater than zero.");
        RuleFor(command => command.Brand).NotEmpty().WithMessage("Brand is required.");
    }
}

