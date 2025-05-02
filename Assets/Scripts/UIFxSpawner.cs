using UnityEngine;
using UnityEngine.UI;

public class UIFxSpawner : MonoBehaviour
{
    public ParticleSystem fxPrefab;        // ���� FX ������
    public RectTransform targetUI;         // ������ ���� UI ���
    public AudioSource audioSource;

    void SpawnFX()
    {
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetUI.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 5f)); // Z �� ����

        Instantiate(fxPrefab, worldPos, Quaternion.identity);
        audioSource.Play();
    }
}
