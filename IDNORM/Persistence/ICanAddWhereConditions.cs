using IDNORM.ORM;

namespace IDNORM.Persistence
{
    public interface ICanAddWhereConditions
    {
        IStillNeedsWhereConditions Where(string column, Condition.Comparators comparator, object value);
    }
}
