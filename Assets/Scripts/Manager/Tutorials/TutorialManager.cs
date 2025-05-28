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
                tutorialText.text = "냥이의 식탁에 오신 걸 환영합니다!";
                Time.timeScale = 0f;
                break;
            case TutorialStep.ClickMoney:
                tutorialText.text = "냥이를 눌러 돈을 벌 수 있습니다!";
                highlightClickArea.SetActive(true);
                break;
            case TutorialStep.ClickMoney2:
                tutorialText.text = "목표 골드를 채우면 다음 스테이지가 해금돼요!";
                highlightGoalGoldArea.SetActive(true);
                break;
            case TutorialStep.BuyUpgrade:
                tutorialText.text = "이곳을 눌러 업그레이드를 할 수 있습니다!";
                highlightClickArea.SetActive(false);
                highlightUpgradeButton.SetActive(true);
                break;
            case TutorialStep.BuyUpgrade2:
                tutorialText.text = "클릭하여 장사를 시작해보세요!";
                break;
            case TutorialStep.Done:
                tutorialCanvas.SetActive(false);
                Owner.IsTutorialMode = false;
                Time.timeScale = 1f;
                break;
        }
    }
}
