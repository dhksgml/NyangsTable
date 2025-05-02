using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; // ����Ƽ �����Ϳ��� ������ �� �ʿ�
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isGameEnded = false;
    public bool IsGameEnded => isGameEnded;

    public static GameData gameData = new GameData();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            gameData.UnlockLevel = PlayerPrefs.GetInt("unLockStage");
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    public void StartGame()
    {
        Debug.Log("Game Started");
    }

    public void EndGame()
    {
        isGameEnded = true;
    }

    public void ResetGame()
    {
        //Debug.Log("NextStage scene load..");
        isGameEnded = false;
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");



#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // ����Ƽ �����Ϳ��� ���� ����
#else
        Application.Quit(); // ����� ���ӿ����� ���� ����
#endif
    }

    public void PauseGame()
    {
        Debug.Log("Game Paused");
    }

    public GameData GetGameData()
    {
        return gameData;
    }

    public void ResetGameData()
    {
        gameData.UnlockLevel = 1;
    }

    private void OnApplicationQuit()
    {
        //�� ���� ��, �ر� �������� ����
        PlayerPrefs.SetInt("unLockStage", gameData.UnlockLevel);
        PlayerPrefs.Save();
    }
}
