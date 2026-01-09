using BuildingBlocks.CrossCutting.CQRS;
using BuildingBlocks.CrossCutting.Pagination;
using Carter;
using Mapster;
using Medicine.Application.Dtos;
using Medicine.Application.Handlers.Queries.GetAllMedicines;
using Microsoft.AspNetCore.Mvc;

namespace Medicine.API.Endpoints;

public record GetAllMedicinesResponse(PaginationResult<MedicineDto> Data);

public class GetAllMedicinesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/medicines", async ([AsParameters] PaginationRequest request,
            [FromServices]IQueryHandler<GetAllMedicinesQuery, GetAllMedicinesResult> handler, CancellationToken cancellationToken) =>
        {
            var result = await handler.Handle(new GetAllMedicinesQuery(request), cancellationToken);

            var response = result.Adapt<GetAllMedicinesResponse>();

            return Results.Ok(response);
        })
      .WithName("GetMedicines")
      .Produces<GetAllMedicinesResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .Produces(StatusCodes.Status500InternalServerError)
      .WithSummary("Get Medicines")
      .WithDescription("Retrieve a list of medicines available in the system.");
    }
}
