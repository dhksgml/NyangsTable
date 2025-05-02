using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public int coinPerInterval = 1;           // ȹ���� ���� ��
    public float interval = 2f;               // �� �ʸ���?
    public AudioClip coinClip;                // ���� �Ҹ�
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
        // ���� ���� ������Ű�� ���� (UI �����ϴ� ��쵵 ���⿡)
        Game.Instance.AddCoin(amount);

        // ȿ���� ���
        if (coinClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(coinClip);
        }
    }
}
