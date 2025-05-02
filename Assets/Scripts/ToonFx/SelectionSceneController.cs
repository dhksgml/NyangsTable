using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionSceneController : MonoBehaviour
{
    // 게임 시작 버튼 클릭 시 호출되는 메서드
    public void StartGame()
    {
        // 로딩 씬으로 전환
        SceneManager.LoadScene("LoadingScene");

        // 4초 후에 스테이지 씬으로 넘어가기
        Invoke("LoadStage", 4f);
    }

    // 실제 스테이지 씬을 로드하는 메서드
    void LoadStage()
    {
        SceneManager.LoadScene("Stage0");
    }
}