using Code.GameState;
using Code.LevelConstructor;
using Code.Models;
using Code.UniversalFactory;
using UnityEngine;

namespace Code.Audio
{
    internal class AudioPlayer : ISoundPlayer
    {
        private readonly AudioSource _source;
        private readonly AudioClip _clip;

        public AudioPlayer(AudioSource source, AudioClip clip)
        {
            _clip = clip;
            _source = source;
        }

        public void PlaySound()
        {
            _source.clip = _clip;
            _source.Play();
        }

        public void StopSound()
        {
            _source.Stop();
        }
    }
}
