
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ISO810_ERP.Extensions;

public static class QueryableExtensions
{
    /* See https://stackoverflow.com/a/307600/9307869 */
    /// <summary>
    /// Dynamically order a queryable collection using the specified property name.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="orderBy"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string orderBy)
    {
        var type = typeof(T);
        var property = type.GetProperty(orderBy);

        if (property == null)
        {
            throw new ArgumentException("Property not found", nameof(orderBy));
        }

        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);
        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
        return source.Provider.CreateQuery<T>(resultExp);
    }

        public static IQueryable<T> OrderByDescendingProperty<T>(this IQueryable<T> source, string orderBy)
    {
        var type = typeof(T);
        var property = type.GetProperty(orderBy);

        if (property == null)
        {
            throw new ArgumentException("Property not found", nameof(orderBy));
        }

        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);
        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
        return source.Provider.CreateQuery<T>(resultExp);
    }
}