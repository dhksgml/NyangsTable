using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScaleEffect : MonoBehaviour
{
    public float scaleFactor = 1.5f; // 크기를 키울 비율
    public float duration = 0.2f;    // 크기 변화에 걸리는 시간

    public void ScaleUpAndDown()
    {
        transform.DOScale(scaleFactor, duration)
            .SetEase(Ease.OutQuad) // 부드러운 확대 효과
            .OnComplete(() =>
                transform.DOScale(1f, duration)
                    .SetEase(Ease.InQuad) // 부드러운 축소 효과
            );
    }
}
