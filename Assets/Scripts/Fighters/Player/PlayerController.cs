using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.VFX;
using DG.Tweening;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private GameObject hitBoxs;

    public GameObject fighterCam;

    #region Intern Variable
    private bool isGrounded;
    private bool isDashDown = false;
    private bool isDie = false;
    private bool isOnPlateform = false;

    private float x = 0;
    [HideInInspector]
    public float y = 0;

    private int chanceCommentateur = 0;


    [Header("Variables")]
    [HideInInspector]
    public float lastDirection = 0;
    //[HideInInspector]
    public bool isAttacking = false;
    //[HideInInspector]
    public bool isParrying = false;
    //[HideInInspector]
    public bool isStun = false;
    //[HideInInspector]
    public bool canDash = true;
    [HideInInspector]
    public string playerName;
    [HideInInspector]
    public GameManager gameManager;
    public float dashForce;

    [SerializeField]
    private bool isInvicible;


    #endregion

    #region VFX
    [Header("VFX")]
    [SerializeField]
    private VisualEffect playerJump;
    [SerializeField]
    private VisualEffect playerLand;
    [SerializeField]
    private VisualEffect playerRun;
    [SerializeField]
    private VisualEffect playerDash;

    #endregion

    [Space(20)]

    #region HP
    [Header("Hp")]
    [SerializeField]
    private float maxHp;
    private float hp;

    private List<Image> whiteHpBarre;
    private List<Image> redHpBarre;

    private bool canDecreaseRedHpBarre = false;
    [SerializeField]
    private float rateRedHpBarre;

    private int currentWhiteCell;
    private int currentRedCell;
    #endregion

    [Space(20)]
    [Header("Autres variables")]
    [SerializeField]
    private FighterData fighterData;
    public FighterData GetFighterData() { return fighterData; }

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject upperLeftLimit, lowerRightLimit;

    [SerializeField]
    private GameObject CameraSong;

    private GameObject menuPause;

    [SerializeField]
    private PlayerInput playerInput;

    #region Movement Variable


    [Space(20)]

    [Header("Mouvement")]
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float airControl;
    [SerializeField]
    private float groundControl;
    [SerializeField]
    private int maxNbJumpInAir;
    private int nbJump;


    [Space(20)]

    [Header("Friction")]
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask OneWayGroundLayer;
    [SerializeField]
    private float groundDrag;
    #endregion




    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        nbJump = maxNbJumpInAir;
        hp = maxHp;

    }

    private void Update() {

        //adding gravity
        rb.AddForce(transform.up * -10);


        RaycastHit raycastHit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out raycastHit, GetFighterData().playerHeight * 0.5f + 0.2f, groundLayer);

        animator.SetBool("In Air", !isGrounded);

        if (raycastHit.transform != null && raycastHit.transform.gameObject.tag.Equals("Plateform"))
            isOnPlateform = true;
        else
            isOnPlateform = false;

        if (lastDirection < 0) {
            animator.SetBool("Mirror", true);
            hitBoxs.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            playerRun.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }
        if (lastDirection > 0) {
            animator.SetBool("Mirror", false);
            hitBoxs.transform.localRotation = Quaternion.Euler(0f, 0, 0f);
            playerRun.transform.localRotation = Quaternion.Euler(0f, 0, 0f);
        }

        if (canDecreaseRedHpBarre) {
            Debug.Log("decrease");
            if (currentRedCell == currentWhiteCell && whiteHpBarre[currentWhiteCell].fillAmount >= redHpBarre[currentRedCell].fillAmount)
                canDecreaseRedHpBarre = false;
            else {
                redHpBarre[currentRedCell].fillAmount -= rateRedHpBarre;
                if (redHpBarre[currentRedCell].fillAmount == 0)
                    currentRedCell -= 1;
            }
        }

        SpeedController();
    }

    void FixedUpdate() {
        if (isGrounded && rb.velocity.y == 0) {
            rb.drag = groundDrag;
            nbJump = maxNbJumpInAir;
        }
        else
            rb.drag = 0;

        if (!isAttacking)
            Move();
    }



    #region Event Input System
    #region Map Player
    public void OnMoveX(InputAction.CallbackContext context) {
        if (!isStun && context.started && isGrounded) {
            animator.SetBool("Run", true);
            playerRun.Play();
        }
        if (!isGrounded)
            playerRun.Stop();

        if (!isStun) {
            x = context.ReadValue<float>();

            if (x > 0)
                x = 1;
            else if (x < 0)
                x = -1;
            else
                x = 0;

            if (lastDirection * -1 == x && rb.velocity.x != 0) {
                Debug.Log("drift");
                animator.SetTrigger("Drift");
            }

            if (x != 0)
                lastDirection = x;

        }
        if (context.canceled) {
            animator.SetBool("Run", false);
            playerRun.Stop();
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
            else if (!isGrounded) {
                LaunchAerialAttack(x, y);
            }
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

    public void OnRightStick(InputAction.CallbackContext context) {
        if (context.performed && !isStun && !isGrounded && !isAttacking) {
            isAttacking = true;
            x = context.ReadValue<Vector2>()[0];
            if (x != 0) {
                lastDirection = x;
            }
            y = context.ReadValue<Vector2>()[1];
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
        if (!isStun && !isAttacking && context.performed) {
            Jump();

        }
    }

    public void OnDash(InputAction.CallbackContext context) {
        if (!isStun && canDash && context.performed) {
            Debug.Log("Dash");
            Dash();
            playerDash.Play();
        }
    }
    public void OnGoDownPlateform(InputAction.CallbackContext context) {
        if (!isStun && isGrounded && isOnPlateform && context.performed) {
            Debug.Log("DashDown");
            DashDown();
        }
    }


    public void OnPause(InputAction.CallbackContext context) {
        if (context.performed) {
            if (menuPause.activeSelf) {
                Debug.Log("yolo");
                menuPause.GetComponent<MenuPause>().Resume();
            }
            else {
                menuPause.SetActive(true);
                gameManager.DisplayBlurEffect();
                Time.timeScale = 0;
                GameManager.SetActionMap("OptionSwap");
            }
        }
    }
    #endregion

    #region Map Options Swapper
    public void OnChangePannel(InputAction.CallbackContext context) {
        if (context.performed)
            menuPause.GetComponent<OptionsSwapper>().OnChangePannel(context);
    }

    public void OnReturn(InputAction.CallbackContext context) {
        if (context.performed)
            menuPause.GetComponent<OptionsSwapper>().OnReturn(context);
    }

    #endregion
    #endregion

    #region Player Movement
    public void Jump() {
        if (nbJump > 0) {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            nbJump -= 1;
            playerJump.Play();
        }
    }

    public void Move() {
        Vector3 move = new Vector3(x, 0, 0);
        if (!isAttacking) {
            if (move.x > 0)
                hitBoxs.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            if (move.x < 0)
                hitBoxs.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
        }

        if (isGrounded) {
            rb.AddForce(move * playerSpeed * 10f * groundControl, ForceMode.Force);
        }

        else if (!isGrounded)
            rb.AddForce(move * playerSpeed * 10f * airControl, ForceMode.Force);

    }

    public void SpeedController() {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, 0f);

        if (flatVelocity.magnitude > playerSpeed) {
            Vector3 limitedSpeed = flatVelocity.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedSpeed.x, rb.velocity.y, 0f);
        }
    }

    public void Dash() {

        if (lastDirection < 0) {

            if (transform.position.x - dashForce < upperLeftLimit.transform.position.x) {
                transform.DOMoveX(upperLeftLimit.transform.position.x, 0.3f);
            }
            else {
                transform.DOMoveX(transform.position.x - dashForce, 0.3f);
            }

        }
        else if (lastDirection > 0) {

            if (transform.position.x + dashForce > lowerRightLimit.transform.position.x) {
                transform.DOMoveX(lowerRightLimit.transform.position.x, 0.3f);
            }
            else {
                transform.DOMoveX(transform.position.x + dashForce, 0.3f);
            }

        }

        if (y < 0) {
            if (transform.position.y - dashForce < lowerRightLimit.transform.position.y) {
                transform.DOMoveY(lowerRightLimit.transform.position.y, 0.3f);
            }
            else {
                transform.DOMoveY(transform.position.y - dashForce, 0.3f);
            }

        }
        else if (y > 0) {
            if (transform.position.y + dashForce > upperLeftLimit.transform.position.y) {
                transform.DOMoveY(upperLeftLimit.transform.position.y, 0.3f);
            }
            else {
                transform.DOMoveY(transform.position.y + dashForce, 0.3f);
            }

        }

        canDash = false;

        StartCoroutine(DashCoolDown());
        StartCoroutine(StopDash());
    }
    public void DashDown() {
        gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        if (transform.position.y - dashForce < lowerRightLimit.transform.position.y) {
            transform.DOMoveY(lowerRightLimit.transform.position.y, 0.2f);
        }
        else {
            transform.DOMoveY(transform.position.y - dashForce, 0.2f);
        }

        isDashDown = true;
        StartCoroutine(StopDash());


    }

    public IEnumerator DashCoolDown() {
        yield return new WaitForSeconds(1);
        canDash = true;

    }

    public IEnumerator StopDash() {
        if (isDashDown) {
            yield return new WaitForSeconds(0.2f);
            isDashDown = false;
        }
        else {
            yield return new WaitForSeconds(0.3f);
        }

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
        if (isInvicible)
            return;
        switch (type) {
            case HitBox.HitBoxType.Heavy:
                if (hp <= 20.0f * maxHp / 100.0f)
                    PlayerDie();
                else
                    hp -= percentageDamage * maxHp / 100.0f;

                chanceCommentateur = Random.Range(0, 3);
                if (chanceCommentateur == 1) {
                    //CameraSong.GetComponent<CommentateurCamera>().CommentateurCoups();
                }
                break;
            case HitBox.HitBoxType.Middle:
                if (hp <= 10.0f * maxHp / 100.0f)
                    PlayerDie();
                else
                    hp -= percentageDamage * maxHp / 100.0f;
                break;
            case HitBox.HitBoxType.Trap:
                hp -= percentageDamage * maxHp / 100.0f;
                chanceCommentateur = Random.Range(0, 5);
                if (chanceCommentateur == 1) {
                    //CameraSong.GetComponent<CommentateurCamera>().CommentateurPiege();
                }


                break;
            default:
                hp -= percentageDamage * maxHp / 100.0f;
                break;
        }

        if (!isDie && hp < 0)
            hp = 1;

        Debug.Log("percentageHp " + (hp / maxHp));

        StartCoroutine(UpdateHpBarre(percentageDamage));

        Debug.Log(gameObject.name + " hp " + hp);
    }


    public IEnumerator UpdateHpBarre(float percentageDamage) {
        float nbCellToUpdate = percentageDamage / 20;
        int nbCellFull = (int)nbCellToUpdate;
        float nbCellDecimal = nbCellToUpdate - nbCellFull;


        for (int i = nbCellFull; !isDie && (i > 0); i -= 1) {
            if (currentWhiteCell > 0) {
                whiteHpBarre[currentWhiteCell].fillAmount = 0;
                currentWhiteCell -= 1;
            }
        }

        if (!isDie) {
            if (whiteHpBarre[currentWhiteCell].fillAmount - nbCellDecimal < 0) {
                //fillAmountRemaining alway be negative
                float fillAmountRemaining = whiteHpBarre[currentWhiteCell].fillAmount - nbCellDecimal;
                whiteHpBarre[currentWhiteCell].fillAmount += fillAmountRemaining;
                currentWhiteCell -= 1;
                if (currentWhiteCell >= 0)
                    whiteHpBarre[currentWhiteCell].fillAmount -= nbCellDecimal + fillAmountRemaining;
            }
            else if (whiteHpBarre[currentWhiteCell].fillAmount - nbCellDecimal == 0) {
                whiteHpBarre[currentWhiteCell].fillAmount -= nbCellDecimal;
                currentWhiteCell -= 1;
            }
            else
                whiteHpBarre[currentWhiteCell].fillAmount -= nbCellDecimal;
        }

        yield return new WaitForSeconds(0.4f);

        canDecreaseRedHpBarre = true;

    }

    public void PlayerDie() {
        Debug.Log("T MORT !!!!!");
        hp = 0;
        isDie = true;
        gameManager.EndRound(playerName);
    }

    public void ResetFighter(int lastDirection) {
        this.lastDirection = lastDirection;
        isAttacking = false;
        isParrying = false;
        isStun = false;
        canDash = true;
        isDie = false;

        animator.SetTrigger("ReturnIdle");

        fighterCam.SetActive(false);

        hp = maxHp;
        for (int i = 0; i < whiteHpBarre.Count; i += 1) {
            whiteHpBarre[i].fillAmount = 1;
            redHpBarre[i].fillAmount = 1;
        }

        currentWhiteCell = whiteHpBarre.Count - 1;
        currentRedCell = redHpBarre.Count - 1;


        nbJump = maxNbJumpInAir;
    }

    public void ApplyKnockback(float knockbackForce, Vector2 knockbackDirection) {
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }


    public void LaunchAerialAttack(float x, float y) {
        Debug.Log("Aerial");
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

        if (x == 0 && y == 0) {
            Debug.Log("No Aerial Direction");
            isAttacking = false;
            return;
        }
    }

    public void RebindAnimator() {
        animator.Rebind();
    }
    #endregion


    #region Set Variable With Animation
    public void SetTriggerStun(float time) {
        if (!isDie) {
            Debug.Log(gameObject.name + " Stun");
            StartCoroutine(StunCoolDown(time));
            animator.SetTrigger("Stun");
        }

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

    public void SetHpBarre(List<Image> whiteHpBarreInGame, List<Image> redHpBarreInGame) {
        whiteHpBarre = whiteHpBarreInGame;
        redHpBarre = redHpBarreInGame;
        currentWhiteCell = whiteHpBarre.Count - 1;
        currentRedCell = redHpBarre.Count - 1;
    }

    public void SetMenuPause(GameObject menuPauseInGame) {
        menuPause = menuPauseInGame;
    }

    public void SetArenaLimit(GameObject upperLeftLimit, GameObject lowerRightLimit) {
        this.upperLeftLimit = upperLeftLimit;
        this.lowerRightLimit = lowerRightLimit;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * (fighterData.playerHeight * 0.5f + 0.2f));
    }


}
