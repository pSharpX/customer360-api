using System;
using System.Collections.Generic;
using System.Text;

namespace Gildemeister.Cliente360.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ColumnAttribute: Attribute
    {
        public string PropertyName { get; set; }
        public Type ObjectType { get; set; }

        public ColumnAttribute(string propertyName, Type objectType)
        {
            PropertyName = propertyName;
            ObjectType = objectType;
        }

        public ColumnAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
