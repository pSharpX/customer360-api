using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Reflection;

namespace Gildemeister.Cliente360.Common
{
    public static class PaginationList
    {
        public static async Task<Pagination<T>> GetPaginationAsync<T>(IQueryable<T> query, string orderBy, bool orderByDesc, int page, int pageSize) where T : class
        {
            Pagination<T> pagination = new Pagination<T>
            {
                TotalItems = query.Count(),
                PageSize = pageSize,
                CurrentPage = page,
                OrderBy = orderBy,
                OrderByDesc = false
            };

            int skip = (page - 1) * pageSize;
            var props = typeof(T).GetProperties();
            //var orderByProperty=  props.FirstOrDefault(x=>x.GetCustomAttributes<SortableAttribute>().OrderBy== orderBy);
            ////props.FirstOrDefault(n => n.GetCustomAttribute<SortableAttribute>()?.OrderBy == orderBy);
            //if (orderByProperty == null)
            //{
            //    throw new Exception($"Field: '{orderBy}' is not sortable");
            //}

            //if (orderByDesc)
            //{
            //    pagination.Result = await query
            //        .OrderByDescending(x => orderByProperty.GetValue(x))
            //        .Skip(skip)
            //        .Take(pageSize)
            //        .ToListAsync();

            //    return pagination;
            //}

            pagination.Result = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return pagination;
        }

        public static Pagination<T> GetPagination<T>(IQueryable<T> query, int page, int pageSize) where T : class
        {
            Pagination<T> pagination = new Pagination<T>
            {
                TotalItems = query.Count(),
                PageSize = pageSize,
                CurrentPage = page,
                OrderBy = string.Empty,
                OrderByDesc = false
            };

            int skip = (page - 1) * pageSize;
            var props = typeof(T).GetProperties();
           

            pagination.Result =  query
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return pagination;
        }

    }
}
