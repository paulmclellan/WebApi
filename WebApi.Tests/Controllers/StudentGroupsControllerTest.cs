using System;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Controllers;
using WebApi.Services;
using WebApi.Services.Interfaces;

namespace WebApi.Tests.Controllers
{
    [TestClass]
    public class StudentGroupsControllerTest
    {
        [TestMethod]
        public void PostRequestSingleGroup()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var studentCore = new StudentCore(mockLogger.Object);
            var controller = new StudentGroupsController(mockLogger.Object, studentCore);

            var input = new String[,] { { "", "Paul", "" }, { "Fred", "", "" }, { "", "John", "" } };
            var expected = new string[,] { { "Paul", "Fred", "John" } };

            // Act
            var actual = controller.Post(input);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PostRequestMultipleGroups()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var studentCore = new StudentCore(mockLogger.Object);
            var controller = new StudentGroupsController(mockLogger.Object, studentCore);

            var input = new String[,] { { "", "Paul", "" }, { "Fred", "", "" }, { "", "", "John" } };
            var expected = new string[,] { { "Paul", "Fred" }, { "John", "" } };

            // Act
            var actual = controller.Post(input);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostRequestBadRequest()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            var studentCore = new StudentCore(mockLogger.Object);
            var controller = new StudentGroupsController(mockLogger.Object, studentCore);

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
            var mockLogger = new Mock<ILogger>();
            var studentCore = new StudentCore(mockLogger.Object);
            var controller = new StudentGroupsController(mockLogger.Object, studentCore);

            var input = new String[,] { { } };

            // Act
            var output = controller.Post(input);

            // Assert
            Assert.IsInstanceOfType(output, typeof(BadRequestResult));
        }
    }
}
