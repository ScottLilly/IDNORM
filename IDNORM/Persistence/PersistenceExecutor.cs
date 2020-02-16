using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDNORM.ORM;

namespace IDNORM.Persistence
{
    public class PersistenceExecutor : ICanAddWhereConditions, IStillNeedsWhereConditions
    {
        private ConditionList _whereConditions = new ConditionList();

        public IStillNeedsWhereConditions Where(string column, Condition.Comparators comparator, object value)
        {
            _whereConditions.AddCondition(column, comparator, value);
            throw new NotImplementedException();
        }

        public Persistence Query()
        {
            throw new NotImplementedException();
        }
    }
}
