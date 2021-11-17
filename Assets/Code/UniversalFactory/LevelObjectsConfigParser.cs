using Code.Configs;
using UnityEngine;

namespace Code.UniversalFactory
{
    internal class LevelObjectsConfigParser
    {
        public GameObject BallStartPlace;
        public GameObject HoleObject;
        public Vector3 BallStartPosition;
        public Vector3 GlassStartPosition;
        public Vector3 HoleStartPosition;

        private readonly LevelObjectConfig[] _levelObjectConfigs;

        public LevelObjectsConfigParser(Data data)
        {
            _levelObjectConfigs = data.LevelObjectConfig;

            Init();
        }

        private void Init()
        {
            for (int i = 0; i < _levelObjectConfigs.Length; i++)
            {
                if (_levelObjectConfigs[i].LevelNumber == 1)
                {
                    BallStartPlace = new ObjectInitialization(new Factory(_levelObjectConfigs[i].BallStartPlace))
                        .Create().gameObject;
                    BallStartPosition = _levelObjectConfigs[i].BallStartPosition.position;
                    GlassStartPosition = _levelObjectConfigs[i].GlassStartPosition.position;
                    for (int j = 0; j < _levelObjectConfigs[i].LevelDetailsPrefabs.Count; j++)
                    {
                        var levelDetail = _levelObjectConfigs[i].LevelDetailsPrefabs[j];
                        var obj = new ObjectInitialization(
                            new Factory(levelDetail)).Create();
                    }
                }
            }
        }

    }
}
