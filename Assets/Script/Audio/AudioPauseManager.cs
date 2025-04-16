using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPauseManager : MonoBehaviour
{
    private void OnApplicationFocus(bool hasFocus)
    {
        Debug.Log("Focus changed: " + hasFocus);

        if (!hasFocus)
        {
            MuteAudio();
        }
        else
        {
            UnmuteAudio();
        }
    }

    private void OnApplicationPause(bool isPaused)
    {
        Debug.Log("Pause changed: " + isPaused);
        if (isPaused)
        {
            MuteAudio();
        }
        else
        {
            UnmuteAudio();
        }
    }

    private void MuteAudio()
    {
        AudioListener.pause = true;
    }

    private void UnmuteAudio()
    {
        AudioListener.pause = false;
    }
}
