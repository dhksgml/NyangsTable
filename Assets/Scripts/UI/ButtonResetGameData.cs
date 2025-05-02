using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonResetGameData : MonoBehaviour
{
    public void ResetGameData()
    {
        GameManager.Instance.ResetGameData();
    }
}
