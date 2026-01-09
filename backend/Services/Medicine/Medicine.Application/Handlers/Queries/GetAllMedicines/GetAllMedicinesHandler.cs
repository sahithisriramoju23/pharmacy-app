using BuildingBlocks.CrossCutting.CQRS;
using BuildingBlocks.CrossCutting.Pagination;
using Medicine.Application.Data;
using Medicine.Application.Dtos;
using Medicine.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Application.Handlers.Queries.GetAllMedicines;

public class GetAllMedicinesHandler(ICosmosDbContext dbContext) : IQueryHandler<GetAllMedicinesQuery, GetAllMedicinesResult>
{
    public async Task<GetAllMedicinesResult> Handle(GetAllMedicinesQuery query, CancellationToken cancellationToken = default)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Medicines.LongCountAsync();

        var medicines = await dbContext.Medicines
            .OrderBy(x=>x.Id)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var medicinesDtos = medicines.Select(m => MedicineExtensions.ToMedicineDto(m.Id.Value, m.Name, m.ExpiryDate, m.Brand, m.Price, m.Quantity, m.Notes)).ToList();
        return new GetAllMedicinesResult(new PaginationResult<MedicineDto>
        (
           pageIndex, pageSize, totalCount, medicinesDtos
        ));
    }
}

