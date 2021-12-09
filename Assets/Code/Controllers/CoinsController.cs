using System.Collections.Generic;
using Code.Assistant;
using Code.Interfaces;
using Code.LevelConstructor;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.Controllers
{
    internal class CoinsController : IInitialize, ICleanup
    {
        private readonly LevelComponentsList _coins;
        private TriggerContacts[] _coinsHits;
        private int _counter;

        public CoinsController(LevelComponentsList coins)
        {
            _coins = coins;
        }

        public void Initialize()
        {
            var coinsHits = new List<TriggerContacts>();
            for (int i = 0; i < _coins.Count; i++)
            {
                coinsHits.Add(_coins[i].Detail.gameObject.GetOrAddComponent<TriggerContacts>());
            }

            _coinsHits = coinsHits.ToArray();

            for (int i = 0; i < _coinsHits.Length; i++)
            {
                _coinsHits[i].IsContact += TakeCoin;
            }
        }

        private void TakeCoin(int ball, int coin)
        {
            for (int i = 0; i < _coins.Count; i++)
            {
                if (_coins[i].Number == coin)
                {
                    _coins[i].Detail.gameObject.SetActive(false);
                }

                Debug.Log($"Coin controller {coin}, {_coins[i].Number}");
            }
        }

        public void Cleanup()
        {
            for (int i = 0; i < _coinsHits.Length; i++)
            {
                _coinsHits[i].IsContact -= TakeCoin;
            }
        }
    }
}
