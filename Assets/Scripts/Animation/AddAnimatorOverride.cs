using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class AddAnimatorOverride : MonoBehaviour
{

    [SerializeField]
    private Animator animatorBase;

    private AnimatorOverrideController animatorOverride;

    private void Awake() {
        animatorOverride = new AnimatorOverrideController(animatorBase.runtimeAnimatorController);
        animatorBase.runtimeAnimatorController = animatorOverride;
        animatorOverride.ApplyOverrides(gameObject.GetComponent<PlayerController>().GetFighterData().GetClipOverride(animatorOverride));
    }

}
