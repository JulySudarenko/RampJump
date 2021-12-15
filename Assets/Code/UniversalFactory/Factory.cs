using UnityEngine;

namespace Code.UniversalFactory
{
    internal class Factory : IFactory
    {
        private readonly Transform _config;

        public Factory(Transform config)
        {
            _config = config;
        }

        public GameObject Create()
        {
            return Object.Instantiate(_config.gameObject);
        }
    }
}
