using BuildingBlocks.CrossCutting.CQRS;
using Carter;
using Mapster;
using Medicine.Application.Handlers.Commands.CreateMedicine;
using Microsoft.AspNetCore.Mvc;

namespace Medicine.API.Endpoints;
public record CreateMedicineRequest(string Name, DateOnly ExpiryDate, int Quantity, decimal Price, string Brand);
public record CreateMedicineResponse(Guid Id);
public class CreateMedicineEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/medicine/create", async (CreateMedicineRequest request, [FromServices] IDispatcher dispatcher, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<CreateMedicineCommand>();
            var result = await dispatcher.Send<CreateMedicineCommand,CreateMedicineResult>(command, cancellationToken);
            var response = result.Adapt<CreateMedicineResponse>();
            return Results.Created($"/api/medicine/{response.Id}", response);
        })
         .WithName("CreateMedicine")
         .Produces<CreateMedicineResponse>(StatusCodes.Status201Created)
         .Produces(StatusCodes.Status400BadRequest)
         .Produces(StatusCodes.Status500InternalServerError)
         .WithOpenApi();
    }
}
