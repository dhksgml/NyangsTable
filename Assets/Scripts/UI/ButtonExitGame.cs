using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExitGame : MonoBehaviour
{
    public void ExitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
