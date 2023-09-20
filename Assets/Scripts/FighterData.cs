using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fighter", menuName = "Fighter")]
public class FighterData : ScriptableObject
{
    public AnimationClip heavyAttack;
    public int heavyAttackDamage;
    public AnimationClip middleAttack;
    public int middleAttackDamage;
    public AnimationClip lightAttack;
    public int lightAttackDamage;
    public AnimationClip jump;

    public List<KeyValuePair<AnimationClip,AnimationClip>> GetClipOverride(AnimatorOverrideController animatorOverride) {

        List<KeyValuePair<AnimationClip, AnimationClip>> clipOverride = new List<KeyValuePair<AnimationClip, AnimationClip>>();

        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["HeavyAttackBase"], heavyAttack));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["MiddleAttackBase"], middleAttack));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["LightAttackBase"], lightAttack));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["JumpBase"], jump));

        

        return clipOverride;
    }

}

