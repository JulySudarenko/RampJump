using System;
using System.Collections;

namespace Code.LevelCreator
{
    public sealed class DetailCreatingInfoList : IEnumerator, IEnumerable
    {
        private DetailCreatingInfo[] _levelDetails;
        private DetailCreatingInfo _current;
        private int _index = -1;
    
        public DetailCreatingInfoList()
        {
            _levelDetails = new DetailCreatingInfo[0];
        }
        
        public int Count => _levelDetails.Length;
    
        public void AddLevelDetail(DetailCreatingInfo detailCreatingInfo)
        {
            if (_levelDetails == null)
            {
                _levelDetails = new[] {detailCreatingInfo};
                return;
            }
    
            Array.Resize(ref _levelDetails, Count + 1);
            _levelDetails[Count - 1] = detailCreatingInfo;
        }
    
        public DetailCreatingInfo this[int index]
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
