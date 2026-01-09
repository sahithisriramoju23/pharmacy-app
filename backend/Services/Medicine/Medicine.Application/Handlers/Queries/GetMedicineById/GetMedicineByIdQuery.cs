using BuildingBlocks.CrossCutting.CQRS;
using FluentValidation;

namespace Medicine.Application.Handlers.Queries.GetMedicineById;

public record GetMedicineByIdQuery(Guid Id) : IQuery<GetMedicineByIdResult>;
public record GetMedicineByIdResult(Guid Id, string Name, DateOnly ExpiryDate, int Quantity, decimal Price, string Brand, string? Notes);

public class GetMedicineByIdQueryValidator : AbstractValidator<GetMedicineByIdQuery>
{
    public GetMedicineByIdQueryValidator()
    {
        RuleFor(query => query.Id).NotEmpty().WithMessage("Id is required.");
    }
}