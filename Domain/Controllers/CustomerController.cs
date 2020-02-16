using System;

using Domain.Interfaces.Views;
using Domain.Models;

using IDNORM;

namespace Domain.Controllers
{
    public static class CustomerController
    {
        static CustomerController()
        {
            ORMEngine.SetDataSource(DbType.MSSQL2008, "(local)", "IDNORM", "IDNORMUser", "IDNORMPassword");
        }

        public static void CreateCustomer(ICustomerCreator view)
        {
            ORMEngine.Insert()
                .Set(Customer._ID, Guid.NewGuid())
                .Set(Customer._NAME, view.CustomerName)
                .Set(Customer._DATE_CREATED, DateTime.Now)
                .Into<Customer>();

            // Clear UI controls
            view.CustomerName = "";
        }

        public static void UpdateCustomer(ICustomerUpdater view)
        {
            ORMEngine.Update()
                .Set(Customer._NAME, view.CustomerName)
                .Where(Customer._ID, Comparators.EqualTo, view.ID)
                .In<Customer>();

            // Clear UI controls
            view.CustomerName = "";
        }

        public static void RetrieveCustomerByUniqueID(ICustomerRetriever view)
        {
            Customer customer = ORMEngine.RetrieveSingleObject()
                .Where(Customer._ID, Comparators.EqualTo, view.ID)
                .From<Customer>();

            // Set UI controls
            view.CustomerName = customer.Name;
            view.DateCreated = customer.DateCreated;
        }

        public static void RetrieveCustomersBySearchCriteria(ICustomerFilter view)
        {
            view.MatchingCustomers = ORMEngine.RetrieveListOfObjects()
                .Where(Customer._NAME, Comparators.Contains, view.SearchCriteria)
                .Where(Customer._DATE_CREATED, Comparators.LessThanOrEqualTo, DateTime.Now)
                .From<Customer>();
        }

        public static void DeleteCustomersByExactNameMatch(ICustomerFilter view)
        {
            ORMEngine.Delete()
                .Where(Customer._NAME, Comparators.EqualTo, view.SearchCriteria)
                .From<Customer>();
        }
    }
}