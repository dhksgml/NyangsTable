using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }

    [SerializeField] private TMP_Text uiTotalGoldText;
    [SerializeField] private GameObject uiNextStageButton;
    [SerializeField] private GameObject uiUpgradeCanvas;
    [SerializeField] private Slider uiGoalGoldBar;
    private bool isUIActive = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GameManager.Instance.ResetGame();

        uiNextStageButton.SetActive(false);
        uiUpgradeCanvas.SetActive(isUIActive);
    }

    public void ToggleUI()
    {
        isUIActive = !isUIActive;
        uiUpgradeCanvas.SetActive(isUIActive);
    }


    //문제 발생!
    private void Update()
    {
        if(GameManager.Instance.IsGameEnded)
        {
            uiNextStageButton.SetActive(true);
        }

        if(!GameManager.Instance.IsGameEnded)
        { 
            uiGoalGoldBar.value = GoldManager.Instance.CalculateGoalGold(); 
        }
        
    }

    public void UpdateGoalGoldSlider()
    {
        uiGoalGoldBar.value = GoldManager.Instance.CalculateGoalGold();
    }

    public void UpdateGoldText(float current)
    {
        uiTotalGoldText.text = "TOTAL GOLD : " + Math.Round(current);
    }

}
