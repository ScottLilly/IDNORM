using IDNORM.Attributes;

namespace Domain.Models
{
    [TableName("Address")]
    public class Address
    {
        public const string _ADDRESS1 = "StreetAddress1";
        public const string _ADDRESS2 = "StreetAddress2";
        public const string _CITY = "City";
        public const string _STATE = "State";
        public const string _ZIPCODE = "ZIPCode";

        [ColumnName(_ADDRESS1)]
        public string StreetAddress1 { get; set; }

        [ColumnName(_ADDRESS2)]
        public string StreetAddress2 { get; set; }

        [ColumnName(_CITY)]
        public string City { get; set; }

        [ColumnName(_STATE)]
        public string State { get; set; }

        [ColumnName(_ZIPCODE)]
        public string ZIPCode { get; set; }
    }
}
