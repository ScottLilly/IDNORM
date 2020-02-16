namespace IDNORM.ORMObjects
{
    public class ORMRetrieveObject : ORMBaseQuery
    {
        public ORMRetrieveObject Where(string columnName, Comparators comparator, object value)
        {
            FilterConditions.Add(new Condition(columnName, comparator, value));

            return this;
        }

        public T From<T>()
        {
            SetType(typeof(T));

            return ORMEngine.PerformSingleObjectRetrieval<T>(this);
        }
    }
}