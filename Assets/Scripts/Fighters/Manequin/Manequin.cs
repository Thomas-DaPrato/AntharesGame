using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class Manequin : MonoBehaviour
{
    [SerializeField]
    private bool isHeavyAttacking;
    [SerializeField]
    private bool isMiddleAttacking;
    [SerializeField]
    private bool isLightAttacking;
    [SerializeField]
    private bool isParrying;


    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Animator animator;

    private void Awake() {
        if(enabled)
            gameObject.GetComponent<PlayerInput>().enabled = false;
    }

    private void Update() {
        if (!playerController.isAttacking && isHeavyAttacking) {
            playerController.isAttacking = true;
            animator.SetTrigger("HeavyAttack");         
        }

        if (!playerController.isAttacking && isMiddleAttacking) {
            playerController.isAttacking = true;
            animator.SetTrigger("MiddleAttack");         
        }

        if (!playerController.isAttacking && isLightAttacking) {
            playerController.isAttacking = true;
            animator.SetTrigger("LightAttack");         
        }
        
        if (!playerController.isParrying && isParrying) {
            animator.SetTrigger("Parry");         
        }
            
    }

   


}
