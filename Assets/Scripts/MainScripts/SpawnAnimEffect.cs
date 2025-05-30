using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnAnimEffect : MonoBehaviour
{
    [SerializeField] private GameObject particle;

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        Instantiate(particle, transform);
    }

    public void SpawnParticle(Vector3 pos)
    {
        Instantiate(particle, pos, Quaternion.identity);
    }
}
