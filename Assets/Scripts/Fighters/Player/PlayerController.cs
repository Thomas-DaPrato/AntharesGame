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
    public bool isGrounded;
    private bool isDashDown = false;
    public bool isDie = false;
    private bool isOnPlateform = false;
    private bool isRunning = false;

    private float x = 0;
    private float xAerial = 0;
    private float yAerial = 0;

    private float xDash = 0;
    private float yDash = 0;

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
    private float dashForceVal;
    public float downForcePlateforme;

    [SerializeField]
    private bool isInvicible;


    #endregion

    #region VFX
    [Header("VFX")]
    [SerializeField]
    private GameObject playerDashVFX;
    [SerializeField]
    private List<GameObject> playerBurnVFX;
    [SerializeField]
    private List<ParticleSystem> playerHeavyVFX;
    [SerializeField]
    private GameObject playerParryVFX;

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

    private int whiteCellIndex;
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
    private AudioSource audioSource;

    [SerializeField]
    private GameObject upperLeftLimit, lowerRightLimit;

    [SerializeField]
    private GameObject CameraSong;

    private GameObject menuPause;

    private GameObject XKey;

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
    private float groundDrag;


    [SerializeField]
    private LayerMask playerLayer;
    #endregion




    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        nbJump = maxNbJumpInAir;
        hp = maxHp;
        dashForceVal = dashForce;
    }

    private void Update() {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycastHit, GetFighterData().playerHeight * 0.5f + 0.2f, groundLayer);


        if (isGrounded && isRunning)
            animator.SetBool("Run", true);
        animator.SetBool("In Air", !isGrounded);

        if (raycastHit.transform != null && raycastHit.transform.gameObject.CompareTag("Plateform"))
            isOnPlateform = true;
        else
            isOnPlateform = false;

        if (!isAttacking) {
            RotateComponent();
        }

        if (canDecreaseRedHpBarre) {
            if (currentRedCell <= whiteCellIndex && whiteHpBarre[whiteCellIndex].fillAmount >= redHpBarre[currentRedCell].fillAmount)
                canDecreaseRedHpBarre = false;
            else {
                redHpBarre[currentRedCell].fillAmount -= rateRedHpBarre;
                if (redHpBarre[currentRedCell].fillAmount == 0 && currentRedCell != whiteCellIndex)
                    currentRedCell -= 1;
            }
        }

        SpeedController();
    }

    private void RotateComponent() {
        if (lastDirection > 0) {
            animator.SetBool("Mirror", false);
            hitBoxs.transform.localRotation = Quaternion.Euler(0f, 0, 0f);
        }
        if (lastDirection < 0) {
            animator.SetBool("Mirror", true);
            hitBoxs.transform.localRotation = Quaternion.Euler(0f, 180, 0f);
        }
    }

    void FixedUpdate() {
        if (isGrounded && rb.velocity.y <= 0) {
            rb.drag = groundDrag;
            nbJump = maxNbJumpInAir;
        }
        else {
            rb.drag = 0;
        }

        if (!isAttacking && !isStun && !isParrying)
            Move();
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (isGrounded) { 
                collision.gameObject.GetComponent<Rigidbody>().mass = 5;
                rb.mass = 5;
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (isGrounded) {
                collision.gameObject.GetComponent<Rigidbody>().mass = 1;
                GetComponentInParent<Rigidbody>().mass = 1;
            }
        }
    }


    #region Event Input System
    #region Map Player
    public void OnMoveX(InputAction.CallbackContext context) {
        if (!isStun) {
            x = context.ReadValue<float>();
            xDash = x;
            SetLastDirection(x);
            isRunning = true;

        }
        if (context.canceled) {
            isRunning = false;
            animator.SetBool("Run", false);
        }
    }

    private void SetLastDirection(float val) {
        if (val > 0)
            val = 1;
        else if (val < 0)
            val = -1;
        else
            val = 0;

        if (val != 0) 
            lastDirection = val;
    }

    public void OnMoveY(InputAction.CallbackContext context) {
        if (!isStun) {
            yAerial = context.ReadValue<float>();
            yDash = yAerial;
        }
    }

    public void OnHeavyAttack(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking && !isStun) {
            isAttacking = true;
            if (isGrounded) {
                animator.SetTrigger("HeavyAttack");
            }
            else if (!isGrounded)
                LaunchAerialAttack(x, yAerial);
        }
    }

    public void OnMiddleAttack(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking && !isStun) {
            isAttacking = true;
            if (isGrounded) {
                animator.SetTrigger("MiddleAttack");
            }
            else if (!isGrounded)
                LaunchAerialAttack(x, yAerial);
        }
    }
    public void OnLightAttack(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking && !isStun) {
            isAttacking = true;
            if (isGrounded) {
                animator.SetTrigger("LightAttack");
            }
            else if (!isGrounded)
                LaunchAerialAttack(x, yAerial);
        }

    }

    public void OnRightStick(InputAction.CallbackContext context) {
        if (context.performed && !isStun && !isGrounded && !isAttacking) {
            isAttacking = true;

            xAerial = context.ReadValue<Vector2>()[0];
            yAerial = context.ReadValue<Vector2>()[1];

            SetLastDirection(xAerial);
            RotateComponent();

            LaunchAerialAttack(xAerial, yAerial);
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
            animator.SetTrigger("Dash");
            Dash();
            playerDashVFX.SetActive(true);
        }
    }
    public void OnGoDownPlateform(InputAction.CallbackContext context) {
        if (!isStun && isGrounded && isOnPlateform && context.performed) {
            playerDashVFX.SetActive(true);
            SpeedDown();
        }
    }


    public void OnPause(InputAction.CallbackContext context) {
        if (context.performed) {
            if (menuPause.activeSelf) {
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
            if (isGrounded)
                animator.SetTrigger("Jump");
            else
                animator.SetTrigger("Salto");
            rb.mass = 1;
            rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            nbJump -= 1;
        }
    }

    public void Move() {
        Vector3 move = new(x, 0, 0);


        if (isGrounded) {
            rb.AddForce(10f * groundControl * playerSpeed * move, ForceMode.Force);
        }

        else if (!isGrounded)
            rb.AddForce(10f * airControl * playerSpeed * move, ForceMode.Force);

    }

    public void SpeedController() {
        Vector3 flatVelocity = new(rb.velocity.x, 0f, 0f);

        if (flatVelocity.magnitude > playerSpeed) {
            Vector3 limitedSpeed = flatVelocity.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedSpeed.x, rb.velocity.y, 0f);
        }
    }

    public void Dash() {

        if((xDash < -0.4 || xDash > 0.4) && (yDash < -0.4 || yDash > 0.4))
        {
            dashForce = Mathf.Sqrt(dashForce * dashForce / 2);
        }

        if (xDash < -0.4) 
        {

            if (transform.position.x - dashForce < upperLeftLimit.transform.position.x) {
                transform.DOMoveX(upperLeftLimit.transform.position.x, 0.3f);
            }
            else {
                transform.DOMoveX(transform.position.x - dashForce, 0.3f);
            }

        }
        else if (xDash > 0.4) {

            if (transform.position.x + dashForce > lowerRightLimit.transform.position.x) {
                transform.DOMoveX(lowerRightLimit.transform.position.x, 0.3f);
            }
            else {
                transform.DOMoveX(transform.position.x + dashForce, 0.3f);
            }

        }

        if (yDash < -0.4) {
            if (transform.position.y - dashForce < lowerRightLimit.transform.position.y) {
                transform.DOMoveY(lowerRightLimit.transform.position.y, 0.3f);
            }
            else {
                transform.DOMoveY(transform.position.y - dashForce, 0.3f);
            }

        }
        else if (yDash > 0.4) {
            
            if (transform.position.y + dashForce > upperLeftLimit.transform.position.y) {
                transform.DOMoveY(upperLeftLimit.transform.position.y, 0.3f);
            }
            else {
                transform.DOMoveY(transform.position.y + dashForce, 0.3f);
            }

        }

        canDash = false;
        dashForce = dashForceVal;
        StartCoroutine(DashCoolDown());
        StartCoroutine(StopDash());
    }
    public void SpeedDown() {
        gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        if (transform.position.y - downForcePlateforme < lowerRightLimit.transform.position.y) {
            transform.DOMoveY(lowerRightLimit.transform.position.y, 0.2f);
        }
        else {
            transform.DOMoveY(transform.position.y - downForcePlateforme, 0.2f);
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
        playerDashVFX.SetActive(false);

    }

    public IEnumerator StunCoolDown(float time) {
        isStun = true;
        yield return new WaitForSeconds(time);
        animator.SetTrigger("ReturnIdle");
        isStun = false;
        isAttacking = false;
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
            case HitBox.HitBoxType.Aerial:
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

        if (!isDie && hp <= 0)
            hp = 1;

        if ((hp / maxHp) * 100 <= 10)
            XKey.GetComponent<DarkenKey>().DarkenXKey();


        StartCoroutine(UpdateHpBarre());

    }


    public IEnumerator UpdateHpBarre() {
        int currentWhiteCell;
        float percentageHP = hp / maxHp;

        float nbCellToUpdate = percentageHP * whiteHpBarre.Count;
        int nbFullCell = (int) nbCellToUpdate;
        float nbDecimalCell = nbCellToUpdate - nbFullCell;

        for (currentWhiteCell = 0; currentWhiteCell < nbFullCell && currentWhiteCell < whiteHpBarre.Count; currentWhiteCell += 1)
            whiteHpBarre[currentWhiteCell].fillAmount = 1;

        whiteHpBarre[currentWhiteCell].fillAmount = nbDecimalCell;
        whiteCellIndex = currentWhiteCell;
        currentWhiteCell += 1;

        for (; currentWhiteCell < whiteHpBarre.Count; currentWhiteCell += 1)
            whiteHpBarre[currentWhiteCell].fillAmount = 0;

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
        canDash = true;
        isDie = false;
        rb.mass = 1;

        x = 0;

        animator.SetTrigger("ReturnIdle");

        fighterCam.SetActive(false);

        hp = maxHp;
        for (int i = 0; i < whiteHpBarre.Count; i += 1) {
            whiteHpBarre[i].fillAmount = 1;
            redHpBarre[i].fillAmount = 1;
        }

        currentRedCell = redHpBarre.Count - 1;
        XKey.GetComponent<DarkenKey>().ResetColorSprite();

        nbJump = maxNbJumpInAir;
    }

    public void ApplyKnockback(float knockbackForce, Vector2 knockbackDirection) {
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }

    public void PlayOneShot(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }


    public void LaunchAerialAttack(float x, float y) {
        Vector2 coordinate = new(Mathf.Abs(x), y);
        coordinate = coordinate.normalized;

        if (coordinate.x <= 0.66f && coordinate.y > 0) {
            animator.SetTrigger("Aerial Up");
        }

        if (coordinate.x > 0.66f) {
            animator.SetTrigger("Aerial Middle");
        }

        if (coordinate.x <= 0.66f && coordinate.y < 0) {
            animator.SetTrigger("Aerial Down");
        }

        if (x == 0 && y == 0) {
            Debug.Log("No Aerial Direction");
            isAttacking = false;
            return;
        }
    }

    public IEnumerator BurnEffect(float timeBurn) {
        EnableBurnEffect();
        yield return new WaitForSeconds(timeBurn);
        DisableBurnEffect();
    }

    public void EnableBurnEffect() {
        foreach (GameObject burnEffect in playerBurnVFX)
            burnEffect.SetActive(true);
    }
    public void DisableBurnEffect() {
        foreach (GameObject burnEffect in playerBurnVFX)
            burnEffect.SetActive(false);
    }

    public void HeavyEffect() {
        foreach (ParticleSystem VFX in playerHeavyVFX)
            VFX.Play();
    }



    #endregion


    #region Set Variable With Animation
    public void SetTriggerStun(float time) {
        if (!isDie) {
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
        currentRedCell = redHpBarre.Count - 1;
    }

    public void SetMenuPause(GameObject menuPauseInGame) {
        menuPause = menuPauseInGame;
    }

    public void SetXKey(GameObject XKeyInGame) {
        XKey = XKeyInGame;
    }

    public void SetArenaLimit(GameObject upperLeftLimit, GameObject lowerRightLimit) {
        this.upperLeftLimit = upperLeftLimit;
        this.lowerRightLimit = lowerRightLimit;
    }

    public void SetParryColor(Material forceShield, Color color) {
        playerParryVFX.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        playerParryVFX.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",color);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * (fighterData.playerHeight * 0.5f + 0.2f));

    }
    



}
