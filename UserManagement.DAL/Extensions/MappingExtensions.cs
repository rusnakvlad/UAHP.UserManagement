using UserManagement.DAL.Models;

namespace UserManagement.DAL.Extensions;
public static class MappingExtensions
{
    public static PaginatedList<TDestination> PaginateList<TDestination>(this IEnumerable<TDestination> enumerable, int pageNumber, int pageSize)
        => PaginatedList<TDestination>.Create(enumerable, pageNumber, pageSize);

    public static Task<PaginatedList<TDestination>> PaginateListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
}
