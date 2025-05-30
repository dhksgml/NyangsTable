using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundScaler : MonoBehaviour
{
    public Transform background;
    public float duration = 1f;

    void Start()
    {
        Vector3 baseScale = background.localScale;
        Vector3 targetScale = baseScale * 1.1f;

        // 부드럽게 크기 커졌다가 원래대로 돌아오는 애니메이션 (Loop)
        background.DOScale(targetScale, duration)
                  .SetEase(Ease.InOutSine)
                  .SetLoops(-1, LoopType.Yoyo);
    }
}
