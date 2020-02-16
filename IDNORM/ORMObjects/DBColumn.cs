namespace IDNORM.ORMObjects
{
    public class DBColumn
    {
        public string Name { get; }
        public object Value { get; }

        public DBColumn(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
