using System.Linq.Expressions;
using eStoreCA.Shared.Enums;

namespace eStoreCA.Shared.Extensions;

public static class LinqExtensions
{
    public static IQueryable<T> OrderByDynamic<T>(
        this IQueryable<T> query,
        string orderByMember,
        AppEnums.DataOrderDirection ascendingDirection)
    {
        var param = Expression.Parameter(typeof(T), "c");

        var body = orderByMember.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

        var queryable = ascendingDirection == AppEnums.DataOrderDirection.Asc
            ? (IOrderedQueryable<T>)Queryable.OrderBy(query.AsQueryable(), (dynamic)Expression.Lambda(body, param))
            : (IOrderedQueryable<T>)Queryable.OrderByDescending(query.AsQueryable(),
                (dynamic)Expression.Lambda(body, param));

        return queryable;
    }

    public static IQueryable<T> WhereDynamic<T>(
        this IQueryable<T> sourceList, string query)
    {
        if (string.IsNullOrEmpty(query)) return sourceList;

        try
        {
            var properties = typeof(T).GetProperties()
                .Where(x => x.CanRead && x.CanWrite && !x.GetGetMethod().IsVirtual);

            //Expression
            sourceList = sourceList.Where(c =>
                properties.Any(p => p.GetValue(c) != null && p.GetValue(c).ToString()
                    .Contains(query, StringComparison.InvariantCultureIgnoreCase)));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return sourceList;
    }


    public static Expression<Func<T, bool>> GetContainsExpression<T>(string propertyName, string containsValue)
    {
        var parameterExp = Expression.Parameter(typeof(T), "type");
        var propertyExp = Expression.Property(parameterExp, propertyName);
        var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        var someValue = Expression.Constant(containsValue, typeof(string));
        var containsMethodExp = Expression.Call(propertyExp, method, someValue);

        return Expression.Lambda<Func<T, bool>>(containsMethodExp, parameterExp);
    }

    public static Expression<Func<T, TKey>> GetPropertyExpression<T, TKey>(string propertyName)
    {
        var parameterExp = Expression.Parameter(typeof(T), "type");
        var exp = Expression.Property(parameterExp, propertyName);
        return Expression.Lambda<Func<T, TKey>>(exp, parameterExp);
    }
}