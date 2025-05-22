using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectEffect : MonoBehaviour
{
    public Transform coinParent;
    public Transform coinStart;
    public Transform coinEnd;
    public float moveDuration;
    public Ease moveEase;

    public int coinAmount;
    public float coinPerDelay;

    public void OnGetButtonClicked()
    {
        for(int i = 0; i < coinAmount; i++)
        {
            var targetDelay = i * coinPerDelay;
            ShowCoin(targetDelay);
        }
    }

    public void ShowCoin(float delay, Vector3 coinStartPosition)
    {
        var coinObject = CoinPool.Instance.GetCoin();
        coinObject.transform.SetParent(coinParent);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(coinStartPosition);
        coinObject.transform.position = screenPos;

        Debug.Log(coinObject.transform.position);

        var offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), 0f);
        var startPos = offset + coinStartPosition;
        coinObject.transform.position = startPos;
        coinObject.transform.DOMove(coinEnd.position, moveDuration)
            .SetEase(moveEase)
            .SetDelay(delay)
            .OnComplete(() =>
            {
                CoinPool.Instance.ReturnCoin(coinObject);
            });
    }

    public void ShowCoin(float delay)
    {
        var coinObject = CoinPool.Instance.GetCoin();
        coinObject.transform.SetParent(coinParent);

        var offset = new Vector3(Random.Range(-100f, 100f), Random.Range(-100, 100f), 0f);
        var startPos = offset + coinStart.transform.position;
        coinObject.transform.position = startPos;
        coinObject.transform.DOMove(coinEnd.position, moveDuration)
            .SetEase(moveEase)
            .SetDelay(delay)
            .OnComplete(()=>
            {
                CoinPool.Instance.ReturnCoin(coinObject);
            });
    }
}
