using UnityEngine;

namespace Code.UniversalFactory
{
    internal class Factory : IFactory
    {
        private readonly Transform _config;
        private readonly Transform _folder;

        public Factory(Transform config, Transform folder)
        {
            _config = config;
            _folder = folder;
        }

        public GameObject Create()
        {
            return Object.Instantiate(_config.gameObject, _folder);
        }
    }
}
