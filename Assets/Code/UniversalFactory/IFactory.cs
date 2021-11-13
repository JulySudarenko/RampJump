using UnityEngine;

namespace Code.UniversalFactory
{
    internal interface IFactory
    {
        GameObject Create();
    }
}
