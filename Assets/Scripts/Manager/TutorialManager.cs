using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TutorialStep
{
    Welcome,
    ClickMoney,
    ClickMoney2,
    BuyUpgrade,
    BuyUpgrade2,
    Done
}

public class TutorialManager : MonoBehaviour
{
    public TutorialStep currentStep;
    public GameObject highlightClickArea;
    public GameObject highlightGoalGoldArea;
    public GameObject highlightUpgradeButton;
    public GameObject tutorialCanvas;
    public TextMeshProUGUI tutorialText;

    void Start()
    {
        Owner.IsTutorialMode = true;
        currentStep = TutorialStep.Welcome;
        ShowStep(currentStep);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            NextStep();
        }
    }

    public void OnPlayerClickMoney()
    {
        if (currentStep == TutorialStep.ClickMoney)
        {
            NextStep();
        }
    }

    public void OnPlayerBuyUpgrade()
    {
        if (currentStep == TutorialStep.BuyUpgrade)
        {
            NextStep();
        }
    }

    void NextStep()
    {
        currentStep++;
        ShowStep(currentStep);
    }

    void ShowStep(TutorialStep step)
    {
        highlightClickArea.SetActive(false);
        highlightUpgradeButton.SetActive(false);
        highlightGoalGoldArea.SetActive(false);

        switch (step)
        {
            case TutorialStep.Welcome:
                tutorialText.text = "������ ��Ź�� ���� �� ȯ���մϴ�!";
                Time.timeScale = 0f;
                break;
            case TutorialStep.ClickMoney:
                tutorialText.text = "���̸� ���� ���� �� �� �ֽ��ϴ�!";
                highlightClickArea.SetActive(true);
                break;
            case TutorialStep.ClickMoney2:
                tutorialText.text = "��ǥ ��带 ä��� ���� ���������� �رݵſ�!";
                highlightGoalGoldArea.SetActive(true);
                break;
            case TutorialStep.BuyUpgrade:
                tutorialText.text = "�̰��� ���� ���׷��̵带 �� �� �ֽ��ϴ�!";
                highlightClickArea.SetActive(false);
                highlightUpgradeButton.SetActive(true);
                break;
            case TutorialStep.BuyUpgrade2:
                tutorialText.text = "Ŭ���Ͽ� ��縦 �����غ�����!";
                break;
            case TutorialStep.Done:
                tutorialCanvas.SetActive(false);
                Owner.IsTutorialMode = false;
                Time.timeScale = 1f;
                break;
        }
    }
}
