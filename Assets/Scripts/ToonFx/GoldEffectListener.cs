using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldEffectListener : MonoBehaviour
{
    public UIEffectSpawner effectSpawner;

    private void OnEnable()
    {
        GoldManager.OnGoldEffect += HandleGoldEffect;
    }

    private void OnDisable()
    {
        GoldManager.OnGoldEffect -= HandleGoldEffect;
    }

    void HandleGoldEffect(Transform spawnTarget)
    {
        Vector3 screenPos = new Vector3(Screen.width / 2f, Screen.height - 200f, 0f);
        effectSpawner.SpawnCoinEffectAt(screenPos);

        void Start()
        {
            Destroy(gameObject, 1.5f); // 1.5초 뒤 자동 제거
        }
    }
}