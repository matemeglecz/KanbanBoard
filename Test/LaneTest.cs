using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using KanbanBoardApi.Controllers;
using KanbanBoardApi.Dal;
using KanbanBoardApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using MockQueryable.Moq;


namespace KanbanBoardApi.Test
{
    [TestClass]
    public class LanesTest
    {
        [TestMethod]
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

        private static (Mock<KanbanBoardContext> mockContext, Mock<DbSet<Lane>> mockDbSet) MockDbSetup()
        {
            var data = new List<Lane>()
            {
                new Lane
                {
                    ID = 123,
                    Title = "TEST Lane 1",
                    Order = 0
                },
                new Lane
                {
                    ID = 456,
                    Title = "TEST Lane 2",
                    Order = 1
                },

            }.AsQueryable();

            var options = new DbContextOptionsBuilder<KanbanBoardContext>()
                .UseInMemoryDatabase(databaseName: "Lanes Test")
                .Options;



            var mockSet = data.BuildMockDbSet();

            var mockContext = new Mock<KanbanBoardContext>(options);

            mockContext.Setup(m => m.Lanes).Returns(mockSet.Object);

            return (mockContext, mockSet);
        }

        [TestMethod]
        public async Task TestRepoDeleteExistingLane()
        {
            var mockDb = MockDbSetup();
            var lr = new LaneRepository(mockDb.mockContext.Object);

            //123 exists
            var result = await lr.DeleteLane(123).ConfigureAwait(true);

            Assert.IsTrue(result);
            mockDb.mockDbSet.Verify(m => m.Remove(It.IsAny<Lane>()), Times.Once());
        }

        [TestMethod]
        public async Task TestRepoDeleteNonExistingLane()
        {
            var mockDb = MockDbSetup();
            var lr = new LaneRepository(mockDb.mockContext.Object);

            //789 does not exists
            var result = await lr.DeleteLane(789).ConfigureAwait(true);

            Assert.IsFalse(result);
            mockDb.mockDbSet.Verify(m => m.Remove(It.IsAny<Lane>()), Times.Never());
        }
    }
}
