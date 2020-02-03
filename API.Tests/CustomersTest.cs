using Domain.Entities;
using Domain.Models;
using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace API.Tests
{
    [TestClass]
    public class CustomersTest
    {
        private const string ApiEndpoint = "https://smithaapi.azurewebsites.net/api";
        [TestMethod]
        public void CreateCustomerTest()
        {
            var client = new ApiService(ApiEndpoint);
            var model = new CustomerModel
            {
                FirstName = "Test",
                MiddleName = "Test",
                LastName = "Test",
                Email = $"unittest{DateTime.Now.Ticks}@Test.com",
                CreatedAt = DateTime.Now
            };
            var customer = client.Post("/Customers/", model);
            Assert.AreEqual(model.FirstName, customer.FirstName);
            Assert.IsTrue(customer.ID > 0);
        }
    }
}
