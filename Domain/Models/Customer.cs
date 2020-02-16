using System;

using IDNORM.Attributes;

namespace Domain.Models
{
    [TableName("Customer")]
    public class Customer
    {
        public const string _ID = "ID";
        public const string _NAME = "Name";
        public const string _DATE_CREATED = "DateCreated";

        [ID]
        [ColumnName(_ID)]
        public Guid ID { get; set; }

        [ColumnName(_NAME)]
        public string Name { get; set; }
        
        [ColumnName(_DATE_CREATED)]
        public DateTime DateCreated { get; set; }
    }
}
