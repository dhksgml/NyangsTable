using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextButtonHandler : MonoBehaviour
{
    public string message;
    public Transform worldPosition;

    public FloatingTextManager floatingTextManager;

    public void Show()
    {
        floatingTextManager.ShowFloatingText(message, worldPosition.position);
    }
}
