using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using IDNORM.Attributes;
using IDNORM.ORMObjects;
using IDNORM.Persistence;

namespace IDNORM
{
    public static class ORMEngine
    {
        private static IDataAccessor _dataAccessor;

        public static void SetDataSource(DbType dbType, string serverName, string databaseName, string userID,
                                         string password)
        {
            switch(dbType)
            {
                case DbType.MSSQL2008:
                    _dataAccessor =
                        new DataAccessorMSSQL2008(DataAccessorMSSQL2008.BuildConnectionString(serverName, databaseName,
                                                                                              userID, password));
                    break;
            }
        }

        public static ORMInsert Insert()
        {
            return new ORMInsert();
        }

        public static void Insert<T>(T obj)
        {
            ValidatePersistenceObject(obj);

            // Build the ORM query object, based on the passed object
            ORMInsert ormInsert = new ORMInsert();

            // Get the property values from the object, and the columns they should be inserted into.
            foreach(PropertyInfo property in obj.GetType().GetProperties()
                                                .Where(x => x.GetCustomAttributes(typeof(ColumnNameAttribute), true)
                                                             .Length == 1))
            {
                ormInsert.ColumnsAffected.Add(new
                                                  DBColumn(((ColumnNameAttribute)property.GetCustomAttributes(typeof(ColumnNameAttribute), true)[0]).Name,
                                                           property.GetValue(obj, null)));
            }

            PerformInsert(ormInsert);
        }

        public static ORMUpdate Update()
        {
            return new ORMUpdate();
        }

        public static void Update<T>(T obj)
        {
            ValidatePersistenceObject(obj);

            // Build an ORMQuery from the passed parameter
            ORMUpdate ormUpdate = new ORMUpdate(obj.GetType());

            foreach(PropertyInfo property in obj.GetType().GetProperties()
                                                .Where(x => x.GetCustomAttributes(typeof(ColumnNameAttribute), true)
                                                             .Length == 1))
            {
                string columnName =
                    ((ColumnNameAttribute)property.GetCustomAttributes(typeof(ColumnNameAttribute), true)[0]).Name;

                if(property.GetCustomAttributes(typeof(IDAttribute), true).Length == 1)
                {
                    // This is an ID Column, so add it to the search criteria
                    ormUpdate.FilterConditions.Add(new Condition(columnName, Comparators.EqualTo,
                                                                 property.GetValue(obj, null)));
                }
                else
                {
                    // This is not an ID column, so include it in the fields to be updated, if not null
                    if(property.GetValue(obj, null) != null)
                    {
                        ormUpdate.ColumnsAffected.Add(new DBColumn(columnName, property.GetValue(obj, null)));
                    }
                }
            }

            PerformUpdate(ormUpdate);
        }

        public static ORMRetrieveObject RetrieveSingleObject()
        {
            return new ORMRetrieveObject();
        }

        public static ORMRetrieveListOfObjects RetrieveListOfObjects()
        {
            return new ORMRetrieveListOfObjects();
        }

        public static ORMDelete Delete()
        {
            return new ORMDelete();
        }

        internal static void PerformInsert(ORMInsert query)
        {
            _dataAccessor.Insert(query);
        }

        internal static void PerformUpdate(ORMUpdate query)
        {
            _dataAccessor.Update(query);
        }

        internal static T PerformSingleObjectRetrieval<T>(ORMRetrieveObject ormQuery)
        {
            DataSet results = _dataAccessor.Retrieve<T>(ormQuery);

            if(results.Tables[0].Rows.Count > 1)
            {
                // Throw exception
            }

            var obj = Activator.CreateInstance(typeof(T));

            // Convert database row to business object
            foreach(PropertyInfo property in typeof(T)
                                             .GetProperties()
                                             .Where(x => x.GetCustomAttributes(typeof(ColumnNameAttribute), true)
                                                          .Length == 1))
            {
                string columnName =
                    ((ColumnNameAttribute)property.GetCustomAttributes(typeof(ColumnNameAttribute), true)[0]).Name;

                property.SetValue(obj, results.Tables[0].Rows[0][columnName], null);
            }

            return (T)obj;
        }

        internal static List<T> PerformMultipleObjectRetrieval<T>(ORMRetrieveListOfObjects ormQuery)
        {
            DataSet results = _dataAccessor.Retrieve<T>(ormQuery);

            List<T> objects = new List<T>();

            // Convert database rows to business objects
            foreach(DataRow row in results.Tables[0].Rows)
            {
                // Create an instance of the typed object
                var obj = Activator.CreateInstance(typeof(T));

                // Set the values of the properties, from the database row
                foreach(PropertyInfo property in typeof(T)
                                                 .GetProperties()
                                                 .Where(x => x.GetCustomAttributes(typeof(ColumnNameAttribute), true)
                                                              .Length == 1))
                {
                    string columnName =
                        ((ColumnNameAttribute)property.GetCustomAttributes(typeof(ColumnNameAttribute), true)[0]).Name;

                    property.SetValue(obj, row[columnName], null);
                }

                objects.Add((T)obj);
            }

            return objects;
        }

        internal static void PerformDelete<T>(ORMDelete ormQuery)
        {
            _dataAccessor.Delete<T>(ormQuery);
        }

        private static void ValidatePersistenceObject(object obj)
        {
            if(obj.GetType().GetCustomAttributes(typeof(TableNameAttribute), true).Length != 1)
            {
                // Raise exception
            }
        }
    }
}