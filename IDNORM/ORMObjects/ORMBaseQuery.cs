using System;
using System.Collections.Generic;

using IDNORM.Attributes;

namespace IDNORM.ORMObjects
{
    public abstract class ORMBaseQuery
    {
        internal string TableName { get; private set; }
        internal List<DBColumn> ColumnsAffected { get; }
        internal List<Condition> FilterConditions { get; }

        protected ORMBaseQuery()
        {
            ColumnsAffected = new List<DBColumn>();
            FilterConditions = new List<Condition>();
        }

        internal void SetType(Type type)
        {
            TableName = ((TableNameAttribute)Activator.CreateInstance(type).GetType().GetCustomAttributes(typeof(TableNameAttribute), true)[0]).Name;
        }
    }
}
