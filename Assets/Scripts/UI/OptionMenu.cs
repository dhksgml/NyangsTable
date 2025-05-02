using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    public GameObject optionPanel;

    private float currentBGMVolume;
    private float currentSFXVolume;

    public void SetActive(bool isActive)
    {
        optionPanel.SetActive(isActive);
    }
    public void SaveCurrentVolume()
    {
        currentBGMVolume = AudioManager.Instance.bgmSource.volume;
        currentSFXVolume = AudioManager.Instance.sfxSource.volume;
    }

    public void RevertSetting()
    {
        AudioManager.Instance.bgmSource.volume = currentBGMVolume;
        AudioManager.Instance.sfxSource.volume = currentSFXVolume;
    }

    public void ApplyChangeSetting()
    {
        //변경 사항 적용

        optionPanel.SetActive(false);
    }
}
