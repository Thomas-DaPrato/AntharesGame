using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;   

[CreateAssetMenu(fileName = "New Fighter", menuName = "Fighter")]
public class FighterData : ScriptableObject
{
    public GameObject prefab;

    public Attack heavyAttack;
    public Attack middleAttack;
    public Attack lightAttack;
    public Attack aerialAttack;

    public float playerHeight;

    public Material skinMirrorMatch;
    public Sprite orginalSkinLoadingScene;
    public Sprite mirrorSkinLoadingScene;


    [Header("Info Menu")]
    public Sprite spriteOriginalNotSelected;
    public Sprite spriteOriginalSelected;
    public Sprite spriteMirrorNotSelected;
    public Sprite spriteMirrorSelected;
    public Stat[] stats = new Stat[3];

}

[Serializable]
public class Stat
{
    public string nameStat;
    [Range(1,3)]
    public int value;
}

