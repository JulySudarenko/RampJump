using Code.Assistant;
using UnityEngine;

namespace Code.Interfaces
{
    internal interface IActiveObject
    {
        GameObject ActiveObjectPrefab { get; }
        ActiveObjectsName Name { get; }
        float Speed { get; }
    }
}
