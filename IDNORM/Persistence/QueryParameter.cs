namespace IDNORM.Persistence
{
    internal class QueryParameter
    {
        public string Name { get; }
        public object Value { get; }

        public QueryParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}