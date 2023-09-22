using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxAnimationEvent : MonoBehaviour
{
    public GameObject HitBoxHeavyAttack;
    public GameObject HitBoxMiddleAttack;
    public GameObject HitBoxLightAttack;

    public void DisplayHitBoxHeavyAttack() {
        HitBoxHeavyAttack.SetActive(true);
    }
    public void HideHitBoxHeavyAttack() {
        HitBoxHeavyAttack.SetActive(false);
    }

    public void DisplayHitBoxMiddleAttack() {
        HitBoxMiddleAttack.SetActive(true);
    }
    public void HideHitBoxMiddleAttack() {
        HitBoxMiddleAttack.SetActive(false);
    }

    public void DisplayHitBoxLightAttack() {
        HitBoxLightAttack.SetActive(true);
    }
    public void HideHitBoxLightAttack() {
        HitBoxLightAttack.SetActive(false);
    }
}
