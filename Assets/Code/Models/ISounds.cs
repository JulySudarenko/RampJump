using UnityEngine;

namespace Code.Models
{
    public interface ISounds
    {
        AudioSource AudioSource { get; }
        AudioClip BallSlimeSound { get; }
        AudioClip CoinSound { get; }
    }
}
