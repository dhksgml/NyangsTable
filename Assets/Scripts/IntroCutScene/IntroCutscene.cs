using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroCutscene : MonoBehaviour
{
    public CanvasGroup logoGroup;
    public float fadeDuration = 1.5f;
    public float logoDisplayTime = 2f;
    public string nextSceneName = "TitleScene";  // 메인 메뉴 씬 이름

    private void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        // 페이드 인
        yield return StartCoroutine(Fade(logoGroup, 0f, 1f));

        // 로고 유지
        yield return new WaitForSeconds(logoDisplayTime);

        // 페이드 아웃
        yield return StartCoroutine(Fade(logoGroup, 1f, 0f));

        // 메인 메뉴 씬으로 전환
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
