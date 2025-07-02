using UnityEngine;

public static class AudioManager
{
    private const string GlobalVolumeKey = "GlobalVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string EffectsVolumeKey = "EffectsVolume";

    public static float GlobalVolume
    {
        get { return PlayerPrefs.GetFloat(GlobalVolumeKey, 1.0f); }
        set { PlayerPrefs.SetFloat(GlobalVolumeKey, value); }
    }

    public static float MusicVolume
    {
        get { return PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f); }
        set { PlayerPrefs.SetFloat(MusicVolumeKey, value); }
    }

    public static float EffectsVolume
    {
        get { return PlayerPrefs.GetFloat(EffectsVolumeKey, 1.0f); }
        set { PlayerPrefs.SetFloat(EffectsVolumeKey, value); }
    }

    public static void ApplyVolumes()
    {
        AudioListener.volume = GlobalVolume;

        AudioSource musicAudioSource = GetMusicAudioSource();
        AudioSource effectsAudioSource = GetEffectsAudioSource();

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        if (musicAudioSource != null)
            musicAudioSource.volume = MusicVolume * GlobalVolume;

        if (effectsAudioSource != null)
            effectsAudioSource.volume = EffectsVolume * GlobalVolume;
    }
=======
=======
>>>>>>> Stashed changes
        public void SaveVolumes()
        {
            PlayerPrefs.SetFloat(GlobalVolumeKey, _globalVolume);
            PlayerPrefs.SetFloat(MusicVolumeKey, _musicVolume);
            PlayerPrefs.SetFloat(EffectsVolumeKey, _effectsVolume);
        }
>>>>>>> Stashed changes

    private static AudioSource GetMusicAudioSource()
    {
        GameObject musicPlayer = GameObject.Find("BackgroundMusic");

        if (musicPlayer != null)
            return musicPlayer.GetComponent<AudioSource>();

        return null;
    }

    private static AudioSource GetEffectsAudioSource()
    {
        GameObject effectsPlayer = GameObject.Find("GameObject");

        if (effectsPlayer != null)
            return effectsPlayer.GetComponent<AudioSource>();

<<<<<<< Updated upstream
        return null;
=======
        public void SetEffectsVolume(float value)
        {
            _effectsVolume = Mathf.Clamp01(value);
            ApplyVolumes();
            SaveVolumes();
        }

        private void LoadVolumes()
        {
            _globalVolume = PlayerPrefs.GetFloat(GlobalVolumeKey, 1f);
            _musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
            _effectsVolume = PlayerPrefs.GetFloat(EffectsVolumeKey, 1f);
        }
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    }
}
