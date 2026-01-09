using BuildingBlocks.CrossCutting.CQRS;
using FluentValidation;
using Medicine.Application.Dtos;


namespace Medicine.Application.Handlers.Commands.UpdateMedicine;

public record UpdateMedicineCommand(Guid Id, string Name, DateOnly ExpiryDate, int Quantity, decimal Price, string Brand, string? Notes) : ICommand<UpdateMedicineResult>;
public record UpdateMedicineResult(Guid Id, string Name, DateOnly ExpiryDate, int Quantity, decimal Price, string Brand, string? Notes);

public class UpdateMedicineCommandValidator : AbstractValidator<UpdateMedicineCommand>
{
    public UpdateMedicineCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(command => command.ExpiryDate).GreaterThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Expiry date must be in the future.");
        RuleFor(command => command.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        RuleFor(command => command.Price).GreaterThan(0)
            .PrecisionScale(18, 2, true)
            .WithMessage("Price must be greater than zero.");
        RuleFor(command => command.Brand).NotEmpty().WithMessage("Brand is required.");
    }
}