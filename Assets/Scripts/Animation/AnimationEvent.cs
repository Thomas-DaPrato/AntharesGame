using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public GameObject hitBoxHeavyAttack;
    public GameObject hitBoxMiddleAttack;
    public GameObject hitBoxLightAttack;
    public MeshRenderer heartBoxParry;

    public void DisplayHitBoxHeavyAttack() {
        hitBoxHeavyAttack.SetActive(true);
    }
    public void HideHitBoxHeavyAttack() {
        hitBoxHeavyAttack.SetActive(false);
    }

    public void DisplayHitBoxMiddleAttack() {
        hitBoxMiddleAttack.SetActive(true);
    }
    public void HideHitBoxMiddleAttack() {
        hitBoxMiddleAttack.SetActive(false);
    }

    public void DisplayHitBoxLightAttack() {
        hitBoxLightAttack.SetActive(true);
    }
    public void HideHitBoxLightAttack() {
        hitBoxLightAttack.SetActive(false);
    }

    public void DisplayHeartBoxParry() {
        heartBoxParry.enabled = true;
    }
    public void HideHeartBoxParry() {
        heartBoxParry.enabled = false;
    }

    public void DisableAllHitBox() {
        hitBoxHeavyAttack.SetActive(false);
        hitBoxMiddleAttack.SetActive(false);
        hitBoxLightAttack.SetActive(false);
    }
}
