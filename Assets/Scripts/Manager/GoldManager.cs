using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : Singleton<GoldManager>
{
    [SerializeField] private int testGold = 10000;
    [SerializeField] private int goalGold = 10100;
    [SerializeField] private RectTransform goldBar; // 골드바 슬라이더 UI

    private int currentGold;
    private readonly string GOLD_KEY = "GoldKey";
    public int CurrentGold => currentGold;


    [SerializeField] private UIEffectSpawner uiEffectSpawner;

    private void Start()
    {
        AddGold(testGold);
        LoadGold();
    }

    private void Update()
    {
        CheckGold();
    }

    private void LoadGold()
    {
        currentGold = PlayerPrefs.GetInt(GOLD_KEY, testGold);
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        PlayerPrefs.SetInt(GOLD_KEY, currentGold);
        PlayerPrefs.Save();

        if (uiEffectSpawner != null)
        {
            // 골드바 UI 위치 기준으로 오른쪽 끝 위치 구하기
            RectTransform goldBarRect = goldBar.GetComponent<RectTransform>();
            Vector3 worldPos = goldBarRect.position + new Vector3(goldBarRect.rect.width * goldBarRect.lossyScale.x / 2f, 0f, 0f);
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);

            // 이펙트 제거
            // if (uiEffectSpawner != null)
            // {
            //     Vector3 screenPos = new Vector3(Screen.width / 2f, 150f, 0f);
            //     uiEffectSpawner.SpawnCoinEffectAt(screenPos);
            // }

        }
    }

    public void RemoveGold(int amount)
    {
        currentGold -= amount;
        PlayerPrefs.SetInt(GOLD_KEY, currentGold);
        PlayerPrefs.Save();
    }

    public void CheckGold()
    {
        if (currentGold >= goalGold)
        {
            GameManager.Instance.EndGame();
        }
    }

    public float CalculateGoalGold()
    {
        return (float)currentGold / goalGold;
    }

    public delegate void GoldEffectHandler(Transform spawnTarget);
    public static event GoldEffectHandler OnGoldEffect;

    public static void TriggerGoldEffect(Transform target)
    {
        OnGoldEffect?.Invoke(target);
    }
}