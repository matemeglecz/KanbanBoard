using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoardApi.Controllers;
using KanbanBoardApi.Dal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KanbanBoardApi.Test
{
    [TestClass] // this class contains test code
    public class LanesTest
    {
        [TestMethod] // this is a test instance
        public async Task TestDeleteControllerNonExistingLane()
        {
            // Arrange

            // repository is mocked with a replacement object for this test as this test verifies the business logic and not the database/repository
            var laneRepositoryMock = new Mock<ILaneRepository>();
            // mock setup: calling GetLaneOrNull always returns null
            laneRepositoryMock.Setup(repo => repo.DeleteLane(It.IsAny<int>())).ReturnsAsync(false);

            //Act
            var lc = new LanesController(laneRepositoryMock.Object);
            var result = await lc.DeleteLane(123).ConfigureAwait(true);

            // Assert
            // calling delete must return NotFound
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }        

        [TestMethod]
        public async Task TestDeleteExistingLane()
        {

            int testId = 123;

            var laneRepositoryMock = new Mock<ILaneRepository>();

            laneRepositoryMock.Setup(repo => repo.DeleteLane(testId)).ReturnsAsync(true);

            var lc = new LanesController(laneRepositoryMock.Object);

            var result = await lc.DeleteLane(testId).ConfigureAwait(true);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }        

    }
}
