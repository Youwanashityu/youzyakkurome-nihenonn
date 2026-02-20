using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BgmController : MonoBehaviour
{
    [SerializeField]
    private int loopStartSample;
    [SerializeField]
    private int loopEndSample;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }
    
    private void Update()
    {
        if(_audioSource.timeSamples >= loopEndSample)
        {
            _audioSource.timeSamples = loopStartSample;
        }
    }
}
