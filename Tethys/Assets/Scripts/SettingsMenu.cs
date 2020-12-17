using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;


public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int curResIndex = 0;


        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                curResIndex = i;
            }
        }
        resDropdown.AddOptions(options);
        resDropdown.value = curResIndex;
        resDropdown.RefreshShownValue();

    }

    public void AdjustVol(float vol)
    {
        audioMixer.SetFloat("volume", vol);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        //Debug.Log("Fullscreen toggle clicked");
        Screen.fullScreen = isFullscreen;
    }

    public void SetRes(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //Debug.Log("Resolution changed to: " + resolution);
    }
}
