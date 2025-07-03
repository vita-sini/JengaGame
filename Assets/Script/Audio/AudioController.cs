using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _effectsAudioSource;
    [SerializeField] private AudioClip[] _soundEffects;

    private void Start()
    {
        AudioManager.ApplyVolumes();
        UpdateMusicVolume();
        UpdateEffectsVolume();
    }

    private void Update()
    {
        UpdateMusicVolume();
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
            _effectsAudioSource.volume = AudioManager.EffectsVolume * AudioManager.GlobalVolume;
    }

    public void PlaySoundEffect(int index)
    {
        if (index >= 0 && index < _soundEffects.Length)
            AudioSource.PlayClipAtPoint(_soundEffects[index], Camera.main.transform.position, AudioManager.EffectsVolume * AudioManager.GlobalVolume);
    }
}
