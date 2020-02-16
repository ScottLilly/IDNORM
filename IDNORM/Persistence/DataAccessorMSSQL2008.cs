namespace IDNORM.Persistence
{
    internal class DataAccessorMSSQL2008 : DataAccessorStandardSQL
    {
        internal DataAccessorMSSQL2008(string connectionString) : base(connectionString)
        {
        }

        internal static string BuildConnectionString(string serverName, string databaseName, string userID,
                                                     string password)
        {
            return
                $"Data Source={serverName};Database={databaseName};User ID={userID};Pwd={password};Integrated Security=false";
        }
    }
}