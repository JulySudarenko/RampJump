using UnityEngine;

namespace Code.Factory
{
    internal class Factory : IFactory
    {
        private readonly GameObject _gameObject;

        public Factory(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public GameObject Create()
        {
            return Object.Instantiate(_gameObject);
        }
    }
}
