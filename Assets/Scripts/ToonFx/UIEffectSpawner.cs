using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffectSpawner : MonoBehaviour
{
    public RectTransform canvasTransform;
    public GameObject coinEffectPrefab;

    public void SpawnCoinEffectAt(Vector3 screenPosition)
    {
        if (coinEffectPrefab == null || canvasTransform == null)
        {
            Debug.LogWarning("UIEffectSpawner: ������ �Ǵ� ĵ������ �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        GameObject effect = Instantiate(coinEffectPrefab, canvasTransform);
        effect.transform.position = screenPosition;
    }
}