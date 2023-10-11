using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Attack 
{
    public AnimationClip clip;
    public int damage;
    public float knockback;
}

[Serializable]
public class AttackMutipleClip 
{
    public AnimationClip clipUp;
    public AnimationClip clipMiddle;
    public AnimationClip clipDown;
    public int damage;
    public float knockback;
}


