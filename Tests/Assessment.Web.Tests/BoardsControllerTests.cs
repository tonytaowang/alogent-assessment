using System;
using Assessment.Web.Controllers;
using Assessment.Web.Models;
using Moq;
using NUnit.Framework;

namespace Assessment.Web.Tests
{
    [TestFixture]
    class BoardsControllerTests
    {
        [Test]
        public void Constructor_CreatesController()
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);
            Assert.NotNull(controller);
        }

        [Test]
        public void GetAll_DoesLookupThroughRepository()
        {
            var boardRepo = new Mock<IBoardRepository>();
            var controller = new BoardsController(boardRepo.Object);

            controller.GetAll();

            boardRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void Find_NegativeId_ThrowsOutOfRangeException()
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                controller.Find(-1);
            });
        }

        [Test]
        public void Find_ZeroId_ThrowsOutOfRangeException()
        {
            var boardRepo = Mock.Of<IBoardRepository>();
            var controller = new BoardsController(boardRepo);
            Assert.Throws<ArgumentOutOfRangeException>(() => 
            {
                controller.Find(0);
            });
        }

        [Test]
        public void Find_ValidId_DoesLookupThroughRepository()
        {
            var boardRepo = new Mock<IBoardRepository>();
            boardRepo.Setup(x => x.Find(It.IsAny<int>())).Returns(new Board());

            var controller = new BoardsController(boardRepo.Object);

            controller.Find(1);

            boardRepo.Verify(x => x.Find(1), Times.Once);
        }

        [Test]
        public void Add_Board()
        {
            var boardRepo = new Mock<IBoardRepository>();
            boardRepo.Setup(x => x.Add(It.IsAny<Board>())).Returns(true);
            var controller = new BoardsController(boardRepo.Object);
            var board = new Board();
            board.Id =4;
            board.Name = "UnitTest";
            controller.Add(board);

            boardRepo.Verify(x => x.Add(board), Times.Once);
        }

        [Test]
        public void Delete_NoExist()
        {
            var boardRepo = new Mock<IBoardRepository>();
            boardRepo.Setup(x => x.Delete(It.IsAny<Board>())).Returns(true);
            var controller = new BoardsController(boardRepo.Object);

            Assert.Throws<ArgumentException>(() =>
            {
                controller.Delete(0);
            });
        }
    }
}
