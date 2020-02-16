namespace IDNORM.ORMObjects
{
    public class Condition
    {
        internal string Column { get; }
        public Comparators Comparator { get; }
        internal object Value { get; }

        public Condition(string column, Comparators comparator, object value)
        {
            Column = column;
            Comparator = comparator;
            Value = value;
        }
    }
}
