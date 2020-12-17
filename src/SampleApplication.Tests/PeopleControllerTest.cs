
using Moq;
using Xunit;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using SampleApplication.Repository;
using SampleApplication.API.Controllers;

namespace SampleApplication.Tests
{
    public class PeopleControllerTest
    {
        [Fact]
        public async Task GetById_ReturnsNotFoundResult_ForNonexistentUserId()
        {
            var mockRepo = new Mock<IPeopleRepository>();
            var mockLogger = new Mock<ILogger<PeopleController>>();
            var controller = new PeopleController(mockLogger.Object, mockRepo.Object);
            var nonExistentUserId = 12345;

            // Act
            var result = await controller.GetAsync(nonExistentUserId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
