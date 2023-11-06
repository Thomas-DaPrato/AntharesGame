using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Member", menuName = "Menber")]
public class TeamData : ScriptableObject
{
    [Header("Info Menu")]
    public Sprite sprite;
    public string name;
    public Stat[] stats = new Stat[3];
}
