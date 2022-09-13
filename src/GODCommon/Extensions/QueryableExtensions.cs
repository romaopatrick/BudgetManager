using System.Linq.Expressions;
using GODCommon.Results.Paging;
using Microsoft.EntityFrameworkCore;

namespace GODCommon.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> DynamicOrderBy<TEntity>(this IQueryable<TEntity> query, string sortColumn,
        bool descending)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "p");

        string command = "OrderBy";

        if (descending) command = "OrderByDescending";

        var property = typeof(TEntity).GetProperty(sortColumn);
        var propertyAccess = Expression.MakeMemberAccess(parameter, property!);

        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        var resultExpression = Expression.Call(typeof(Queryable), command,
            new [] { typeof(TEntity), property!.PropertyType },
            query.Expression, Expression.Quote(orderByExpression));

        return query.Provider.CreateQuery<TEntity>(resultExpression);
    } 
    
    public static async Task<Paged<TEntity>> Page<TEntity>(this IQueryable<TEntity> dbSet,
        long? skip, long? range, string? keyToOrder, bool? desc,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        long count = await dbSet.CountAsync(cancellationToken);

        if (skip >= count)
            skip = (long)(Math.Ceiling((double)count / range!.Value) * range - range);

        if (skip < 0) skip = 0;

        if (skip + range > count) range = count - skip;

        return new(await dbSet
            .DynamicOrderBy(FindPropertyName(keyToOrder!, typeof(TEntity)), desc!.Value)
            .Skip((int)skip!)
            .Take((int)range!)
            .ToArrayAsync(cancellationToken), skip.Value, range.Value, count);
    }
    private static string FindPropertyName(string property, Type type)
    {
        var properties = type.GetProperties();
        var defaultProperty = properties.FirstOrDefault()?.Name;
        if (string.IsNullOrWhiteSpace(defaultProperty))
            throw new NotImplementedException($"No property found to order by for type {type.Name}");

        return string.IsNullOrEmpty(property)
                ? defaultProperty
                : properties.FirstOrDefault(prop
                    => prop.Name.Equals(property, StringComparison.InvariantCultureIgnoreCase))?.Name 
                  ?? defaultProperty;
    }
}