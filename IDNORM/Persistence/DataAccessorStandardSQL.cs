using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IDNORM.Attributes;
using IDNORM.ORMObjects;

namespace IDNORM.Persistence
{
    internal class DataAccessorStandardSQL : IDataAccessor
    {
        private const string COLUMN_SUFFIX = "_COL";
        private readonly SQLCommandManager _sqlCommandManager;

        internal DataAccessorStandardSQL(string connectionString)
        {
            _sqlCommandManager = new SQLCommandManager(connectionString);
        }

        public virtual void Insert(ORMInsert ormQuery)
        {
            string columns = "";
            string values = "";

            foreach(DBColumn column in ormQuery.ColumnsAffected)
            {
                if(column == ormQuery.ColumnsAffected.Last())
                {
                    columns += $"[{column.Name}]";
                    values += $"@{column.Name}{COLUMN_SUFFIX}";
                }
                else
                {
                    columns += $"[{column.Name}],";
                    values += $"@{column.Name}{COLUMN_SUFFIX},";
                }
            }

            string sql = $"INSERT INTO {ormQuery.TableName} ({columns}) VALUES ({values})";

            _sqlCommandManager.ExecuteNonQuery(CreateDBQuery(sql, ormQuery.ColumnsAffected, ormQuery.FilterConditions));
        }

        public virtual void Update(ORMUpdate ormQuery)
        {
            string sql = $"UPDATE [{ormQuery.TableName}] SET ";

            foreach(DBColumn dbColumn in ormQuery.ColumnsAffected)
            {
                if(dbColumn == ormQuery.ColumnsAffected.Last())
                {
                    sql += string.Format("[{0}] = @{0}{1}", dbColumn.Name, COLUMN_SUFFIX);
                }
                else
                {
                    sql += string.Format("[{0}] = @{0}{1},", dbColumn.Name, COLUMN_SUFFIX);
                }
            }

            _sqlCommandManager.ExecuteNonQuery(CreateDBQuery(sql, ormQuery.ColumnsAffected, ormQuery.FilterConditions));
        }

        public virtual DataSet Retrieve<T>(ORMBaseQuery ormQuery)
        {
            Type classType = typeof(T);

            if(classType.GetCustomAttributes(typeof(TableNameAttribute), true).Length != 1)
            {
                // Raise exception
            }

            // TODO: Raise exception if searchParameters contains columns that aren't in the class properties

            string sql = $"SELECT * FROM [{((TableNameAttribute)classType.GetCustomAttributes(typeof(TableNameAttribute), true)[0]).Name}]";

            DBQuery query = CreateDBReadOnlyQuery(sql, ormQuery.FilterConditions);

            return _sqlCommandManager.ExecuteQuery(query);
        }

        public virtual void Delete<T>(ORMDelete ormQuery)
        {
            Type classType = typeof(T);

            if(classType.GetCustomAttributes(typeof(TableNameAttribute), true).Length != 1)
            {
                // Raise exception
            }

            // TODO: Raise exception if searchParameters contains columns that aren't in the class properties

            string sql = $"DELETE FROM [{((TableNameAttribute)classType.GetCustomAttributes(typeof(TableNameAttribute), true)[0]).Name}]";

            DBQuery query = CreateDBReadOnlyQuery(sql, ormQuery.FilterConditions);

            _sqlCommandManager.ExecuteQuery(query);
        }

        #region Private methods

        private static DBQuery CreateDBReadOnlyQuery(string sql, List<Condition> whereConditions = null)
        {
            return CreateDBQuery(sql, null, whereConditions);
        }

        private static DBQuery CreateDBQuery(string sql, List<DBColumn> columns = null,
                                             List<Condition> whereConditions = null)
        {
            DBQuery query = new DBQuery(ConcatenateQueryStringConditions(sql, whereConditions));

            if(columns != null)
            {
                PopulateQueryColumnParameters(query, columns);
            }

            if(whereConditions != null)
            {
                PopulateQueryConditionParameters(query, whereConditions);
            }

            return query;
        }

        private static string ConcatenateQueryStringConditions(string sql, List<Condition> whereConditions)
        {
            string whereClause = "";

            if((whereConditions != null) && (whereConditions.Count > 0))
            {
                whereClause += " WHERE ";

                foreach(Condition condition in whereConditions)
                {
                    switch(condition.Comparator)
                    {
                        case Comparators.EqualTo:
                            whereClause += string.Format(" [{0}] = @{0}", condition.Column);
                            break;
                        case Comparators.NotEqualTo:
                            whereClause += string.Format(" [{0}] <> @{0}", condition.Column);
                            break;
                        case Comparators.LessThan:
                            whereClause += string.Format(" [{0}] < @{0}", condition.Column);
                            break;
                        case Comparators.LessThanOrEqualTo:
                            whereClause += string.Format(" [{0}] <= @{0}", condition.Column);
                            break;
                        case Comparators.GreaterThan:
                            whereClause += string.Format(" [{0}] > @{0}", condition.Column);
                            break;
                        case Comparators.GreaterThanOrEqualTo:
                            whereClause += string.Format(" [{0}] >= @{0}", condition.Column);
                            break;
                        case Comparators.Contains:
                            whereClause += string.Format(" [{0}] LIKE @{0}", condition.Column);
                            break;
                        case Comparators.StartsWith:
                            whereClause += string.Format(" [{0}] LIKE @{0}", condition.Column);
                            break;
                        case Comparators.EndsWith:
                            whereClause += string.Format(" [{0}] LIKE @{0}", condition.Column);
                            break;
                    }

                    if(condition != whereConditions.Last())
                    {
                        whereClause += " AND ";
                    }
                }
            }

            return sql + whereClause;
        }

        private static void PopulateQueryColumnParameters(DBQuery query, List<DBColumn> columns)
        {
            foreach(DBColumn column in columns)
            {
                query.AddParameter(column.Name.EnsurePrefixOf("@") + COLUMN_SUFFIX, column.Value ?? DBNull.Value);
            }
        }

        private static void PopulateQueryConditionParameters(DBQuery query, List<Condition> whereConditions)
        {
            foreach(Condition condition in whereConditions)
            {
                switch(condition.Comparator)
                {
                    case Comparators.Contains:
                        query.AddParameter(condition.Column.EnsurePrefixOf("@"),
                                           condition.Value.ToString().EnsurePrefixOf("%").EnsureSuffixOf("%"));
                        break;
                    case Comparators.StartsWith:
                        query.AddParameter(condition.Column.EnsurePrefixOf("@"),
                                           condition.Value.ToString().EnsureSuffixOf("%"));
                        break;
                    case Comparators.EndsWith:
                        query.AddParameter(condition.Column.EnsurePrefixOf("@"),
                                           condition.Value.ToString().EnsurePrefixOf("%"));
                        break;
                    default:
                        query.AddParameter(condition.Column.EnsurePrefixOf("@"), condition.Value ?? DBNull.Value);
                        break;
                }
            }
        }

        #endregion
    }
}