using UnityEngine;

namespace Code.UniversalFactory
{
    internal class LevelDetail
    {
        public readonly Transform Detail;
        public readonly int LevelNumber;

        public LevelDetail(Transform detail, int levelNumber)
        {
            Detail = detail;
            LevelNumber = levelNumber;
        }

    }
}
