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
        // 여기에 UI 갱신도 추가 가능
        Debug.Log("코인 획득! 현재 코인: " + coin);
    }
}
