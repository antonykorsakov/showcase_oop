using System;
using System.Reflection;
using Features.TetrisModule.Core.Controller;
using Features.TetrisModule.Core.Data;
using Moq;
using NUnit.Framework;

namespace Features.TetrisModule.Tests.EditMode
{
    public sealed class TetrisGridControllerTests
    {
        private const BindingFlags BindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;

        private TetrisGridController _controller;
        private Mock<ITetrisCoreConfig> _configMock;

        [SetUp]
        public void Setup()
        {
            _configMock = new Mock<ITetrisCoreConfig>();
        }

        [Test]
        public void Clear_SetsAllCellsToEmptyAndInvokesEvents()
        {
            _configMock.Setup(c => c.Width).Returns(5);
            _configMock.Setup(c => c.Height).Returns(5);
            _controller = new TetrisGridController(_configMock.Object);

            bool gridClearedEventCalled = false;
            bool gridChangedEventCalled = false;

            _controller.GridClearedEvent += () => gridClearedEventCalled = true;
            _controller.GridChangedEvent += () => gridChangedEventCalled = true;

            ((ITetrisGridController)_controller).Clear();

            for (int x = 0; x < _controller.Width; x++)
            {
                for (int y = 0; y < _controller.Height; y++)
                {
                    Assert.AreEqual(CellState.Empty, _controller.GetCellState(x, y));
                }
            }

            Assert.IsTrue(gridClearedEventCalled);
            Assert.IsTrue(gridChangedEventCalled);
        }

        [Test]
        public void ClearFullLines_RemovesFullLinesAndInvokesEvents()
        {
            _configMock.Setup(c => c.Width).Returns(4);
            _configMock.Setup(c => c.Height).Returns(4);
            _controller = new TetrisGridController(_configMock.Object);

            bool gridLinesBurntEventCalled = false;
            bool gridChangedEventCalled = false;

            _controller.GridLinesBurntEvent += count => gridLinesBurntEventCalled = true;
            _controller.GridChangedEvent += () => gridChangedEventCalled = true;

            for (int x = 0; x < _controller.Width; x++)
            {
                _controller.GetCellState(x, 0);

                var field = typeof(TetrisGridController).GetField("_grid", BindingAttr);
                Assert.IsNotNull(field, "_grid field should not be null");

                var grid = (CellState[,])field.GetValue(_controller);
                grid[x, 0] = CellState.I;
            }

            int clearedLines = ((ITetrisGridController)_controller).ClearFullLines();

            Assert.AreEqual(1, clearedLines);
            Assert.IsTrue(gridLinesBurntEventCalled);
            Assert.IsTrue(gridChangedEventCalled);

            for (int y = 0; y < _controller.Height; y++)
            {
                for (int x = 0; x < _controller.Width; x++)
                {
                    Assert.AreEqual(CellState.Empty, _controller.GetCellState(x, y));
                }
            }
        }

        [Test]
        public void Constructor_InvalidDimensions_ThrowsException()
        {
            _configMock.Setup(c => c.Width).Returns(0);
            _configMock.Setup(c => c.Height).Returns(-5);

            Assert.Throws<ArgumentException>(() => new TetrisGridController(_configMock.Object));
        }

        [Test]
        public void Constructor_ValidDimensions_CreatesGrid()
        {
            _configMock.Setup(c => c.Width).Returns(10);
            _configMock.Setup(c => c.Height).Returns(20);

            _controller = new TetrisGridController(_configMock.Object);

            Assert.AreEqual(10, _controller.Width);
            Assert.AreEqual(20, _controller.Height);
        }
    }
}