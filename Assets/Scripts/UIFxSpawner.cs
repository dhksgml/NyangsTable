using UnityEngine;
using UnityEngine.UI;

public class UIFxSpawner : MonoBehaviour
{
    public ParticleSystem fxPrefab;        // 气磷 FX 橇府普
    public RectTransform targetUI;         // 气磷捞 磐龙 UI 夸家
    public AudioSource audioSource;

    void SpawnFX()
    {
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetUI.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 5f)); // Z 蔼 林狼

        Instantiate(fxPrefab, worldPos, Quaternion.identity);
        audioSource.Play();
    }
}
