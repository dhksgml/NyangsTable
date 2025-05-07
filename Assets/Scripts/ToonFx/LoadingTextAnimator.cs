using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingTextAnimator : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public string fullText = "Loading...";
    public float delay = 0.2f; // 글자 하나씩 나타나는 속도

    void Start()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        while (true)
        {
            loadingText.text = "";
            for (int i = 0; i <= fullText.Length; i++)
            {
                loadingText.text = fullText.Substring(0, i);
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(0.5f); // 다 나오고 잠깐 멈춤
        }
    }
}