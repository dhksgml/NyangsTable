using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CutSceneObject : MonoBehaviour
{
    [SerializeField] private Ease ease;
    [SerializeField] private Vector3 targetScale;
    private void Start()
    {
        transform.DOScale(targetScale, 1).SetEase(ease);
    }
}
