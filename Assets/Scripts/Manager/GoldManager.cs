using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

    [SerializeField] private int testGold = 100;
    [SerializeField] private int goalGold = 10100;
    [SerializeField] private RectTransform goldBar; // 골드바 슬라이더 UI

    private int currentGold;
    //private readonly string GOLD_KEY = "GoldKey";
    public int CurrentGold => currentGold;


    [SerializeField] private UIEffectSpawner uiEffectSpawner;

    public float maxReductionRate = 0.5f; //Roach 수만큼 획득 골드 감소 패널티
    public int maxRoachCountForPenalty = 5;
    public int minGold = 1;
    
    RandomPrefabSpawner randomPrefabSpawner;

    NumberCounter numberCounter;

    public event Action<int, Vector3> OnGoldAdded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    private void Start()
    {
        //AddGold(testGold);
        //LoadGold();

        randomPrefabSpawner = FindObjectOfType<RandomPrefabSpawner>();
        numberCounter = FindObjectOfType<NumberCounter>();
    }

    private void Update()
    {
        CheckGold();
    }

    //private void LoadGold()
    //{
    //    currentGold = PlayerPrefs.GetInt(GOLD_KEY, testGold);
    //}

    

    
    public void AddGold(int amount)
    {
        if (randomPrefabSpawner != null)
        {
            int roachCount = randomPrefabSpawner.GetRouchCount();
            float ratio = roachCount / (float)maxRoachCountForPenalty;
            float penalty = Mathf.Clamp01((Mathf.Sqrt(ratio) * maxReductionRate));
            int finalGold = Mathf.Max(minGold, Mathf.RoundToInt(amount * (1f - penalty)));

            Debug.Log($"벌레 수: {roachCount}, 보정률: {penalty}, 원금: {amount}, 최종 지급: {finalGold}");


            currentGold += finalGold;
            //ShowFloatingText(finalGold.ToString());
        }
        else
        {
            currentGold += amount;
            //ShowFloatingText(amount.ToString());
        }
            

        //PlayerPrefs.SetInt(GOLD_KEY, currentGold);
        //PlayerPrefs.Save();

        if(numberCounter != null)
            numberCounter.SetTargetGold(currentGold);

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
    

    public void AddGold(int amount, Vector3 worldPosition)
    {

        int finalGold = amount;

        if (randomPrefabSpawner != null)
        {
            int roachCount = randomPrefabSpawner.GetRouchCount();
            float ratio = roachCount / (float)maxRoachCountForPenalty;
            float penalty = Mathf.Clamp01((Mathf.Sqrt(ratio) * maxReductionRate));
            finalGold = Mathf.Max(minGold, Mathf.RoundToInt(amount * (1f - penalty)));
        }

        currentGold += finalGold;

        if (numberCounter != null)
            numberCounter.SetTargetGold(currentGold);

        // ✅ 이벤트 호출
        OnGoldAdded?.Invoke(finalGold, worldPosition);
    }

    public void RemoveGold(int amount)
    {
        currentGold -= amount;
        //PlayerPrefs.SetInt(GOLD_KEY, currentGold);
        //PlayerPrefs.Save();

        if (numberCounter != null)
            numberCounter.SetTargetGold(currentGold);
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