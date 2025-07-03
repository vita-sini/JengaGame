using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _globalVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _effectsVolumeSlider;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _effectsAudioSource;

    private void Start()
    {
        _globalVolumeSlider.value = AudioManager.GlobalVolume;
        _musicVolumeSlider.value = AudioManager.MusicVolume;
        _effectsVolumeSlider.value = AudioManager.EffectsVolume;

        _globalVolumeSlider.onValueChanged.AddListener(OnGlobalVolumeChanged);
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        _effectsVolumeSlider.onValueChanged.AddListener(OnEffectsVolumeChanged);
    }

    private void OnGlobalVolumeChanged(float value)
    {
        AudioManager.GlobalVolume = value;
        AudioManager.ApplyVolumes();
    }

    private void OnMusicVolumeChanged(float value)
    {
        AudioManager.MusicVolume = value;
        AudioManager.ApplyVolumes();
        UpdateMusicVolume();
    }

    private void OnEffectsVolumeChanged(float value)
    {
        AudioManager.EffectsVolume = value;
        AudioManager.ApplyVolumes();
        UpdateEffectsVolume();
    }

    private void UpdateMusicVolume()
    {
        if (_musicAudioSource != null)
            _musicAudioSource.volume = AudioManager.MusicVolume * AudioManager.GlobalVolume;
    }

    private void UpdateEffectsVolume()
    {
        if (_effectsAudioSource != null)
            _effectsAudioSource.volume = AudioManager.EffectsVolume;
    }
}
