using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 playerControllerVelocity;
    private bool groundedPlayer;

    private float lastDirection; 

    public bool isAttacking = false;
    public bool isParrying = false;
    public bool isStunt = false;



    [SerializeField]
    private FighterData fighterData;
    public FighterData GetFighterData() { return fighterData; }

    [SerializeField]
    private float playerSpeed = 6.0f;
    [SerializeField]
    private float playerMaxSpeed = 7.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private Animator animator;



    private float direction = 0;
    private bool jump = false;

    private int hp = 10;

    private void Awake() {
        controller = gameObject.GetComponent<CharacterController>();
    }
   
    void FixedUpdate() {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(direction, 0, 0);
        if (move.x > 0)
            gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        if (move.x < 0)
            gameObject.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        controller.Move(move * Time.deltaTime * playerSpeed);


        // Changes the height position of the player..
        if (jump && groundedPlayer)
            playerVelocity.y += jumpHeight * 3;

        playerVelocity.y += gravityValue * Time.deltaTime;


        if (!groundedPlayer)
            if (lastDirection > 0)
                if (playerVelocity.x < playerMaxSpeed)
                    playerVelocity.x += controller.velocity.x * Time.deltaTime * playerSpeed;
                else
                    playerVelocity.x = playerMaxSpeed;

            else
                if (playerVelocity.x > -playerMaxSpeed)
                    playerVelocity.x += controller.velocity.x * Time.deltaTime * playerSpeed;
                else
                    playerVelocity.x = -playerMaxSpeed;
        else
            playerVelocity.x = 0;

        

        controller.Move(playerVelocity * Time.deltaTime);

    }


    public void OnMove(InputAction.CallbackContext context) {
        if (!isStunt) {
            direction = context.ReadValue<float>();
            if (direction != 0)
                lastDirection = direction;
        }

    }

    public void OnHeavyAttack(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking && !isStunt) {
            Debug.Log(gameObject.name + " Heavy");
            isAttacking = true;
            animator.SetTrigger("HeavyAttack");
        }
    }

    public void OnMiddleAttack(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking && !isStunt) {
            Debug.Log(gameObject.name + " Middle");
            isAttacking = true;
            animator.SetTrigger("MiddleAttack");
        }
    }
    public void OnLightAttack(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking && !isStunt) {
            Debug.Log(gameObject.name + " Light");
            isAttacking = true;
            animator.SetTrigger("LightAttack");
        }

    }
    public void OnParry(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking && !isStunt) {
            Debug.Log(gameObject.name + " Parry");
            isParrying = true;
            animator.SetTrigger("Parry");
        }

    }

    public void OnJump(InputAction.CallbackContext context) {
        if (!isStunt)
            jump = context.action.triggered;
    }

    public void TakeDamage(int damage) {
        if ((hp -= damage) <= 0)
            Debug.Log("T MORT !!!!!");
        else
            Debug.Log("hp : " + hp);
    }

    public void SetTriggerInterrupt() {
        Debug.Log(gameObject.name + " Interrupt");
        animator.SetTrigger("Interrupt");
    }

    public void SetIsAttackingFalse() {
        isAttacking = false;
    }
    public void SetIsParryingFalse() {
        isParrying = false;
    }

    public void SetIsStuntTrue() {
        isStunt = true;
    }

    public void SetIsStuntFalse() {
        isStunt = false;
        isAttacking = false;
        Debug.Log("Player is not anymore stunt");
    }
    
}
