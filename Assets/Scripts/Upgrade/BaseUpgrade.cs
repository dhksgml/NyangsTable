using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BaseUpgrade : MonoBehaviour
{
    [Header("Max Levels")]
    public int maxWorkerLevel = 6;
    public int maxWorkerSpeedLevel = 6;
    public int maxWorkStationLevel = 4;
    public int maxUpgradeProductionTimeLevel = 6;
    public int maxUpgradeProductPriceLevel = 6;

    [Header("Upgrades")]
    [SerializeField] private float upgradeProductionTimeMultiplier = 0.8f;
    [SerializeField] private float upgradeProductPriceMultiplier = 1.5f;
    [SerializeField] private float upgradeWorkerSpeed = 10f;

    [Header("Cost Multiplier")]
    [SerializeField] private float buyWorkerCostMultiplier = 2;
    [SerializeField] private float buyWorkeStationCostMultiplier = 2;
    [SerializeField] private float upgradeProductionTimeCostMultiplier = 2;
    [SerializeField] private float upgradeProductPriceCostMultiplier = 2;
    [SerializeField] private float upgradeWorkerSpeedCostMultiplier = 2;

    [Header("Cost")]
    [SerializeField] private int buyWorkerCost = 100;
    [SerializeField] private int buyWorkeStationCost = 1000;
    [SerializeField] private int upgradeProductionTimeCost =100;
    [SerializeField] private int upgradeProductPriceCost =100;

    [SerializeField] private int upgradeWorkerSpeedCost =100;

    private Coroutine currentCoroutine;
    private UpgradeUI ui;

    public int CurrentWorkerLevel { get; set; } = 1;
    public int CurrentWorkerSpeedLevel { get; set; } = 1;
    public int CurrentWorkStationLevel { get; set; } = 1;
    public int CurrentUpgradeProductionTimeLevel { get; set; } = 1;
    public int CurrentUpgradeProductPriceLevel { get; set; } = 1;
    

    //access from UpgradeUI
    public int BuyWorkerCost => buyWorkerCost;
    public int BuyWorkStationCost => buyWorkeStationCost;
    public int UpgradeProductPriceCost => upgradeProductPriceCost;
    public int UpgradeProductionTimeCost => upgradeProductionTimeCost;
    public int UpgradeWorkerSpeedCost => upgradeWorkerSpeedCost;


    public class UpgradeData
    {
        public int Cost { get; set; }
        public float CostMultiplier { get; set; }
        public Action UpdateLevelAction { get; set; }
        public Action<int> UpdateCostAction { get; set; }

        public UpgradeData(int cost, float costMultiplier, Action updateLevelAction, Action<int> updateCostAction)
        {
            Cost = cost;
            CostMultiplier = costMultiplier;
            UpdateLevelAction = updateLevelAction;
            UpdateCostAction = updateCostAction;
        }
    }

    private void Start()
    {
        ui = FindObjectOfType<UpgradeUI>();
    }


    // 골드 충분할 때만 업그레이드 시도
    private void TryUpgrade(int cost, float multiplier, Action levelUpAction, Action<int> updateCostAction, Action upgradeAction)
    {
        if (GoldManager.Instance.CurrentGold < cost)
        {
            Debug.Log("Cost 부족");
            if(currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(ui.UpdateFailUI("Not enough Money!"));
            return; 
        }
        
        UpgradeData upgradeData = new UpgradeData(cost, multiplier, levelUpAction, updateCostAction);

        GoldManager.Instance.RemoveGold(upgradeData.Cost);

        upgradeData.UpdateLevelAction.Invoke();
        upgradeAction.Invoke();

        updateCostAction.Invoke(upgradeData.Cost);

        ui.UpdateUI();

    }

    public void BuyWorker()
    {
        if(CurrentWorkerLevel >= maxWorkerLevel)
        {
            Debug.Log("max worker Level");
            currentCoroutine = StartCoroutine(ui.UpdateFailUI("Max Level"));
            return;
        }

        TryUpgrade(buyWorkerCost, buyWorkerCostMultiplier,
        () => CurrentWorkerLevel++, UpdateWorkerCost,
        () => WorkerManager.Instance.CreateWorker());
    }

    public void BuyWorkStation()
    {
        if (CurrentWorkStationLevel >= maxWorkStationLevel)
        {
            Debug.Log("max WorkStation Level");
            currentCoroutine = StartCoroutine(ui.UpdateFailUI("Max Level"));
            return;
        }

        TryUpgrade(buyWorkeStationCost, buyWorkeStationCostMultiplier,
        () => CurrentWorkStationLevel++, UpdateWorkStationCost,
        () => WorkSpaceManager.Instance.CreateWorkstation());
    }

    
    public void UpgradeProductPrice()
    {
        if (CurrentUpgradeProductPriceLevel >= maxUpgradeProductPriceLevel)
        {
            Debug.Log("max ProductPrice Level");
            currentCoroutine = StartCoroutine(ui.UpdateFailUI("Max Level"));
            return;
        }

        TryUpgrade(upgradeProductPriceCost, upgradeProductPriceCostMultiplier,
        () => CurrentUpgradeProductPriceLevel++, UpdateProductPriceCost,
        () => WorkSpaceManager.Instance.Product.ProductPrice *= upgradeProductPriceMultiplier);
    }
   
    public void UpgradeProductSpeed()
    {
        if (CurrentUpgradeProductionTimeLevel >= maxUpgradeProductionTimeLevel)
        {
            Debug.Log("max product speed Level");
            currentCoroutine = StartCoroutine(ui.UpdateFailUI("Max Level"));
            return;

        }
        TryUpgrade(upgradeProductionTimeCost, upgradeProductionTimeCostMultiplier,
        () => CurrentUpgradeProductionTimeLevel++, UpdateProductionTimeCost,
        () => WorkSpaceManager.Instance.Product.ProductionTime *= upgradeProductionTimeMultiplier);
    }

    public void UpgradeWorkerSpeed()
    {
        if (CurrentWorkerSpeedLevel >= maxWorkerSpeedLevel)
        {
            Debug.Log("max Worker Speed Level");
            currentCoroutine = StartCoroutine(ui.UpdateFailUI("Max Level"));
            return;

        }

        TryUpgrade(upgradeWorkerSpeedCost, upgradeWorkerSpeedCostMultiplier,
        () => CurrentWorkerSpeedLevel++, UpdateWorkerSpeedCost,
        () =>
        {
            WorkerManager.Instance.UpgradeSpeed(upgradeWorkerSpeed);
        });
    }

    //가격 계산
    private void UpdateWorkerCost(int cost) => buyWorkerCost = (int)(cost * buyWorkerCostMultiplier);
    private void UpdateWorkStationCost(int cost) => buyWorkeStationCost = (int)(cost * buyWorkeStationCostMultiplier);
    private void UpdateProductPriceCost(int cost) => upgradeProductPriceCost = (int)(cost * upgradeProductPriceCostMultiplier);
    private void UpdateProductionTimeCost(int cost) => upgradeProductionTimeCost = (int)(cost * upgradeProductionTimeCostMultiplier);
    private void UpdateWorkerSpeedCost(int cost) => upgradeWorkerSpeedCost = (int)(cost * upgradeWorkerSpeedCostMultiplier);
}
