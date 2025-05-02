using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypingLoadingText : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public string fullText = "·Îµù";
    public float delay = 0.5f;

    void Start()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        loadingText.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            loadingText.text += fullText[i];
            yield return new WaitForSeconds(delay);
        }
    }
}