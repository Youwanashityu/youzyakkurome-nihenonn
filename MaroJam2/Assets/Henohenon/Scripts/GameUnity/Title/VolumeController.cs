using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;

    private void OnEnable()
    {
        bgmSlider.onValueChanged.AddListener(ChangeBgmVolume);
        seSlider.onValueChanged.AddListener(ChangeSeVolume);
        ChangeBgmVolume(bgmSlider.value);
        ChangeSeVolume(seSlider.value);
    }

    private void OnDisable()
    {
        bgmSlider.onValueChanged.RemoveAllListeners();
        seSlider.onValueChanged.RemoveAllListeners();
    }

    private void ChangeBgmVolume(float scale)
    {
        ChangeVolume("BGM-Volume", scale);
    }

    private void ChangeSeVolume(float scale)
    {
        ChangeVolume("SE-Volume", scale);
    }

    private void ChangeVolume(string propName, float scale)
    {
        audioMixer.SetFloat(propName, scale * (scale > 0 ? 20 : 80f));
    }
}
