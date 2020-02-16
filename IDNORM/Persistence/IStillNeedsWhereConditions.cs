using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNORM.Persistence
{
    public interface IStillNeedsWhereConditions : ICanAddWhereConditions
    {
        Persistence Query();
    }
}
