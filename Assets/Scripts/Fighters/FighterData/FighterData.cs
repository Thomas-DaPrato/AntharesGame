using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fighter", menuName = "Fighter")]
public class FighterData : ScriptableObject
{
    [Header("Info Menu")]
    public Sprite sprite;
    public int speed;
    public int strength;
    public int range;
    
    [Header("Attack Animation")]
    public Attack lightAttack;
    public Attack middleAttack;
    public Attack heavyAttack;
    public AttackMutipleClip aerialsAttack;

    [Header("Mouvement Animation")]
    public AnimationClip jump;

    [Header("Defense Animation")]
    public AnimationClip parry;
    public float time;

    public AnimationClip interrupt;

    [Header("Player Variable")]
    public float playerHeight;

    public List<KeyValuePair<AnimationClip,AnimationClip>> GetClipOverride(AnimatorOverrideController animatorOverride) {

        List<KeyValuePair<AnimationClip, AnimationClip>> clipOverride = new List<KeyValuePair<AnimationClip, AnimationClip>>();

        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["HeavyAttackBase"], heavyAttack.clip));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["MiddleAttackBase"], middleAttack.clip));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["LightAttackBase"], lightAttack.clip));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["AerialUpBase"], aerialsAttack.clipUp));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["AerialMiddleBase"], aerialsAttack.clipMiddle));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["AerialDownBase"], aerialsAttack.clipDown));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["JumpBase"], jump));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["ParryBase"], parry));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["InterruptBase"], interrupt));

        

        return clipOverride;
    }

}

