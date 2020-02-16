using System;

namespace Domain.Interfaces.Views
{
    public interface ICustomerCreator
    {
        Guid ID { get; set; }
        string CustomerName { get; set; }
    }
}
