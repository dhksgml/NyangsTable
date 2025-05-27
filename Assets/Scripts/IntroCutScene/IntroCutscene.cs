using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroCutscene : MonoBehaviour
{
    public CanvasGroup logoGroup;
    public float fadeDuration = 1.5f;
    public float logoDisplayTime = 2f;
    public string nextSceneName = "TitleScene";  // ���� �޴� �� �̸�

    private void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        // ���̵� ��
        yield return StartCoroutine(Fade(logoGroup, 0f, 1f));

        // �ΰ� ����
        yield return new WaitForSeconds(logoDisplayTime);

        // ���̵� �ƿ�
        yield return StartCoroutine(Fade(logoGroup, 1f, 0f));

        // ���� �޴� ������ ��ȯ
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator Fade(CanvasGroup group, float from, float to)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            group.alpha = Mathf.Lerp(from, to, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        group.alpha = to;
    }
}
