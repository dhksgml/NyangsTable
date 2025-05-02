using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public int coin = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        // ���⿡ UI ���ŵ� �߰� ����
        Debug.Log("���� ȹ��! ���� ����: " + coin);
    }
}
