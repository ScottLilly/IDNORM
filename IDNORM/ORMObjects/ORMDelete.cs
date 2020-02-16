namespace IDNORM.ORMObjects
{
    public class ORMDelete : ORMBaseQuery
    {
        public ORMDelete Where(string columnName, Comparators comparator, object value)
        {
            FilterConditions.Add(new Condition(columnName, comparator, value));

            return this;
        }

        public void From<T>()
        {
            SetType(typeof(T));

            ORMEngine.PerformDelete<T>(this);
        }
    }
}