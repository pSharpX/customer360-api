using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Infrastructure
{
    public class StoredProcedure
    {
        public StoredProcedure()
        {
            this.IsOutput = false;

        }

        public object Value { get; set; }

        public bool IsOutput { get; set; }
    }
}
