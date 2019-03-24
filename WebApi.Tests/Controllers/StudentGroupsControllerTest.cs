using System;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using WebApi.Services;

namespace WebApi.Tests.Controllers
{
    [TestClass]
    public class StudentGroupsControllerTest
    {
        [TestMethod]
        public void PostRequestSingleGroup()
        {
            // Arrange
            Logger logger = new Logger();
            StudentGroupsController controller = new StudentGroupsController(logger);
            String[,] input = new String[,] { { "", "Paul", "" }, { "Fred", "", "" }, { "", "John", "" } };
            String[,] expected = new string[,] { { "Paul", "Fred", "John" } };

            // Act
            var output = controller.Post(input);

            // Assert
            CollectionAssert.AreEqual(expected, output);
        }

        [TestMethod]
        public void PostRequestMultipleGroups()
        {
            // Arrange
            Logger logger = new Logger();
            StudentGroupsController controller = new StudentGroupsController(logger);
            String[,] input = new String[,] { { "", "Paul", "" }, { "Fred", "", "" }, { "", "", "John" } };
            String[,] expected = new string[,] { { "Paul", "Fred" }, { "John", "" } };

            // Act
            var output = controller.Post(input);

            // Assert
            CollectionAssert.AreEqual(expected, output);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostRequestBadRequest()
        {
            // Arrange
            Logger logger = new Logger();
            StudentGroupsController controller = new StudentGroupsController(logger);
            String[,] input = null;

            // Act
            var output = controller.Post(input);

            // Assert
            Assert.IsInstanceOfType(output, typeof(BadRequestResult));
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostRequestEmptyRequest()
        {
            // Arrange
            Logger logger = new Logger();
            StudentGroupsController controller = new StudentGroupsController(logger);
            String[,] input = new String[,] { { } };

            // Act
            var output = controller.Post(input);

            // Assert
            Assert.IsInstanceOfType(output, typeof(BadRequestResult));
        }
    }
}
