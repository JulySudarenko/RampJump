using Code.Audio;
using Code.Interfaces;
using Code.LevelConstructor;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.Controllers
{
    internal class SlimeCheckout : IInitialize, ICleanup
    {
        private readonly LevelComponentsList _componentsList;
        private readonly Hit _ballHit;
        private readonly AudioPlayer _audioPlayer;
        private bool _isDetailHit;
        private bool _isSlime;

        public SlimeCheckout(LevelComponentsList detailsList, AudioSource source, AudioClip clip, Hit ballHit)
        {
            _componentsList = detailsList;
            _ballHit = ballHit;
            _audioPlayer = new AudioPlayer(source, clip);
            _componentsList = detailsList;
        }

        public void Initialize()
        {
            _ballHit.OnHitStay += OnStartSlime;
            _ballHit.OnHitEnd += OnEndSlime;
        }

        private void OnStartSlime(int detailID, int ballID)
        {
            if (!_isSlime)
            {
                CheckHit(detailID, ballID);
                if (_isDetailHit)
                {
                    _audioPlayer.PlaySound();
                    _isDetailHit = false;
                    _isSlime = true;
                }
            }
        }

        private void OnEndSlime(int detailID, int ballID)
        {
            CheckHit(detailID, ballID);
            if (_isDetailHit)
            {
                _audioPlayer.StopSound();
                _isDetailHit = false;
                _isSlime = false;
            }
        }

        private void CheckHit(int detailID, int ballID)
        {
            for (int i = 0; i < _componentsList.Count; i++)
            {
                if (_componentsList[i].Number == detailID)
                {
                    _isDetailHit = true;
                }
            }
        }

        public void Cleanup()
        {
            _ballHit.OnHitStay -= OnStartSlime;
            _ballHit.OnHitEnd -= OnEndSlime;
        }
    }
}
