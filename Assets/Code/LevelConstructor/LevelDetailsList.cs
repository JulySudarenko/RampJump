using System;
using System.Collections;

namespace Code.UniversalFactory
{
    internal sealed class LevelDetailsList : IEnumerator, IEnumerable
    {
        private LevelDetail[] _levelDetails;
        private LevelDetail _current;
        private int _index = -1;

        public LevelDetailsList()
        {
            _levelDetails = new LevelDetail[0];
        }
        
        public int Count => _levelDetails.Length;

        public void AddLevelDetail(LevelDetail levelDetail)
        {
            if (_levelDetails == null)
            {
                _levelDetails = new[] {levelDetail};
                return;
            }

            Array.Resize(ref _levelDetails, Count + 1);
            _levelDetails[Count - 1] = levelDetail;
        }

        public LevelDetail this[int index]
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
