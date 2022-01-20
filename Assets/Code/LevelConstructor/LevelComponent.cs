using UnityEngine;

namespace Code.LevelConstructor
{
    internal class LevelComponent
    {
        public readonly Transform Detail;
        public readonly int Number;

        public LevelComponent(Transform detail, int number)
        {
            Detail = detail;
            Number = number;
        }
    }
}
