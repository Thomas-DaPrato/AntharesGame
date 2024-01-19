using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SetRateVFXFlame : MonoBehaviour
{
    [SerializeField]
    private float rate;

    [SerializeField]
    private List<VisualEffect> VFXs = new List<VisualEffect>();

    private void OnEnable() {
        foreach (VisualEffect vfx in VFXs) {
            vfx.playRate = rate;
        }
    }
}
