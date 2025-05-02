using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScaleEffect : MonoBehaviour
{
    public float scaleFactor = 1.5f; // ũ�⸦ Ű�� ����
    public float duration = 0.2f;    // ũ�� ��ȭ�� �ɸ��� �ð�

    public void ScaleUpAndDown()
    {
        transform.DOScale(scaleFactor, duration)
            .SetEase(Ease.OutQuad) // �ε巯�� Ȯ�� ȿ��
            .OnComplete(() =>
                transform.DOScale(1f, duration)
                    .SetEase(Ease.InQuad) // �ε巯�� ��� ȿ��
            );
    }
}
