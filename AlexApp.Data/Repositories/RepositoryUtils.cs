using AlexApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Linq.Dynamic;

namespace AlexApp.Data.Repositories
{
    static class RepositoryUtils
    {
        public static (IEnumerable<T> items, int totalItems) GetFilteredRange<T>(DbSet<T> dbSet, int page, int pageSize, Expression<Func<T, bool>> filter, List<Expression<Func<T, object>>> includes = null) where T : class, IEntityEF
        {
            var query = dbSet.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            int totalItems = query.Count();

            if (page > 1)
            {
                query = query.Skip((page - 1) * pageSize);
            }
            if (pageSize > 0)
            {
                query = query.Take(pageSize);
            }

            return (query.ToList(), totalItems);
        }

        public static IEnumerable<T> GetFilteredItems<T>(DbSet<T> dbSet, Expression<Func<T, bool>> filter, List<Expression<Func<T, object>>> includes = null) where T : class, IEntityEF
        {
            var query = dbSet.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = query.OrderBy(e => e.Id);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }
    }
}
