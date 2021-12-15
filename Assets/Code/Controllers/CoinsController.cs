using System.Collections.Generic;
using Code.Assistant;
using Code.Interfaces;
using Code.LevelConstructor;
using Code.UniversalFactory;
using DG.Tweening;
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

            TwistCoins();
        }

        private void TwistCoins()
        {
            for (int i = 0; i < _coins.Count; i++)
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(_coins[i].Detail.DORotate(new Vector3(0.0f, 90.0f), 5.0f, RotateMode.Fast));
                sequence.Append(_coins[i].Detail.DORotate(new Vector3(0.0f, 180.0f), 5.0f, RotateMode.Fast));
                sequence.SetLoops(-1, LoopType.Yoyo);
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
            }
        }

        public void Cleanup()
        {
            for (int i = 0; i < _coinsHits.Length; i++)
            {
                _coinsHits[i].IsContact -= TakeCoin;
            }

            for (int i = 0; i < _coins.Count; i++)
            {
                _coins[i].Detail.DOKill();
            }
        }
    }
}
