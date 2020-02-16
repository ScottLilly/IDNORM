using System.Collections.Generic;

using Domain.Models;

namespace Domain.Interfaces.Views
{
    public interface ICustomerFilter
    {
        string SearchCriteria { get; }
        List<Customer> MatchingCustomers { set; }
    }
}
