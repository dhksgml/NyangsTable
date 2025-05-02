using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinButtonSound : MonoBehaviour
{
    public void PlayCoinSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayCoinSFX();
        }
    }
}