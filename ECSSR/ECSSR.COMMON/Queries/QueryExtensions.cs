using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ECSSR.COMMON.Queries
{
    public static class QueryExtensions
    {

        
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, EntitySort sort)
        {
            return sort == null ? query : Sort(query, new[] { sort });
        }

      
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, IEnumerable<EntitySort> sorts)
        {
            if (sorts == null || !sorts.Any())
                return query;

            // Create ordering expression e.g. Field1 asc, Field2 desc
            var builder = new StringBuilder();
            foreach (var sort in sorts)
            {
                if (builder.Length > 0)
                    builder.Append(",");
                builder.Append(sort.Name).Append(" ");
                var isDescending = !string.IsNullOrWhiteSpace(sort.Direction)
                    && sort.Direction.StartsWith(EntitySortDirections.Descending, StringComparison.OrdinalIgnoreCase);
                builder.Append(isDescending ? EntitySortDirections.Descending : EntitySortDirections.Ascending);
            }

            return query.OrderBy(builder.ToString());

        }

        
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, EntityFilter filter)
        {
            if (filter == null)
                return query;

            var builder = new LinqExpressionBuilder();
            builder.Build(filter);
            var predicate = builder.Expression;
            var parameters = builder.Parameters.ToArray();
            if (string.IsNullOrWhiteSpace(predicate))
                return query;
            return query.Where(predicate, parameters);
        }

       
        public static IQueryable<T> IncludeIf<T>(this IQueryable<T> source, bool condition, string path)
           where T : class
        {
            return condition
                ? source.Include(path)
                : source;
        }
        
        public static IQueryable<T> IncludeIf<T, TProperty>(this IQueryable<T> source, bool condition, Expression<Func<T, TProperty>> path)
          where T : class
        {
            return condition
                ? source.Include(path)
                : source;
        }
       
        public static IQueryable<T> IncludeIf<T>(
           this IQueryable<T> source,
           bool condition,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
           where T : class
        {
            return condition
                ? include(source)
                : source;
        }

        public static void AddRange<T>(this ICollection<T> destination,
                               IEnumerable<T> source)
        {
            List<T> list = destination as List<T>;

            if (list != null)
            {
                list.AddRange(source);
            }
            else
            {
                foreach (T item in source)
                {
                    destination.Add(item);
                }
            }
        }

    }
}

