using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionSceneController : MonoBehaviour
{
    // ���� ���� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void StartGame()
    {
        // �ε� ������ ��ȯ
        SceneManager.LoadScene("LoadingScene");

        // 4�� �Ŀ� �������� ������ �Ѿ��
        Invoke("LoadStage", 4f);
    }

    // ���� �������� ���� �ε��ϴ� �޼���
    void LoadStage()
    {
        SceneManager.LoadScene("Stage0");
    }
}