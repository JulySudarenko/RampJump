using UnityEngine;

namespace Code.UniversalFactory
{
    internal class ObjectInitialization
    {
        private readonly IFactory _factory;

        public ObjectInitialization(IFactory factory)
        {
            _factory = factory;
        }

        public GameObject Create()
        {
            var newObject = _factory.Create();
            return newObject;
        }
    }
}
