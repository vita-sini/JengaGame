using System.Collections;
using System.Collections.Generic;
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

        if (musicAudioSource != null)
        {
            musicAudioSource.volume = MusicVolume * GlobalVolume;
        }

        if (effectsAudioSource != null)
        {
            effectsAudioSource.volume = EffectsVolume * GlobalVolume;
        }
    }

    private static AudioSource GetMusicAudioSource()
    {
        // Здесь вы можете вернуть ссылку на ваш AudioSource для музыки
        // Например, если у вас есть GameObject с именем "MusicPlayer"

        GameObject musicPlayer = GameObject.Find("BackgroundMusic");

        if (musicPlayer != null)
        {
            return musicPlayer.GetComponent<AudioSource>();
        }

        return null;
    }

    private static AudioSource GetEffectsAudioSource()
    {
        // Здесь вы можете вернуть ссылку на ваш AudioSource для звуковых эффектов
        // Например, если у вас есть GameObject с именем "EffectsPlayer"

        GameObject effectsPlayer = GameObject.Find("GameObject");

        if (effectsPlayer != null)
        {
            return effectsPlayer.GetComponent<AudioSource>();
        }

        return null;
    }
}
