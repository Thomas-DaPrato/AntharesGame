using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    private Rigidbody rb;

    #region Intern Variable
    private bool isGrounded;
    private float x = 0;
    [HideInInspector]
    public float y = 0;


    [HideInInspector]
    public float lastDirection = 0;
    [HideInInspector]
    public bool isAttacking = false;
    [HideInInspector]
    public bool isParrying = false;
    [HideInInspector]
    public bool isStun = false;
    [HideInInspector]
    public bool canDash = true;
    [HideInInspector]
    public string playerName;
    [HideInInspector]
    public GameManager gameManager;


    #endregion

    [Space(20)]


    [SerializeField]
    private float maxHp;
    private float hp;

    [SerializeField]
    private FighterData fighterData;
    public FighterData GetFighterData() { return fighterData; }

    [SerializeField]
    private Animator animator;

    private Image hpBarre;
    private GameObject menuPause;

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
        hp = maxHp;
    }

    private void Update() {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 2 * 0.5f + 0.2f, groundLayer);


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
    public void OnMoveX(InputAction.CallbackContext context) {
        if (!isStun) {
            x = context.ReadValue<float>();
            if (x != 0) {
                lastDirection = x;
            }
        }
    }
    public void OnMoveY(InputAction.CallbackContext context) {
        if (!isStun) {
            y = context.ReadValue<float>();
            
        }
    }

    public void OnHeavyAttack(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking && !isStun) {
            isAttacking = true;
            if (isGrounded) {
                Debug.Log(gameObject.name + " Heavy");
                animator.SetTrigger("HeavyAttack");
            }
            else if (!isGrounded)
                LaunchAerialAttack(x, y);
        }
    }

    public void OnMiddleAttack(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking && !isStun) {
            isAttacking = true;
            if (isGrounded) {
                Debug.Log(gameObject.name + " Middle");
                animator.SetTrigger("MiddleAttack");
            }
            else if (!isGrounded)
                LaunchAerialAttack(x, y);
        }
    }
    public void OnLightAttack(InputAction.CallbackContext context) {  
        if (context.performed && !isAttacking && !isStun) {
            isAttacking = true;
            if (isGrounded) {
                Debug.Log(gameObject.name + " Light");
                animator.SetTrigger("LightAttack");
            }
            else if (!isGrounded)
                LaunchAerialAttack(x, y);
        }

    }
    public void OnParry(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking && !isStun) {
            Debug.Log(gameObject.name + " Parry");
            animator.SetTrigger("Parry");
        }

    }

    public void OnJump(InputAction.CallbackContext context) {
        if (!isStun && context.performed)
            Jump();
    }

    public void OnDash(InputAction.CallbackContext context) {
        if (!isStun && canDash && context.performed) {
            Debug.Log("Dash");
            Dash();
        }
    }
    public void OnDashDown(InputAction.CallbackContext context)
    {
        if (!isStun && isGrounded && context.performed)
        {
            Debug.Log("DashDown");
            DashDown();
        }
    }

    public void OnPause(InputAction.CallbackContext context) {
        if (context.performed) {
            if (menuPause.activeSelf) {
                menuPause.SetActive(false);
                Time.timeScale = 1;               
            }
            else {
                menuPause.SetActive(true);
                Time.timeScale = 0;
            }
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
        Vector3 move = new Vector3(x, 0, 0);
        if (!isAttacking) {
            if (move.x > 0)
                gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            if (move.x < 0)
                gameObject.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }

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
    public void DashDown()
    {
        Vector3 move = new Vector3(0, -1, 0);
        rb.AddForce(move*10 , ForceMode.Impulse);
        
        
    }

    public IEnumerator DashCoolDown() {
        yield return new WaitForSeconds(1);
        canDash = true;
    }

    public IEnumerator StunCoolDown(float time) {
        isStun = true;
        yield return new WaitForSeconds(time);
        animator.SetTrigger("ReturnIdle");
        isStun = false;
        isAttacking = false;
        Debug.Log(gameObject.name + " is not anymore stunt");
    }
    #endregion

    #region Player Fonctions
    public void TakeDamage(float percentageDamage, HitBox.HitBoxType type) {
        switch (type) {
            case HitBox.HitBoxType.Heavy:
                if (hp <= 20.0f * maxHp / 100.0f)
                    PlayerDie();
                else
                    hp -= percentageDamage * maxHp / 100.0f; 
                break;
            case HitBox.HitBoxType.Middle:
                if (hp <= 10.0f * maxHp / 100.0f)
                    PlayerDie();
                else {
                    hp -= percentageDamage * maxHp / 100.0f;
                    if (hp <= 0)
                        hp = 1;
                }
                break;
            default :
                hp -= percentageDamage * maxHp / 100.0f;
                if (hp <= 0)
                    hp = 1;
                break;
        }

        Debug.Log("percentageHp " + (hp / maxHp));
        if(hpBarre != null) {
            hpBarre.fillAmount = hp / maxHp;
        }
            
        Debug.Log(gameObject.name + " hp " + hp);
        //Debug.Log("fill " + hpBarre.fillAmount);
    }

    public void PlayerDie() {
        Debug.Log("T MORT !!!!!");
        gameManager.EndRound(playerName);
    }

    public void ResetFighter() {
        lastDirection = 0;
        isAttacking = false;
        isParrying = false;
        isStun = false;
        canDash = true;

        hp = maxHp;
        nbJump = maxNbJumpInAir;
    }

    public void ApplyKnockback(float knockbackForce, Vector2 knockbackDirection) {
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }
    

    public void LaunchAerialAttack(float x, float y) {

        Vector2 coordinate = new Vector2(Mathf.Abs(x), y);
        coordinate = coordinate.normalized;

        if (coordinate.x <= 0.66f && coordinate.y > 0) {
            Debug.Log(gameObject.name + " Aerial Up");
            animator.SetTrigger("Aerial Up");
        }

        if (coordinate.x > 0.66f) {
            Debug.Log(gameObject.name + " Aerial Middle");
            animator.SetTrigger("Aerial Middle");
        }
        
        if (coordinate.x <= 0.66f && coordinate.y < 0) {
            Debug.Log(gameObject.name + " Aerial Down");
            animator.SetTrigger("Aerial Down");
        }
    }
    #endregion


    #region Set Variable With Animation
    public void SetTriggerStun(float time) {
        Debug.Log(gameObject.name + " Stun");
        StartCoroutine(StunCoolDown(time));
        animator.SetTrigger("Stun");
    }

    public void SetIsAttackingFalse() {
        isAttacking = false;
    }


    public void SetIsParryingTrue() {
        isParrying = true;
    }
    public void SetIsParryingFalse() {
        isParrying = false;
    }


    public void SetIsStunFalse() {
        isStun = false;
        
    }
    #endregion

    public void SetIsGrounded(bool val) {
        isGrounded = val;
    }

    public void SetHpBarre(Image hpBarreInGame) {
        hpBarre = hpBarreInGame;
    }

    public void SetMenuPause(GameObject menuPauseInGame) {
        menuPause = menuPauseInGame;
        Debug.Log("SetMenuPause " + menuPause);
    }


}
