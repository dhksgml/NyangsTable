using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour
{
    public void LoadStage1()
    {
        SceneManager.LoadScene("Stage1"); // 정확한 이름 사용
    }
}