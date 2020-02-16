using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace IDNORM.Persistence
{
    internal class SQLCommandManager
    {
        private const int COMMAND_TIMEOUT_IN_SECONDS = 300;

        private readonly string _connectionString;

        internal SQLCommandManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        internal void ExecuteNonQuery(DBQuery query)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = ConvertToSqlCommand(query))
                {
                    command.Connection = connection;
                    command.CommandTimeout = COMMAND_TIMEOUT_IN_SECONDS;
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        internal object ExecuteScalar(DBQuery query)
        {
            object result;

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = ConvertToSqlCommand(query))
                {
                    command.Connection = connection;
                    command.CommandTimeout = COMMAND_TIMEOUT_IN_SECONDS;
                    connection.Open();

                    result = command.ExecuteScalar();
                }
            }

            return result;
        }

        internal DataSet ExecuteQuery(DBQuery query)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = ConvertToSqlCommand(query))
                {
                    command.Connection = connection;
                    command.CommandTimeout = COMMAND_TIMEOUT_IN_SECONDS;

                    DataSet results = new DataSet();
                    results.Locale = CultureInfo.CurrentCulture;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(results);

                    return results;
                }
            }
        }

        private static SqlCommand ConvertToSqlCommand(DBQuery query)
        {
            SqlCommand command = new SqlCommand(query.QueryString);
            command.CommandType = CommandType.Text;

            foreach(QueryParameter queryParameter in query.Parameters)
            {
                command.Parameters.Add(new SqlParameter(queryParameter.Name.EnsurePrefixOf("@"), queryParameter.Value));
            }

            return command;
        }
    }
}