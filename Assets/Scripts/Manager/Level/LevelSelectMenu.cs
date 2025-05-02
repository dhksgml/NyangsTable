using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    public static LevelSelectMenu Instance { get; private set; }

    public int totalLevel = 0;

    public int unlockedLevel = 1;
    private LevelButton[] levelButtons;

    private int currentSelectedLevel;

    private GameData gameData;
    //int levelat = 1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        //levelat = PlayerPrefs.GetInt("levelReached");
        levelButtons = GetComponentsInChildren<LevelButton>();

        if (GameManager.Instance)
            gameData = GameManager.Instance.GetGameData();
        else
            gameData = new GameData();
        unlockedLevel = gameData.UnlockLevel;
        Refresh();
    }

    public void Refresh()
    {
        int index = 0;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (levelButtons[i] == null)
                continue;

            int level = index + i + 1;

            if (level <= totalLevel)
            {
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].Setup(level, level <= unlockedLevel);
            }
            else
            {
                levelButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void ClearStage()
    {
        if (unlockedLevel <= currentSelectedLevel)
        {
            gameData.UnlockLevel = currentSelectedLevel + 1;
            unlockedLevel = gameData.UnlockLevel;
            Refresh();
        }
    }

    public void SelectCurrentLevel(int stageLevel)
    {
        currentSelectedLevel = stageLevel;
    }
}
