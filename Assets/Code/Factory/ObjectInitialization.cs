using UnityEngine;

namespace Code.Factory
{
    internal class ObjectInitialization
    {
        private readonly IFactory _factory;

        public ObjectInitialization(IFactory factory)
        {
            _factory = factory;
        }

        public Transform Create()
        {
            var newObject = _factory.Create();
            return newObject.transform;
        }
    }
}
