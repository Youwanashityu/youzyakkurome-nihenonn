using UnityEngine;

namespace Henohenon.Scripts.GameUnity.General
{
    public class GeneralElements : MonoBehaviour
    {
        [SerializeField] private SoundPlayer soundPlayer;
        [SerializeField] private AudioClip click;
        [SerializeField] private AudioClip clickDecision;
        [SerializeField] private AudioClip likeabilityUp;
        [SerializeField] private AudioClip likeabilityDown;
        [SerializeField] private AudioClip gatyaStart;
        [SerializeField] private AudioClip gatyaSingleN;
        [SerializeField] private AudioClip gatyaSingleSr;
    
        public ISoundEffectsPlayer SoundEffectsPlayer => soundPlayer;
        public IVoicePlayer VoicePlayer => soundPlayer;
        public AudioClip Click => click;
        public AudioClip ClickDecision => clickDecision;
        public AudioClip LikeabilityUp => likeabilityUp;
        public AudioClip LikeabilityDown => likeabilityDown;
        public AudioClip GatyaStart => gatyaStart;
        public AudioClip GatyaSingleN => gatyaSingleN;
        public AudioClip GatyaSingleSr => gatyaSingleSr;    
    }
}
