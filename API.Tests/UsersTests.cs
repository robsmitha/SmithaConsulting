using Domain.Models;
using Domain.Services;
using Domain.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Tests
{
    [TestClass]
    public class UsersTests : BaseTests
    {
        [TestMethod]
        public void CreateNewUserTest()
        {
            var guid = Guid.NewGuid();
            var model = new UserModel
            {
                FirstName = $"User{guid}",
                MiddleName = $"User{guid}",
                LastName = $"User{guid}",
                Email = $"User{guid}@unittest.com",
                Username = $"Username{guid}",
                Password = SecurePasswordHasher.Hash("testpassword123!!"),
                MerchantName = $"Merchant{guid}"
            };
            var user = Client.Post("/Users/", model);
            Assert.IsNotNull(user.FirstName);
            Assert.IsNotNull(user.MiddleName);
            Assert.IsNotNull(user.LastName);
            Assert.IsNotNull(user.Email);
            Assert.IsNotNull(user.Username);
            Assert.IsNotNull(user.Password);
            Assert.AreEqual(model.Username, user.Username);
            Assert.IsTrue(user.ID > 0);
        }

        [TestMethod]
        public void CreateExistingUserTest()
        {
            var model = new UserModel
            {
                FirstName = "ExistingUserTest",
                MiddleName = "ExistingUserTest",
                LastName = "ExistingUserTest",
                Email = "ExistingUserTest@unittest.com",
                Username = "rob.smitha",
                Password = SecurePasswordHasher.Hash("testpassword123!!")
            };
            var existingUser = Client.Get<UserModel>($"/Users/GetByUsername/{model.Username}");
            Assert.IsTrue(existingUser.ID > 0);
        }
    }
}
