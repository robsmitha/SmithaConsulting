using Domain.Entities;
using Domain.Models;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace API.Tests
{
    [TestClass]
    public class CustomersTests : BaseTests
    {
        [TestMethod]
        public void CreateCustomerTest()
        {
            var guid = Guid.NewGuid();
            var model = new CustomerModel
            {
                FirstName = $"Customer{guid}",
                MiddleName = $"Customer{guid}",
                LastName = $"Customer{guid}",
                Email = $"Customer{guid}@unittest.com",
                CreatedAt = DateTime.Now
            };
            var customer = Client.Post("/Customers/", model);
            Assert.IsNotNull(customer.FirstName);
            Assert.IsNotNull(customer.MiddleName);
            Assert.IsNotNull(customer.LastName);
            Assert.IsNotNull(customer.Email);
            Assert.IsNotNull(customer.CreatedAt);
            Assert.AreEqual(model.Email, customer.Email);
            Assert.IsTrue(customer.ID > 0);

        }
    }
}
