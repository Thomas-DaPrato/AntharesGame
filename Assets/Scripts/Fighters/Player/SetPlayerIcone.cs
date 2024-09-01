using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class SetPlayerIcone : MonoBehaviour
{
    public Sprite P1Icone;
    public Sprite P2Icone;
    /*public string P1IconeEntry;
    public string P2IconeEntry;
    */
    public LocalizedSprite spriteP1;
    public LocalizedSprite spriteP2;

    public Image support;


    public void Init(string playerName)
    {
        if (playerName.Equals("P1"))
        {
            support.GetComponent<LocalizeSpriteEvent>().AssetReference = spriteP1;
            //support.sprite = P1Icone;
        }
        else
        {
            support.GetComponent<LocalizeSpriteEvent>().AssetReference = spriteP2;
            //support.sprite = P2Icone;
        }

    }
}
