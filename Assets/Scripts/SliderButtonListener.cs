using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderButtonListener : MonoBehaviour
{
    public Slider slider;
    public SliderType sliderType;

    public enum SliderType
    {
        BGM,
        SFX
    }

    private void OnEnable()
    {
        switch (sliderType)
        {
            case SliderType.BGM:
                SetBGMSliderValue();
                break;
            case SliderType.SFX:
                SetSFXSliderValue();
                break;
        }
    }

    public void SetBGMSliderValue()
    {
        slider.value = AudioManager.Instance.bgmSource.volume;
    }

    public void SetSFXSliderValue()
    {
        slider.value = AudioManager.Instance.sfxSource.volume;
    }

    public void ChangeVolume()
    {
        switch (sliderType)
        {
            case SliderType.BGM:
                AudioManager.Instance.SetBGMVolume(slider.value);
                break;
            case SliderType.SFX:
                AudioManager.Instance.SetSFXVolume(slider.value);
                break;
        }
        
    }
}
