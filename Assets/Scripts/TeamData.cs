using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Member", menuName = "Member")]
public class TeamData : ScriptableObject
{
    [Header("Info Menu")]
    public string memberName;
    public Stat[] stats = new Stat[3];
}
