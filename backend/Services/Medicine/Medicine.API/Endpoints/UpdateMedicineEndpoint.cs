using BuildingBlocks.CrossCutting.CQRS;
using Carter;
using Mapster;
using Medicine.Application.Handlers.Commands.UpdateMedicine;
using Microsoft.AspNetCore.Mvc;

namespace Medicine.API.Endpoints;

public record UpdateMedicineRequest(string Name, DateOnly ExpiryDate, int Quantity, decimal Price, string Brand,string? Notes);
public record UpdateMedicineResponse(Guid Id, string Name, DateOnly ExpiryDate, int Quantity, decimal Price, string Brand, string? Notes);
public class UpdateMedicineEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/medicine/update/{id}", async (Guid id, UpdateMedicineRequest request, [FromServices]ICommandHandler<UpdateMedicineCommand, UpdateMedicineResult> handler, CancellationToken cancellationToken) =>
        {
            var command = new UpdateMedicineCommand(id,request.Name,request.ExpiryDate,request.Quantity,request.Price,request.Brand,request.Notes);
            var result = await handler.Handle(command, cancellationToken);
            var response = result.Adapt<UpdateMedicineResponse>();
            return Results.Ok(result);
        })
      .WithName("UpdateMedicine")
      .Produces<UpdateMedicineResponse>(StatusCodes.Status200OK)
      .Produces(StatusCodes.Status404NotFound)
      .Produces(StatusCodes.Status400BadRequest)
      .Produces(StatusCodes.Status500InternalServerError)
      .WithOpenApi();
    }
}
