using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fighter", menuName = "Fighter")]
public class FighterData : ScriptableObject
{
    public AnimationClip punch;
    public int punchAtkDmg;
    public AnimationClip jump;
    public AnimationClip foot;

    public List<KeyValuePair<AnimationClip,AnimationClip>> GetClipOverride(AnimatorOverrideController animatorOverride) {

        List<KeyValuePair<AnimationClip, AnimationClip>> clipOverride = new List<KeyValuePair<AnimationClip, AnimationClip>>();

        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["PunchBase"], punch));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["FootBase"], foot));
        clipOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(animatorOverride["JumpBase"], jump));

        

        return clipOverride;
    }

}

