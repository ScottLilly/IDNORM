using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Domain.Controllers;
using Domain.Interfaces.Views;
using Domain.Models;

namespace WorkBench
{
    public partial class Workbench : Form, ICustomerUpdater, ICustomerRetriever, ICustomerFilter
    {
        public Workbench()
        {
            InitializeComponent();
        }

        public string SearchCriteria => txtFilter.Text;

        public List<Customer> MatchingCustomers
        {
            set => dgResults.DataSource = value;
        }

        public DateTime DateCreated { get; set; }

        public Guid ID
        {
            get => new Guid(txtID.Text);
            set => txtID.Text = value.ToString();
        }

        public string CustomerName
        {
            get => txtCustomerName.Text;
            set => txtCustomerName.Text = value;
        }

        private void btnCreateCustomer_Click(object sender, EventArgs e)
        {
            CustomerController.CreateCustomer(this);
        }

        private void btnLoadCustomer_Click(object sender, EventArgs e)
        {
            CustomerController.RetrieveCustomerByUniqueID(this);
        }

        private void btnViewCustomers_Click(object sender, EventArgs e)
        {
            CustomerController.RetrieveCustomersBySearchCriteria(this);
        }

        private void btnDeleteCustomers_Click(object sender, EventArgs e)
        {
            CustomerController.DeleteCustomersByExactNameMatch(this);
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            CustomerController.UpdateCustomer(this);
        }
    }
}