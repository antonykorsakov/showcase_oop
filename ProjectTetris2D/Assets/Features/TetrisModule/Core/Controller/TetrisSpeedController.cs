using System;
using Features.TetrisModule.Core.Data;

namespace Features.TetrisModule.Core.Controller
{
    public sealed class TetrisSpeedController : ITetrisSpeedController
    {
        private readonly ITetrisCoreConfig _config;

        private float _timer;
        private float _currentDelay;
        private bool _isRunning;
        private Action _callback;

        public TetrisSpeedController(ITetrisCoreConfig config)
        {
            _config = config;

            Level.ValueChangedEvent += LevelOnValueChangedEvent;
            Level.Set(config.SpeedLevelsCount, 0);
        }

        public GameParameterController Level { get; } = new("SpeedLevel");

        public void Pause() => _isRunning = false;
        public void Resume() => _isRunning = true;

        public void Tick(float deltaTime)
        {
            if (!_isRunning || _callback == null)
                return;

            _timer += deltaTime;
            if (_timer >= _currentDelay)
            {
                _callback();
                _timer = 0f;
            }
        }

        void ITetrisSpeedController.SetAction(Action callback) => _callback = callback;

        void ITetrisSpeedController.TurnOnAccelerate()
        {
            _currentDelay = _config.FastFallDelay;
            _timer = 0;
        }

        void ITetrisSpeedController.TurnOffAccelerate()
        {
            _currentDelay = _config.GetStandardFallDelay(Level.Value);
            _timer = 0;
        }
        
        private void LevelOnValueChangedEvent()
        {
            _currentDelay = _config.GetStandardFallDelay(Level.Value);
            _timer = 0;
        }
    }
}