using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class NumberCounter : MonoBehaviour
{
    //[SerializeField] private TMP_Text numberText;
    [SerializeField] private float current, target;

    private Tween goldTween;

    private void Start()
    {
        StopDisplay();
        current = 0;
        target = GoldManager.Instance.CurrentGold;
        Display();
    }


    public void Display()
    {
        goldTween = DOVirtual.Float(current, target, 0.3f, (x) =>
        {
            current = x;

            if(UI.Instance != null)
                UI.Instance.UpdateGoldText(current);

        }).OnComplete(() => {
            if (!GameManager.Instance.IsGameEnded)
            {
                Display();
            }
            else
            {
                StopDisplay();
            }
        });
    }

    public void StopDisplay()
    {
        goldTween?.Kill();
    }

    public void SetTargetGold(float value)
    {
        target = value;
    }

    private void OnDestroy()
    {
        StopDisplay();
    }
}
