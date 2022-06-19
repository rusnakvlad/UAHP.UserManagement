using System.Reflection;

namespace UserManagement.DAL.Extensions;

public static class QueriableExtensions
{
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> src, string? orderBy = null)
    {
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            PropertyInfo prop = typeof(T).GetProperty(orderBy);
            src = src.OrderBy(x => prop.GetValue(x, null));
        }

        return src;
    }
}
