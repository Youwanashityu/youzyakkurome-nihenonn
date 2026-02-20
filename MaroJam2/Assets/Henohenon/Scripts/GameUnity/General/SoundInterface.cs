using UnityEngine;

namespace Henohenon.Scripts.GameUnity.General
{
    public interface ISoundEffectsPlayer
    {
        public void Play(AudioClip clip);
    }

    public interface IVoicePlayer
    {
        public void Play(AudioClip clip);
    }
}