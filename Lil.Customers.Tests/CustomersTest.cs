using Lil.Customers.Controllers;
using Lil.Customers.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Lil.Customers.Tests
{
    [TestClass]
    public class CustomersTest
    {
        [TestMethod]
        public void GetAsyncReturnsOk()
        {
            var customersController = new CustomersController(new CustomersProvider());
            var result = customersController.GetAsync("1").Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetAsyncReturnsNotFound()
        {
            var customersController = new CustomersController(new CustomersProvider());
            var result = customersController.GetAsync("99").Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

    }
}