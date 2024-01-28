using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkenKey : MonoBehaviour
{
    public Sprite darkenX;
    public Sprite notDarkenX;

    public Image supportKey;
    
    public void DarkenXKey() {
        supportKey.sprite = darkenX;
    }

    public void ResetColorSprite() {
        supportKey.sprite = notDarkenX;
    }
}
