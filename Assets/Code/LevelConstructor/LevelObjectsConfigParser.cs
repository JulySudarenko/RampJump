using Code.Assistant;
using Code.Configs;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.LevelConstructor
{
    internal class LevelObjectsConfigParser : ILevel
    {
        public LevelComponentsList CoinsList { get; private set; }
        public Transform Bottom { get; private set; }
        public Transform BallStartPlace { get; private set; }
        public Vector3 BallStartPosition { get; private set; }
        public Vector3 HoleStartPosition { get; private set; }
        public int TotalLevels { get; }
        private readonly LevelComponentsList _levelComponents;
        private readonly LevelObjectConfig[] _levelObjectConfigs;
        private readonly Transform _components;
        private readonly Transform _coins;

        public LevelObjectsConfigParser(LevelObjectConfig[] data)
        {
            _levelObjectConfigs = data;
            _levelComponents = new LevelComponentsList();
            CoinsList = new LevelComponentsList();
            _components = new GameObject("Components").transform;
            _coins = new GameObject("Coins").transform;
            TotalLevels = data.Length;
            Init();
        }

        private void Init()
        {
            Bottom = new ObjectInitialization(new Factory(_levelObjectConfigs[0].BottomPrefab, _components)).Create();
            BallStartPlace = new ObjectInitialization(new Factory(_levelObjectConfigs[0].BallStartPlace, _components))
                .Create();
            for (int i = 0; i < _levelObjectConfigs.Length; i++)
            {
                InitDetails(_levelObjectConfigs[i]);
                InitCoins(_levelObjectConfigs[i]);
            }
        }

        private void InitDetails(LevelObjectConfig levelDetail)
        {
            for (int j = 0; j < levelDetail.LevelDetailsPrefabs.Count; j++)
            {
                var obj = new ObjectInitialization(
                    new Factory(levelDetail.LevelDetailsPrefabs[j], _components)).Create();
                obj.gameObject.SetActive(false);
                _levelComponents.AddLevelDetail(new LevelComponent(obj.transform, levelDetail.LevelNumber));
            }
        }

        private void InitCoins(LevelObjectConfig levelDetail)
        {
            for (int j = 0; j < levelDetail.CoinsPlaces.Count; j++)
            {
                var coin = levelDetail.CoinPrefab;
                coin.transform.position = levelDetail.CoinsPlaces[j].position;

                var newCoin = new ObjectInitialization(
                    new Factory(coin, _coins)).Create();
                newCoin.gameObject.SetActive(false);
                _levelComponents.AddLevelDetail(new LevelComponent(newCoin.transform,
                    levelDetail.LevelNumber));

                var newCoinCollider = newCoin.gameObject.GetOrAddComponent<Collider>();
                CoinsList.AddLevelDetail(new LevelComponent(newCoin.transform,
                    newCoinCollider.gameObject.GetInstanceID()));
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
            for (int i = 0; i < _levelComponents.Count; i++)
            {
                if (_levelComponents[i].Number == levelNumber)
                {
                    _levelComponents[i].Detail.gameObject.SetActive(true);
                }
                else
                {
                    _levelComponents[i].Detail.gameObject.SetActive(false);
                }
            }
        }
    }
}
