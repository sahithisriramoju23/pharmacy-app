using BuildingBlocks.CrossCutting.CQRS;
using Carter;
using Mapster;
using Medicine.Application.Handlers.Commands.DeleteMedicineById;
using Microsoft.AspNetCore.Mvc;

namespace Medicine.API.Endpoints;

public record DeleteMedicineByIdResponse(Guid Id, bool IsSuccess);
public class DeleteMedicineEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/medicine/delete/{id}", async (Guid id,[FromServices]ICommandHandler<DeleteMedicineByIdCommand, DeleteMedicineByIdResult> handler, CancellationToken cancellationToken) =>
        {
            var command = new DeleteMedicineByIdCommand(id);
            var result = await handler.Handle(command, cancellationToken);
            var response = result.Adapt<DeleteMedicineByIdResponse>();
            return Results.Ok(response);
        })
       .WithName("DeleteProduct")
       .Produces<DeleteMedicineByIdResponse>(StatusCodes.Status200OK)
       .Produces(StatusCodes.Status404NotFound)
       .Produces(StatusCodes.Status500InternalServerError)
       .WithOpenApi();
    }
}
