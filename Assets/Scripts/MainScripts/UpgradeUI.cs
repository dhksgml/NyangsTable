
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [Header("Update Fail Text")]
    [SerializeField] private TextMeshProUGUI updateFailText;

    [Header("Level")]
    [SerializeField] private TextMeshProUGUI totalGoldTMP;
    [SerializeField] private TextMeshProUGUI workerCountLevel;
    [SerializeField] private TextMeshProUGUI workerSpeedLevel;
    [SerializeField] private TextMeshProUGUI workStationCountLevel;
    [SerializeField] private TextMeshProUGUI productionSpeedLevel;
    [SerializeField] private TextMeshProUGUI productPriceLevel;

    [Header("Cost")]
    [SerializeField] private TextMeshProUGUI workerCountUpgradeCost;
    [SerializeField] private TextMeshProUGUI workerSpeedUpgradeCost;
    [SerializeField] private TextMeshProUGUI workStationCountCost;
    [SerializeField] private TextMeshProUGUI productionSpeedUpgradeCost;
    [SerializeField] private TextMeshProUGUI productPriceUpgradeCost;

    [Header("Max Level Upgrade Fail Text")]
    [SerializeField] private string maxLevelUpgradeFailStr = "MAX";

    private BaseUpgrade baseUpgrade;

    private void Start()
    {
        baseUpgrade = FindObjectOfType<BaseUpgrade>();
        UpdateUI();
    }
    public void UpdateUI()
    {
        workerCountLevel.text = (baseUpgrade.CurrentWorkerLevel == baseUpgrade.maxWorkerLevel) ? maxLevelUpgradeFailStr : baseUpgrade.CurrentWorkerLevel.ToString();
        workStationCountLevel.text = (baseUpgrade.CurrentWorkStationLevel == baseUpgrade.maxWorkStationLevel) ? maxLevelUpgradeFailStr : baseUpgrade.CurrentWorkStationLevel.ToString();
        productionSpeedLevel.text = (baseUpgrade.CurrentUpgradeProductionTimeLevel == baseUpgrade.maxUpgradeProductionTimeLevel) ? maxLevelUpgradeFailStr : baseUpgrade.CurrentUpgradeProductionTimeLevel.ToString();
        productPriceLevel.text = (baseUpgrade.CurrentUpgradeProductPriceLevel == baseUpgrade.maxUpgradeProductPriceLevel) ? maxLevelUpgradeFailStr : baseUpgrade.CurrentUpgradeProductPriceLevel.ToString();
        workerSpeedLevel.text = (baseUpgrade.CurrentWorkerSpeedLevel == baseUpgrade.maxWorkerSpeedLevel) ? maxLevelUpgradeFailStr : baseUpgrade.CurrentWorkerSpeedLevel.ToString();

        workerCountUpgradeCost.text = (baseUpgrade.CurrentWorkerLevel == baseUpgrade.maxWorkerLevel) ? maxLevelUpgradeFailStr : baseUpgrade.BuyWorkerCost.ToString();
        workStationCountCost.text = (baseUpgrade.CurrentWorkStationLevel == baseUpgrade.maxWorkStationLevel) ? maxLevelUpgradeFailStr : baseUpgrade.BuyWorkStationCost.ToString();
        productionSpeedUpgradeCost.text = (baseUpgrade.CurrentUpgradeProductionTimeLevel == baseUpgrade.maxUpgradeProductionTimeLevel) ? maxLevelUpgradeFailStr : baseUpgrade.UpgradeProductionTimeCost.ToString();
        productPriceUpgradeCost.text = (baseUpgrade.CurrentUpgradeProductPriceLevel == baseUpgrade.maxUpgradeProductPriceLevel) ? maxLevelUpgradeFailStr : baseUpgrade.UpgradeProductPriceCost.ToString();
        workerSpeedUpgradeCost.text = (baseUpgrade.CurrentWorkerSpeedLevel == baseUpgrade.maxWorkerSpeedLevel) ? maxLevelUpgradeFailStr : baseUpgrade.UpgradeWorkerSpeedCost.ToString();
    }

    public IEnumerator UpdateFailUI(string text)
    {
        updateFailText.text = text;
        yield return new WaitForSeconds(1f);
        updateFailText.text = "";
    }
    
    
    //private void Update()
    //{
    //    totalGoldTMP.text = $"Total Gold: {GoldManager.Instance.CurrentGold}";
    //}

}
