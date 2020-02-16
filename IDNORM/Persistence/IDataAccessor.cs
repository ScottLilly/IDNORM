using System.Data;
using IDNORM.ORMObjects;

namespace IDNORM.Persistence
{
    internal interface IDataAccessor
    {
        void Insert(ORMInsert query);
        void Update(ORMUpdate query);
        DataSet Retrieve<T>(ORMBaseQuery query);
        void Delete<T>(ORMDelete query);
    }
}