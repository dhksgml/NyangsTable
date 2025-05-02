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
            Debug.LogWarning("UIEffectSpawner: 프리팹 또는 캔버스가 할당되지 않았습니다.");
            return;
        }

        GameObject effect = Instantiate(coinEffectPrefab, canvasTransform);
        effect.transform.position = screenPosition;
    }
}