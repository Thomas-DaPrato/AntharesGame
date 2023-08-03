using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAnimatorOverride : MonoBehaviour
{
    [SerializeField]
    private FighterData fighterData;

    [SerializeField]
    private Animator animatorBase;

    private AnimatorOverrideController animatorOverride;

    private void Start() {
        animatorOverride = new AnimatorOverrideController(animatorBase.runtimeAnimatorController);
        animatorBase.runtimeAnimatorController = animatorOverride;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            Debug.Log("override");


            animatorOverride.ApplyOverrides(fighterData.GetClipOverride(animatorOverride));

        }
    }
}
