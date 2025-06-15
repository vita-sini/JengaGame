using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Volume Settings")]
        [Range(0f, 1f)] public float GlobalVolume = 1f;
        [Range(0f, 1f)] public float MusicVolume = 1f;
        [Range(0f, 1f)] public float EffectsVolume = 1f;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _effectsSource;

        private const string GlobalVolumeKey = "GlobalVolume";
        private const string MusicVolumeKey = "MusicVolume";
        private const string EffectsVolumeKey = "EffectsVolume";

        private void Awake()
        {
            LoadVolumes();
            ApplyVolumes();
        }

        public void LoadVolumes()
        {
            GlobalVolume = PlayerPrefs.GetFloat(GlobalVolumeKey, 1f);
            MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
            EffectsVolume = PlayerPrefs.GetFloat(EffectsVolumeKey, 1f);
        }

        public void SaveVolumes()
        {
            PlayerPrefs.SetFloat(GlobalVolumeKey, GlobalVolume);
            PlayerPrefs.SetFloat(MusicVolumeKey, MusicVolume);
            PlayerPrefs.SetFloat(EffectsVolumeKey, EffectsVolume);
        }

        public void ApplyVolumes()
        {
            AudioListener.volume = GlobalVolume;

            if (_musicSource != null)
                _musicSource.volume = MusicVolume * GlobalVolume;

            if (_effectsSource != null)
                _effectsSource.volume = EffectsVolume * GlobalVolume;
        }

        public void SetMusicSource(AudioSource source) => _musicSource = source;
        public void SetEffectsSource(AudioSource source) => _effectsSource = source;
    }
}
