using System;
using System.Collections.Generic;
using Code.Assistant;
using Code.Audio;
using Code.GameState;
using Code.Interfaces;
using Code.LevelConstructor;
using Code.UniversalFactory;
using Code.View;
using DG.Tweening;
using UnityEngine;

namespace Code.Controllers
{
    internal class CoinsController : IInitialize, ICleanup, IState
    {
        public event Action<State> OnChangeState;
        private readonly LevelComponentsList _coins;
        private readonly CoinCounterView _coinCounterView;
        private readonly StarEffectView _starEffectView;
        private readonly ISoundPlayer _soundPlayer;
        private readonly float _rotationAngle;
        private readonly float _rotationSpeed;
        private TriggerContacts[] _coinsHits;
        private int _coinsOnLevelMax;
        private int _counter;


        public CoinsController(LevelComponentsList coins, AudioSource audioSource, AudioClip clip, float rotationAngle,
            float rotationSpeed, CoinCounterView coinCounterView, StarEffectView starEffectView)
        {
            _coins = coins;
            _rotationAngle = rotationAngle;
            _rotationSpeed = rotationSpeed;
            _coinCounterView = coinCounterView;
            _starEffectView = starEffectView;
            _soundPlayer = new AudioPlayer(audioSource, clip);
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

            _coinCounterView.HideOrShow(true);
            TwistCoins();
            CountCoins();
        }

        private void TwistCoins()
        {
            for (int i = 0; i < _coins.Count; i++)
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(_coins[i].Detail.DORotate(new Vector3(0.0f, _rotationAngle), _rotationSpeed));
                sequence.Append(_coins[i].Detail.DORotate(new Vector3(0.0f, -_rotationAngle), _rotationSpeed));
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
                    _soundPlayer.PlaySound();
                    _counter++;
                    _coinCounterView.ShowCoinsCount(_counter, _coinsOnLevelMax);
                }
            }
        }
        
        public void ChangeState(State state)
        {
            switch (state)
            {
                case State.Start:
                    CountCoins();
                    break;
                case State.BallTouched:
                    _coinCounterView.HideOrShow(true);
                    break;
                case State.BallKicked:
                    break;
                case State.Victory:
                    _coinCounterView.HideOrShow(false);
                    int rate = GetResult();
                    _starEffectView.ShowEndStarEffect(rate);
                    break;
                case State.Defeat:
                    int rateD = GetResult();
                    _starEffectView.ShowEndStarEffect(rateD);
                    _coinCounterView.HideOrShow(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void CountCoins()
        {
            _coinsOnLevelMax = 0;
            _counter = 0;
            for (int i = 0; i < _coins.Count; i++)
            {
                if (_coins[i].Detail.gameObject.activeInHierarchy)
                {
                    _coinsOnLevelMax++;
                }
            }

            _coinCounterView.ShowCoinsCount(_counter, _coinsOnLevelMax);
        }

        private int GetResult()
        {
            float ratio = (float) _counter / _coinsOnLevelMax * 100.0f;
            Debug.Log(ratio);
            if (ratio >= 99.9f) return 3;
            if (ratio >= 66.6f) return 2;
            if (ratio >= 33.3f) return 1;
            return 0;
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
