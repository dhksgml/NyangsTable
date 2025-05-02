using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; // 유니티 에디터에서 종료할 때 필요
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
        EditorApplication.isPlaying = false; // 유니티 에디터에서 실행 중지
#else
        Application.Quit(); // 빌드된 게임에서는 정상 종료
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
        //앱 나갈 때, 해금 스테이지 저장
        PlayerPrefs.SetInt("unLockStage", gameData.UnlockLevel);
        PlayerPrefs.Save();
    }
}
