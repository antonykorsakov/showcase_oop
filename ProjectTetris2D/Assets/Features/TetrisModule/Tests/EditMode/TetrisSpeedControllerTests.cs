using System.Reflection;
using Features.TetrisModule.Core.Controller;
using Features.TetrisModule.Core.Data;
using Moq;
using NUnit.Framework;

namespace Features.TetrisModule.Tests.EditMode
{
    public sealed class TetrisSpeedControllerTests
    {
        private const BindingFlags BindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;

        private Mock<ITetrisCoreConfig> _configMock;
        private ITetrisSpeedController _controller;

        [SetUp]
        public void SetUp()
        {
            _configMock = new Mock<ITetrisCoreConfig>();
            _configMock.Setup(c => c.SpeedLevelsCount).Returns(10);
            _configMock.Setup(c => c.FastFallDelay).Returns(0.1f);
            _configMock.Setup(c => c.GetStandardFallDelay(It.IsAny<int>())).Returns(1f);

            _controller = new TetrisSpeedController(_configMock.Object);
        }

        [Test]
        public void Pause_ShouldNotTriggerCallbackIfNotRunning()
        {
            var callbackCalled = false;

            _controller.SetAction(() => callbackCalled = true);
            _controller.Level.Set(0, 0);
            _controller.Pause();
            _controller.Tick(1.0f);

            Assert.IsFalse(callbackCalled);
        }

        [Test]
        public void TurnOffAccelerate_ShouldRestoreDefaultDelay()
        {
            _controller.TurnOnAccelerate();
            var controllerType = typeof(TetrisSpeedController);
            var currentDelayField = controllerType.GetField("_currentDelay", BindingAttr);
            Assert.IsNotNull(currentDelayField, "_currentDelay field should not be null");

            var currentDelay = (float)currentDelayField.GetValue(_controller);
            Assert.AreEqual(0.1f, currentDelay);

            _controller.TurnOffAccelerate();

            currentDelay = (float)currentDelayField.GetValue(_controller);
            Assert.AreEqual(1f, currentDelay);
        }

        [Test]
        public void TurnOnAccelerate_ShouldChangeDelayToFastFallDelay()
        {
            _controller.TurnOnAccelerate();

            var controllerType = typeof(TetrisSpeedController);
            var currentDelayField = controllerType.GetField("_currentDelay", BindingAttr);
            Assert.IsNotNull(currentDelayField, "_currentDelay field should not be null");

            var currentDelay = (float)currentDelayField.GetValue(_controller);
            Assert.AreEqual(0.1f, currentDelay);
        }
    }
}