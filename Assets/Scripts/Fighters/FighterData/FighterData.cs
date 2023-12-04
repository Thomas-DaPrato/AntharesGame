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


    [Header("Info Menu")]
    public Sprite sprite;
    public Stat[] stats = new Stat[3];

}

[Serializable]
public class Stat
{
    public string nameStat;
    [Range(1,3)]
    public int value;
}

