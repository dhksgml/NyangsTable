using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearButton : MonoBehaviour
{
    public void OnClearButtonPressed()
    {
        LevelSelectMenu.Instance.ClearStage();
        GameManager.Instance.ResetGame();
    }
}
