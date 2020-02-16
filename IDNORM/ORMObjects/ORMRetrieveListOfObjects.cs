using System.Collections.Generic;

namespace IDNORM.ORMObjects
{
    public class ORMRetrieveListOfObjects : ORMBaseQuery
    {
        public ORMRetrieveListOfObjects Where(string columnName, Comparators comparator, object value)
        {
            FilterConditions.Add(new Condition(columnName, comparator, value));

            return this;
        }

        public List<T> From<T>()
        {
            SetType(typeof(T));

            return ORMEngine.PerformMultipleObjectRetrieval<T>(this);
        }
    }
}