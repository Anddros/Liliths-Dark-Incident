using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public void ToggleFullScreen()
    {
        if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }

    public void ActivateVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
}
