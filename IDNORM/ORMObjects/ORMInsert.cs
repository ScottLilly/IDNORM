using System;

namespace IDNORM.ORMObjects
{
    public class ORMInsert : ORMBaseQuery
    {
        public ORMInsert()
        {
        }

        public ORMInsert(Type type)
        {
            SetType(type);
        }

        public ORMInsert Set(string columnName, object value)
        {
            ColumnsAffected.Add(new DBColumn(columnName, value));

            return this;
        }

        public void Into<T>()
        {
            SetType(typeof(T));

            ORMEngine.PerformInsert(this);
        }
    }
}