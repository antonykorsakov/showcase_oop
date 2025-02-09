using System;
using Features.Common.Core;
using Features.LogModule.Core;
using UnityEngine;

namespace Features.TetrisModule.Core.Controller
{
    public sealed class GameParameterController
    {
        private readonly RandomShuffle _randomShuffle = new();
        private readonly string _name;
        private int _limit;
        private int _value = -1;

        public GameParameterController(string name)
        {
            _name = name;
        }

        public int Limit => _limit;
        public int Value => _value;

        public event Action ValueChangedEvent;

        public void Set(int limit, int value)
        {
            _limit = limit;
            _randomShuffle.Initialize(_limit);
            SetValue(value);
        }

        public void Set(int limit)
        {
            _limit = limit;
            _randomShuffle.Initialize(_limit);
            SetValue(_value);
        }

        public void IncreaseValue() => SetValue(_value + 1);
        public void DecreaseValue() => SetValue(_value - 1);

        public void SetRandomValue()
        {
            var randomValue = _randomShuffle.GetNumber();
            SetValue(randomValue);
        }

        private void SetValue(int value)
        {
            value = Mathf.Clamp(value, 0, _limit - 1);
            if (_value == value)
                return;

            Log.Print($"{_name} (0..{_limit}) : {_value} -> {value};");
            _value = value;
            ValueChangedEvent?.Invoke();
        }
    }
}