using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public GameObject HitBoxHeavyAttack;
    public GameObject HitBoxMiddleAttack;
    public GameObject HitBoxLightAttack;
    public MeshRenderer HeartBoxParry;

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

    public void DisplayHeartBoxParry() {
        HeartBoxParry.enabled = true;
    }
    public void HideHeartBoxParry() {
        HeartBoxParry.enabled = false;
    }
}
