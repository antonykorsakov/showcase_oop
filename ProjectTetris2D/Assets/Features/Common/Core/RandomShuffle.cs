using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Features.Common.Core
{
    public sealed class RandomShuffle
    {
        private int _index;
        private int _count;
        private List<int> _shuffledList;

        public void Initialize(int limit, int multiplier = 2)
        {
            _count = limit * multiplier;
            _shuffledList = new List<int>(_count);
            _index = 0;

            for (int counter = 0; counter < multiplier; counter++)
            {
                for (int number = 0; number < limit; number++)
                    _shuffledList.Add(number);
            }

            Shuffle();
        }

        public int GetNumber()
        {
            int value = _shuffledList[_index];
            _index++;

            if (_index >= _count)
            {
                Shuffle();
                _index = 0;
            }

            return value;
        }

        private void Shuffle()
        {
            for (int i = _count - 1; i > 0; i--)
            {
                int index = Random.Range(0, i + 1);
                (_shuffledList[i], _shuffledList[index]) = (_shuffledList[index], _shuffledList[i]);
            }
        }
    }
}