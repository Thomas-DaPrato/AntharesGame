using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public GameObject hitBoxHeavyAttack;
    public GameObject hitBoxMiddleAttack;
    public GameObject hitBoxLightAttack;
    public GameObject hitBoxAerialAttackUp;
    public GameObject hitBoxAerialAttackMiddle;
    public GameObject hitBoxAerialAttackDown;
    public MeshRenderer heartBoxParry;

    [SerializeField]
    private PlayerController playerController;


    #region Heavy Attack
    public void DisplayHitBoxHeavyAttack() {
        if(!playerController.isDie)
            hitBoxHeavyAttack.SetActive(true);
    }
    public void HideHitBoxHeavyAttack() {
        hitBoxHeavyAttack.SetActive(false);
    }
    #endregion

    #region Middle Attack
    public void DisplayHitBoxMiddleAttack() {
        if (!playerController.isDie)
            hitBoxMiddleAttack.SetActive(true);
    }
    public void HideHitBoxMiddleAttack() {
        hitBoxMiddleAttack.SetActive(false);
    }
    #endregion

    #region Light Attack
    public void DisplayHitBoxLightAttack() {
        if (!playerController.isDie)
            hitBoxLightAttack.SetActive(true);
    }
    public void HideHitBoxLightAttack() {
        hitBoxLightAttack.SetActive(false);
    }
    #endregion

    #region HeartBoxParry
    public void DisplayHeartBoxParry() {
        if (!playerController.isDie)
            heartBoxParry.enabled = true;
    }
    public void HideHeartBoxParry() {
        heartBoxParry.enabled = false;
    }
    #endregion

    #region Aerial Attack Up
    public void DisplayHitBoxAerialAttackUp() {
        if (!playerController.isDie)
            hitBoxAerialAttackUp.SetActive(true);
    }
    public void HideHitBoxAerialAttackUp() {
        hitBoxAerialAttackUp.SetActive(false);
    }
    #endregion

    #region Aerial Attack Middle 
    public void DisplayHitBoxAerialAttackMiddle() {
        if (!playerController.isDie)
            hitBoxAerialAttackMiddle.SetActive(true);
    }
    public void HideHitBoxAerialAttackMiddle() {
        hitBoxAerialAttackMiddle.SetActive(false);
    }
    #endregion

    #region Aerial Attack Down
    public void DisplayHitBoxAerialAttackDown() {
        if (!playerController.isDie)
            hitBoxAerialAttackDown.SetActive(true);
    }
    public void HideHitBoxAerialAttackDown() {
        hitBoxAerialAttackDown.SetActive(false);
    }
    #endregion


    public void DisableAllHitBox() {
        hitBoxHeavyAttack.SetActive(false);
        hitBoxMiddleAttack.SetActive(false);
        hitBoxLightAttack.SetActive(false);
    }
}
