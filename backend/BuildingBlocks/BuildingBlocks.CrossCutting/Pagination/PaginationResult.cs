namespace BuildingBlocks.CrossCutting.Pagination;

public class PaginationResult<TEntity>(int pageIndex, int pageSize, long totalCount, IEnumerable<TEntity> items)
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public long TotalCount { get; } = totalCount;
    public IEnumerable<TEntity> Items { get; } = items;
}
