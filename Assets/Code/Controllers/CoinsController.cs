using System.Collections.Generic;
using Code.Assistant;
using Code.Audio;
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
        private readonly ISoundPlayer _soundPlayer;
        private readonly float _rotationAngle;
        private readonly float _rotationSpeed;
        private TriggerContacts[] _coinsHits;
        private int _counter;


        public CoinsController(LevelComponentsList coins, AudioSource audioSource, AudioClip clip, float rotationAngle,
            float rotationSpeed)
        {
            _coins = coins;
            _rotationAngle = rotationAngle;
            _rotationSpeed = rotationSpeed;
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

            TwistCoins();
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
