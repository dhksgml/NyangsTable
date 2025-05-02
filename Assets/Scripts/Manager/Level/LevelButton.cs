using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public Sprite lockSprite;
    public Sprite unlockSprite;
    
    public Image image;
    public Image lockImage;

    public TMP_Text lockText;

    private int level = 0;

    private Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
    }

    public void Setup(int level, bool isUnlock)
    {
        this.level = level;
        //levelText.text = level.ToString();

        if(isUnlock)
        {
            image.sprite = unlockSprite;
            button.enabled = true;
            lockImage.enabled = false;
            lockText.enabled = false;
        }
        else
        {
            image.color = new Color32(77, 77, 77, 255);
            button.enabled = false;
            lockImage.enabled = true;
            lockText.enabled = true;
        }
    }
}
