using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;
    [SerializeField] private Slider cvSlider;

    private void OnEnable()
    {
        bgmSlider.onValueChanged.AddListener(ChangeBgmVolume);
        seSlider.onValueChanged.AddListener(ChangeSeVolume);
        cvSlider.onValueChanged.AddListener(ChangeCvVolume);
        ChangeBgmVolume(bgmSlider.value);
        ChangeSeVolume(seSlider.value);
        ChangeCvVolume(cvSlider.value);
    }

    private void OnDisable()
    {
        bgmSlider.onValueChanged.RemoveAllListeners();
        seSlider.onValueChanged.RemoveAllListeners();
        cvSlider.onValueChanged.RemoveAllListeners();
    }

    private void ChangeBgmVolume(float scale)
    {
        ChangeVolume("BGM", scale);
    }

    private void ChangeSeVolume(float scale)
    {
        ChangeVolume("SE", scale);
    }

    private void ChangeCvVolume(float scale)
    {
        ChangeVolume("CV", scale);
    }

    private void ChangeVolume(string propName, float scale)
    {
        audioMixer.SetFloat(propName, scale * (scale > 0 ? 20 : 80f));
    }
}
