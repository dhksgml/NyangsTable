using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Tutorial2Step
{
    Welcome,
    SpawnRanch,
    SpawnRanch1,
    SpawnRanch2,
    Done
}

public class Tutorial2Manager : MonoBehaviour
{
    public Tutorial2Step currentStep;
    public GameObject highlightClickArea;
    public GameObject highlightRoach;
    public GameObject tutorialCanvas;
    public TextMeshProUGUI tutorialText;

    //public List<string> tutorialTexts = new List<string>();

    public bool isClickable;

    void Start()
    {
        Owner.IsTutorialMode = true;
        currentStep = Tutorial2Step.Welcome;
        ShowStep(currentStep);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && isClickable)
        {
            NextStep();
        }
    }

    public void SetClickable()
    {
        isClickable = true;
    }

    void NextStep()
    {
        currentStep++;
        ShowStep(currentStep);
    }

    void ChangeText(string text)
    {
        tutorialText.text = text;
    }

    void ShowStep(Tutorial2Step step)
    {
        highlightClickArea.SetActive(false);
        highlightRoach.SetActive(false);

        switch (step)
        {
            case Tutorial2Step.Welcome:               
                tutorialText.text = "À¸¾Ç!";
                highlightClickArea.SetActive(true);
                
                Time.timeScale = 0f;
                break;
            case Tutorial2Step.SpawnRanch:
                tutorialText.text = "¹ÙÄû¹ú·¹´Ù!";
                highlightRoach.SetActive(true);
                break;
            case Tutorial2Step.SpawnRanch1:
                tutorialText.text = "¹ÙÄû¹ú·¹°¡ ÀÖÀ¸¸é È¹µæÇÏ´Â °ñµå°¡ ÁÙ¾îµì´Ï´Ù.";
                highlightRoach.SetActive(true);
                break;
            case Tutorial2Step.SpawnRanch2:
                tutorialText.text = "¹ÙÄû¹ú·¹´Â Å¬¸¯ÇØ¼­ ¾ø¾Ù ¼ö ÀÖ½À´Ï´Ù.";
                highlightRoach.SetActive(true);
                break;
            case Tutorial2Step.Done:
                tutorialCanvas.SetActive(false);
                Owner.IsTutorialMode = false;
                Time.timeScale = 1f;
                break;
        }
    }
}
