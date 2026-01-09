using BuildingBlocks.CrossCutting.CQRS;
using BuildingBlocks.CrossCutting.Pagination;
using Medicine.Application.Dtos;

namespace Medicine.Application.Handlers.Queries.GetAllMedicines;

public record GetAllMedicinesQuery(PaginationRequest PaginationRequest) : IQuery<GetAllMedicinesResult>;
public record GetAllMedicinesResult(PaginationResult<MedicineDto> Data);

