using Henohenon.Scripts.LibUnity;
using UnityEngine;

namespace Henohenon.Scripts.GameUnity.General
{
    public class SoundSingleton: MonoBehaviour, ISoundEffectsPlayer, IVoicePlayer
    {
        [SerializeField] private AudioSource seSource;
        [SerializeField] private AudioSource voiceSource;
        
        void ISoundEffectsPlayer.Play(AudioClip clip)
        {
            seSource.PlayOneShot(clip);
        }

        void IVoicePlayer.Play(AudioClip clip)
        {
            voiceSource.clip = clip;
            voiceSource.Play();
        }
    }
}