using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;   

[CreateAssetMenu(fileName = "New Fighter", menuName = "Fighter")]
public class FighterData : ScriptableObject
{
    public GameObject prefab;

    [Header("Fight")]
    [Header("Attack")]
    public Attack heavyAttack;
    public Attack middleAttack;
    public Attack lightAttack;
    public Attack aerialAttack;

    [Space(5)]

    [Header("Variable")]
    public float stunTimeParry;
    public float playerHeight;

    [Space(10)]

    [Header("Mirror Color")]
    public Material skinMirrorMatch;
    public Sprite orginalSkinLoadingScene;
    public Sprite mirrorSkinLoadingScene;

    [Space(10)]

    [Header("Color Dash")]
    public Color colorDash;
    public Color mirrorColorDash;
    
    [Space(10)]

    [Header("Color Heavy Feedback")]
    public Color colorHeavyFeedback;
    public Color mirrorColorHeavyFeedback;

    [Space(10)]

    [Header("Color")]
    public Color originalColor;
    public Color mirrorColor;

    [Space(10)]

    [Header("Info Menu")]
    public string nickName;
    public Sprite spriteOriginalNotSelected;
    public Sprite spriteOriginalSelected;
    public Sprite spriteMirrorNotSelected;
    public Sprite spriteMirrorSelected;
    public TextAsset lore;
    public string loreEntry;
    public Stat[] stats = new Stat[3];

    [Space(10)]

    [Header("Win Prefab")]

    public GameObject prefabWin;

}

[Serializable]
public class Stat
{
    public string nameStat;
    public string nameStatEntry;
    [Range(1,3)]
    public int value;
}

