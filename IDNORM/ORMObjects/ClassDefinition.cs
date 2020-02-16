using System;
using System.Collections.Generic;
using System.Reflection;

namespace IDNORM.ORMObjects
{
    internal class ClassDefinition
    {
        public Type DataType { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public List<PropertyInfo> Properties { get; private set; }

        public ClassDefinition(Type dataType, string fullName, string name)
        {
            DataType = dataType;
            FullName = fullName;
            Name = name;

            Properties = new List<PropertyInfo>();
        }
    }
}
