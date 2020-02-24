using System;
using System.Collections.Generic;
using System.Text;

namespace AlexApp.Application.Dto
{
    public class PageInfo<T>
    {
        public const int minValuePage = 1;
        public const int minValuePageSize = 1;
        public const int maxValuePageSize = 500;

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int ItemsPerPage { get; private set; }
        public IEnumerable<T> Result { get; private set; }

        private PageInfo() { }

        [Obsolete]
        public void SetItems(int totalItems, IEnumerable<T> result)
        {
            TotalItems = totalItems;
            Result = result;
        }

        [Obsolete]
        public static PageInfo<T> CreateNew(int page, int pageSize)
        {
            var pageInfo = new PageInfo<T>
            {
                CurrentPage = (page < minValuePage) ? minValuePage : page,
                ItemsPerPage = (pageSize > maxValuePageSize || pageSize < minValuePageSize) ? maxValuePageSize : pageSize,
                TotalItems = 0,
                Result = new List<T>()
            };
            return pageInfo;
        }

        public static PageInfo<T> Create(int page, int pageSize, IEnumerable<T> items, int count)
        {
            return new PageInfo<T>
            {
                CurrentPage = (page < minValuePage) ? minValuePage : page,
                ItemsPerPage = (pageSize > maxValuePageSize || pageSize < minValuePageSize) ? maxValuePageSize : pageSize,
                TotalItems = count,
                Result = items
            };
        }
    }
}
