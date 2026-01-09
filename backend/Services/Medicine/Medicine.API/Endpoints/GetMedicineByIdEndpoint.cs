using BuildingBlocks.CrossCutting.CQRS;
using Carter;
using Mapster;
using Medicine.Application.Handlers.Queries.GetMedicineById;
using Microsoft.AspNetCore.Mvc;

namespace Medicine.API.Endpoints;

public record GetMedicineByIdResponse(Guid Id, string Name, DateOnly ExpiryDate, int Quantity, decimal Price, string Brand);

public class GetMedicineByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/medicine/{id}", async (Guid id, [FromServices]IQueryHandler<GetMedicineByIdQuery, GetMedicineByIdResult> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetMedicineByIdQuery(id);
            var medicineResult = await handler.Handle(query, cancellationToken);
            var response = medicineResult.Adapt<GetMedicineByIdResponse>();
            return response != null ? Results.Ok(response) : Results.NotFound();
        })
        .WithName("GetMedicine")
        .Produces<GetMedicineByIdResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi();
    }
}
