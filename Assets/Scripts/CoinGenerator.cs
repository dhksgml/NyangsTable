using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public int coinPerInterval = 1;           // 획득할 코인 수
    public float interval = 2f;               // 몇 초마다?
    public AudioClip coinClip;                // 코인 소리
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(GenerateCoin());
    }

    IEnumerator GenerateCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            AddCoin(coinPerInterval);
        }
    }

    void AddCoin(int amount)
    {
        // 코인 수를 증가시키는 로직 (UI 연동하는 경우도 여기에)
        Game.Instance.AddCoin(amount);

        // 효과음 재생
        if (coinClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(coinClip);
        }
    }
}
