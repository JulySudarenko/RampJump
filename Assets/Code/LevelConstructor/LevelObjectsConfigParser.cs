using Code.Configs;
using UnityEngine;

namespace Code.UniversalFactory
{
    internal class LevelObjectsConfigParser : ILevel
    {
        public Transform BallStartPlace { get; private set; }
        public Vector3 BallStartPosition { get; private set; }
        public Vector3 HoleStartPosition { get; private set; }
        public int TotalLevels { get; }
        private readonly LevelDetailsList _levelDetails;
        private readonly LevelObjectConfig[] _levelObjectConfigs;

        public LevelObjectsConfigParser(LevelObjectConfig[] data)
        {
            _levelObjectConfigs = data;
            _levelDetails = new LevelDetailsList();
            TotalLevels = data.Length;
            Init();
        }

        private void Init()
        {
            BallStartPlace = new ObjectInitialization(new Factory(_levelObjectConfigs[0].BallStartPlace))
                .Create();
            for (int i = 0; i < _levelObjectConfigs.Length; i++)
            {
                for (int j = 0; j < _levelObjectConfigs[i].LevelDetailsPrefabs.Count; j++)
                {
                    var levelDetail = _levelObjectConfigs[i].LevelDetailsPrefabs[j];
                    var obj = new ObjectInitialization(
                        new Factory(levelDetail)).Create();
                    obj.gameObject.SetActive(false);
                    _levelDetails.AddLevelDetail(new LevelDetail(obj.transform, _levelObjectConfigs[i].LevelNumber));
                }
            }
        }

        public void InitNewLevel(int levelNumber)
        {
            RestartLevel(levelNumber);


            for (int i = 0; i < _levelObjectConfigs.Length; i++)
            {
                if (_levelObjectConfigs[i].LevelNumber == levelNumber)
                {
                    BallStartPlace.position = _levelObjectConfigs[i].BallStartPlace.position;
                    HoleStartPosition = _levelObjectConfigs[i].HoleStartPosition.position;
                    BallStartPosition = _levelObjectConfigs[i].BallStartPosition.position;
                }
            }
        }

        private void RestartLevel(int levelNumber)
        {
            for (int i = 0; i < _levelDetails.Count; i++)
            {
                if (_levelDetails[i].LevelNumber == levelNumber)
                {
                    _levelDetails[i].Detail.gameObject.SetActive(true);
                }
                else
                {
                    _levelDetails[i].Detail.gameObject.SetActive(false);
                }
            }
        }
    }
}
