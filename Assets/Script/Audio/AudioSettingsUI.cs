using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class AudioSettingsUI : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Slider _globalVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _effectsVolumeSlider;

        [Header("Audio Manager")]
        [SerializeField] private AudioManager _audio;

        private void Start()
        {
            if (_audio == null)
            {
                return;
            }

            _globalVolumeSlider.value = _audio.GlobalVolume;
            _musicVolumeSlider.value = _audio.MusicVolume;
            _effectsVolumeSlider.value = _audio.EffectsVolume;

            _globalVolumeSlider.onValueChanged.AddListener(v =>
            {
                _audio.GlobalVolume = v;
                _audio.ApplyVolumes();
                _audio.SaveVolumes();
            });

            _musicVolumeSlider.onValueChanged.AddListener(v =>
            {
                _audio.MusicVolume = v;
                _audio.ApplyVolumes();
                _audio.SaveVolumes();
            });

            _effectsVolumeSlider.onValueChanged.AddListener(v =>
            {
                _audio.EffectsVolume = v;
                _audio.ApplyVolumes();
                _audio.SaveVolumes();
            });
        }
    }
}
