using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    #region Intern Variable
    private float direction = 0;
    private float lastDirection;

    private int hp = 10;

    public bool isAttacking = false;
    public bool isParrying = false;
    public bool isStunt = false;
    public bool canDash = true;
    #endregion


    [SerializeField]
    private FighterData fighterData;
    public FighterData GetFighterData() { return fighterData; }

    [SerializeField]
    private Animator animator;

    #region Movement Variable
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float playerMaxSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float gravityValue;
    [SerializeField]
    private float dashDistance;
    [SerializeField]
    private int maxNbJump;
    private int nbJump;
    
    #endregion




    private void Awake() {
        controller = gameObject.GetComponent<CharacterController>();
        nbJump = maxNbJump;
    }

    void FixedUpdate() {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            nbJump = maxNbJump;
            playerVelocity.y = 0f;
        }

        Move();

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



    #region Event Input System
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
        if (!isStunt && context.performed)
            Jump();
    }

    public void OnDash(InputAction.CallbackContext context) {
        if (!isStunt && canDash && context.performed) {
            Dash();
        }
    }
    #endregion

    #region Player Movement
    public void TakeDamage(int damage) {
        if ((hp -= damage) <= 0)
            Debug.Log("T MORT !!!!!");
        else
            Debug.Log("hp : " + hp);
    }

    public void Jump() {
        if (nbJump > 0) {
            playerVelocity.y += jumpHeight * 3;
            nbJump -= 1;
        }
    }

    public void Move() {
        Vector3 move = new Vector3(direction, 0, 0);
        if (move.x > 0)
            gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        if (move.x < 0)
            gameObject.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    public void Dash() {
        Vector3 move = new Vector3(lastDirection, 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed * dashDistance);
        canDash = false;
        StartCoroutine(DashCoolDown());
    }

    public IEnumerator DashCoolDown() {
        yield return new WaitForSeconds(1);
        canDash = true;
    }
    #endregion


    #region Set Variable With Animation
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
    #endregion

}
