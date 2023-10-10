using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;



[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    private Rigidbody rb;

    #region Intern Variable
    private bool isGrounded;
    private float direction = 0;

    [HideInInspector]
    public float lastDirection = 0;


    private int hp = 10;

    [HideInInspector]
    public bool isAttacking = false;
    [HideInInspector]
    public bool isParrying = false;
    [HideInInspector]
    public bool isStunt = false;
    [HideInInspector]
    public bool canDash = true;
    #endregion

    [Space(20)]


    [SerializeField]
    private FighterData fighterData;
    public FighterData GetFighterData() { return fighterData; }

    [SerializeField]
    private Animator animator;

    #region Movement Variable
    [SerializeField]
    private float playerSpeed;

    [Space(20)]

    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float airControl;

    [Space(20)]

    [SerializeField]
    private float dashDistance;

    [Space(20)]

    [SerializeField]
    private int maxNbJumpInAir;
    private int nbJump;
    
    [Space(20)]

    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float groundDrag;
    #endregion




    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        nbJump = maxNbJumpInAir;
    }

    private void Update() {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, fighterData.playerHeight * 0.5f + 0.2f, groundLayer);

        SpeedController();
    }

    void FixedUpdate() {
        if (isGrounded) {
            rb.drag = groundDrag;
            nbJump = maxNbJumpInAir;
        }
        else
            rb.drag = 0;

        Move();
    }



    #region Event Input System
    public void OnMove(InputAction.CallbackContext context) {
        if (!isStunt) {
            direction = context.ReadValue<float>();
            if (direction != 0) {
                lastDirection = direction;
            }
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
            Debug.Log("Dash");
            Dash();
        }
    }
    #endregion

    #region Player Movement
    public void Jump() {
        if (nbJump > 0) {
            rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            nbJump -= 1;
        }
    }

    public void Move() {
        Vector3 move = new Vector3(direction, 0, 0);
        if (move.x > 0)
            gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        if (move.x < 0)
            gameObject.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

        if(isGrounded)
            rb.AddForce(move * playerSpeed * 10f, ForceMode.Force);

        else if(!isGrounded)
            rb.AddForce(move * playerSpeed * 10f * airControl, ForceMode.Force);

    }

    public void SpeedController() {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, 0f);

        if(flatVelocity.magnitude > playerSpeed) {
            Vector3 limitedSpeed = flatVelocity.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedSpeed.x, rb.velocity.y, 0f);
        }
    }

    public void Dash() {
        Vector3 move = new Vector3(lastDirection, 0, 0);
        rb.AddForce(move * dashDistance, ForceMode.Impulse);
        canDash = false;
        StartCoroutine(DashCoolDown());
    }

    public IEnumerator DashCoolDown() {
        yield return new WaitForSeconds(1);
        canDash = true;
    }
    #endregion

    #region Player Fonctions
    public void TakeDamage(int damage) {
        if ((hp -= damage) <= 0)
            Debug.Log("T MORT !!!!!");
        else
            Debug.Log("hp : " + hp);
    }

    public void ApplyKnockback(float knockback, float oponenentDirection) {
        Vector3 knockbackDirection = new Vector3(oponenentDirection, 1, 0);
        Debug.Log(knockbackDirection);
        rb.AddForce(knockbackDirection * knockback * Time.deltaTime, ForceMode.Impulse);
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

    public void SetIsGrounded(bool val) {
        isGrounded = val;
    }



}
