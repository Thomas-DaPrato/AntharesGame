using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    private Rigidbody rb;

    #region Intern Variable
    private bool isGrounded;
    private bool cansDashUp = true;
    private bool isDashDown = false;
    [HideInInspector]
    public bool isOnPlateform = false;

    

    private float x = 0;
    [HideInInspector]
    public float y = 0;

    private int chanceCommentateur=0;


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
    
    /*[SerializeField]
    private TrailRenderer tr;*/

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

    [SerializeField]
    private TrailRenderer tr;

    [SerializeField]
    private GameObject upperLeftLimit , lowerRightLimit;

    [SerializeField]
    private GameObject CameraSong;

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
        RaycastHit raycastHit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out raycastHit, GetFighterData().playerHeight * 0.5f + 0.2f, groundLayer);
        if (raycastHit.transform != null && raycastHit.transform.gameObject.tag.Equals("Plateform"))
            isOnPlateform = true;
        else
            isOnPlateform = false;

        SpeedController();
    }

    void FixedUpdate() {
        if (isGrounded) {
            rb.drag = groundDrag;
            nbJump = maxNbJumpInAir;
            cansDashUp = true;
        }
        else
            rb.drag = 0;

        Move();
    }



    #region Event Input System
    public void OnMoveX(InputAction.CallbackContext context) {
        if (context.started)
            animator.SetBool("Run", true);
        if (!isStun) {
            x = context.ReadValue<float>();
            if (x != 0) {
                lastDirection = x;
            }
        }
        if (context.canceled)
            animator.SetBool("Run", false);
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
        if (!isStun && context.performed)
            Jump();
    }

    public void OnDash(InputAction.CallbackContext context) {
        if (!isStun && canDash && context.performed) {
            Debug.Log("Dash");
            Dash();
        }
    }
    public void OnGoDownPlateform(InputAction.CallbackContext context)
    {
        if (!isStun && isGrounded && isOnPlateform && context.performed){
            Debug.Log("DashDown");
            DashDown();
        }
    }

    public void OnDashUp(InputAction.CallbackContext context)
    {
        if (!isStun && context.performed && cansDashUp && !isGrounded){
            cansDashUp = false;
            Debug.Log("DashUp");
            DashUp();
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
                gameObject.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            if (move.x < 0)
                gameObject.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
        }

        if (isGrounded) {
            rb.AddForce(move * playerSpeed * 10f, ForceMode.Force);
        }

        else if (!isGrounded)
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
        /*Vector3 move = new Vector3(lastDirection, 0, 0);
        rb.AddForce(move * dashDistance, ForceMode.Impulse);*/
        
        if (lastDirection < 0){

            if(transform.position.x - dashForce < upperLeftLimit.transform.position.x)
            {
                transform.DOMoveX(upperLeftLimit.transform.position.x, 0.3f);
            }
            else
            {
                transform.DOMoveX(transform.position.x - dashForce, 0.3f);
            }
            
        }else if (lastDirection > 0){

            if (transform.position.x + dashForce > lowerRightLimit.transform.position.x)
            {
                transform.DOMoveX(lowerRightLimit.transform.position.x, 0.3f);
            }
            else
            {
                transform.DOMoveX(transform.position.x + dashForce, 0.3f);
            }
            
        }

        if (y < 0)
        {
            if(transform.position.y - dashForce < lowerRightLimit.transform.position.y)
            {
                transform.DOMoveY(lowerRightLimit.transform.position.y, 0.3f);
            }
            else
            {
                transform.DOMoveY(transform.position.y - dashForce, 0.3f);
            }
            
        }
        else if (y > 0)
        {
            if (transform.position.y + dashForce > upperLeftLimit.transform.position.y)
            {
                transform.DOMoveY(upperLeftLimit.transform.position.y, 0.3f);
            }
            else
            {
                transform.DOMoveY(transform.position.y + dashForce, 0.3f);
            }
            
        }

        canDash = false;
        tr.emitting = true;

        StartCoroutine(DashCoolDown());
        StartCoroutine(StopDash());
    }
    public void DashDown()
    {
        if(transform.position.y - dashForce < lowerRightLimit.transform.position.y)
        {
            transform.DOMoveY(lowerRightLimit.transform.position.y, 0.2f);
        }
        else
        {
            transform.DOMoveY(transform.position.y - dashForce, 0.2f);
        }
        
        tr.emitting = true;
        isDashDown=true;
        StartCoroutine(StopDash());
        //Vector3 move = new Vector3(0, -1, 0);
        //rb.AddForce(move* dashDistance/2, ForceMode.Impulse);


    }
    public void DashUp()
    {
        if (transform.position.y + dashForce > upperLeftLimit.transform.position.y)
        {
            transform.DOMoveY(upperLeftLimit.transform.position.y, 0.3f);
        }
        else
        {
            transform.DOMoveY(transform.position.y + dashForce, 0.3f);
        }
        tr.emitting = true;
        StartCoroutine(StopDash());


    }

    public IEnumerator DashCoolDown() {
        yield return new WaitForSeconds(1);
        canDash = true;
        
    }

    public IEnumerator StopDash()
    {
        if (isDashDown)
        {
            yield return new WaitForSeconds(0.2f);
            isDashDown = false;
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
        }
        
        tr.emitting = false;
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

                chanceCommentateur = Random.Range(0, 3);
                if (chanceCommentateur == 1)
                {
                    CameraSong.GetComponent<CommentateurCamera>().CommentateurCoups();
                }
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
            case HitBox.HitBoxType.Trap:
                hp -= percentageDamage * maxHp / 100.0f;
                if (hp <= 0)
                    hp = 1;

                chanceCommentateur = Random.Range(0, 5);
                if (chanceCommentateur == 1)
                {
                    CameraSong.GetComponent<CommentateurCamera>().CommentateurPiege();
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
