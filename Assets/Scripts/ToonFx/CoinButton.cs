using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinButton : MonoBehaviour
{
    public int goldAmount = 1;
    public Transform effectTarget; // ���ٳ� ��ư �� �� ���ϴ� ��ġ
    public GameObject coinEffectPrefab; // ����Ʈ ������

    public void OnClickAddGold()
    {
        // ��� ����
        //GoldManager.Instance.AddGold(goldAmount);

        // ����Ʈ ����
        if (coinEffectPrefab != null && effectTarget != null)
        {
            GameObject effect = Instantiate(coinEffectPrefab, effectTarget.position, Quaternion.identity, effectTarget);
            Destroy(effect, 1.5f); // 1.5�� �� ����Ʈ ����
        }

    }
}
