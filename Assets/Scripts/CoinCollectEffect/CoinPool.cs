using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public static CoinPool Instance;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int initialSize = 20;
    private Queue<GameObject> coinPool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        for(int i = 0; i < initialSize; i++)
        {
            var coin = Instantiate(coinPrefab, transform);
            coin.SetActive(false);
            coinPool.Enqueue(coin);
        }
    }

    public GameObject GetCoin()
    {
        if(coinPool.Count > 0)
        {
            var coin = coinPool.Dequeue();
            coin.SetActive(true);
            return coin;
        }
        else
        {
            var coin = Instantiate(coinPrefab, transform);
            return coin;
        }
    }

    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
        coinPool.Enqueue(coin);
    }    
}
