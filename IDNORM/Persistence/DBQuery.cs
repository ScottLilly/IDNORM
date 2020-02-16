using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IDNORM.Persistence
{
    internal class DBQuery
    {
        private readonly List<QueryParameter> _queryParameters;

        public string QueryString { get; }
        public ReadOnlyCollection<QueryParameter> Parameters => _queryParameters.AsReadOnly();

        public DBQuery(string queryString)
        {
            _queryParameters = new List<QueryParameter>();

            QueryString = queryString;
        }

        public void AddParameter(string name, object value)
        {
            _queryParameters.Add(new QueryParameter(name, value));
        }
    }
}