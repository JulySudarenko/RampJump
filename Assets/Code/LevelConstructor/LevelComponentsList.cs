using System;
using System.Collections;

namespace Code.LevelConstructor
{
    internal sealed class LevelComponentsList : IEnumerator, IEnumerable
    {
        private LevelComponent[] _levelDetails;
        private LevelComponent _current;
        private int _index = -1;

        public LevelComponentsList()
        {
            _levelDetails = new LevelComponent[0];
        }
        
        public int Count => _levelDetails.Length;

        public void AddLevelDetail(LevelComponent levelComponent)
        {
            if (_levelDetails == null)
            {
                _levelDetails = new[] {levelComponent};
                return;
            }

            Array.Resize(ref _levelDetails, Count + 1);
            _levelDetails[Count - 1] = levelComponent;
        }

        public LevelComponent this[int index]
        {
            get => _levelDetails[index];
            set => _levelDetails[index] = value;
        }


        public bool MoveNext()
        {
            if (_index == _levelDetails.Length - 1)
            {
                Reset();
                return false;
            }

            _index++;
            return true;
        
        }
        public void Reset() => _index = -1;

        public object Current => _levelDetails[_index];

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
