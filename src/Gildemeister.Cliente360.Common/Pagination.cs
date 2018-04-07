using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Common
{
    public class Pagination<T>
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public string OrderBy { get; set; }

        public bool OrderByDesc { get; set; }

        public List<T> Result { get; set; }
    }
}
