using UnityEngine;

namespace Code.Models
{
    public interface IBallSoundsModel
    {
        AudioSource AudioSource { get; }
        AudioClip BallSlimeSound { get; }
        AudioClip CoinSound { get; }
    }
}
