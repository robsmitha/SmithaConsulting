using Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace API.Tests
{
    [TestClass]
    public class BaseTests
    {
        protected const string ApiEndpoint = "https://smithaapi.azurewebsites.net/api";
        protected const string ApiKey = "IqjpsXh8yMZuMZ2wHH1DASw9omfKYnLa";
        protected readonly ApiService Client;
        public BaseTests()
        {
            if(Client == null)
            {
                Client = new ApiService(ApiEndpoint, ApiKey);
            }
        }
        protected void CheckAuthHeader(string authHeader)
        {
            string expectedHeader = $"Bearer {ApiKey}";
            Assert.AreEqual(authHeader, expectedHeader);
        }
    }
}
