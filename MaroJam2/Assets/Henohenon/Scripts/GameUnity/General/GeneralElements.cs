using UnityEngine;

namespace Henohenon.Scripts.GameUnity.General
{
    public class GeneralElements : MonoBehaviour
    {
        [SerializeField] private SoundPlayer soundPlayer;

        public ISoundEffectsPlayer SoundEffectsPlayer => soundPlayer;
        public IVoicePlayer VoicePlayer => soundPlayer;
    }
}
