using System;

namespace IDNORM.ORMObjects
{
    public class ORMUpdate : ORMBaseQuery
    {
        public ORMUpdate()
        {
        }

        public ORMUpdate(Type type)
        {
            SetType(type);
        }

        public ORMUpdate Set(string columnName, object value)
        {
            ColumnsAffected.Add(new DBColumn(columnName, value));

            return this;
        }

        public ORMUpdate Where(string columnName, Comparators comparator, object value)
        {
            FilterConditions.Add(new Condition(columnName, comparator, value));

            return this;
        }

        public void In<T>()
        {
            SetType(typeof(T));

            ORMEngine.PerformUpdate(this);
        }
    }
}