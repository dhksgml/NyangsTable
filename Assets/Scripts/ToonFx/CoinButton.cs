using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinButton : MonoBehaviour
{
    public int goldAmount = 1;
    public Transform effectTarget; // 골드바나 버튼 위 등 원하는 위치
    public GameObject coinEffectPrefab; // 이펙트 프리팹

    public void OnClickAddGold()
    {
        // 골드 증가
        //GoldManager.Instance.AddGold(goldAmount);

        // 이펙트 생성
        if (coinEffectPrefab != null && effectTarget != null)
        {
            GameObject effect = Instantiate(coinEffectPrefab, effectTarget.position, Quaternion.identity, effectTarget);
            Destroy(effect, 1.5f); // 1.5초 후 이펙트 제거
        }

    }
}
