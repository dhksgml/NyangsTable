using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1f;

    // 씬을 덮어주는 어두워지는 효과
    public IEnumerator FadeOut()
    {
        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;

        while (fadeImage.color.a < 1f)
        {
            c.a += Time.deltaTime * fadeSpeed;
            fadeImage.color = c;
            yield return null;
        }
    }

    // 씬 밝아지는 효과
    public IEnumerator FadeIn()
    {
        Color c = fadeImage.color;
        c.a = 1f;
        fadeImage.color = c;

        while (fadeImage.color.a > 0f)
        {
            c.a -= Time.deltaTime * fadeSpeed;
            fadeImage.color = c;
            yield return null;
        }
    }
}