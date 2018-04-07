using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Infrastructure
{
    public class SortableAttribute : Attribute
    {
        public string OrderBy { get; set; }
    }
}
