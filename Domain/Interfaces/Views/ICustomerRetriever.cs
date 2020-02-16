using System;

namespace Domain.Interfaces.Views
{
    public interface ICustomerRetriever
    {
        Guid ID { get; set; }
        string CustomerName { get; set; }
        DateTime DateCreated { get; set; }
    }
}
