using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.VFX;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using MoreMountains.Feedbacks;
using Lofelt.NiceVibrations;

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
    private bool isDashing = false;
    public bool isDie = false;
    private bool isOnPlateform = false;
    private bool isRunning = false;
    private bool isResetRumble = false;
    [HideInInspector]
    public bool lightAttackCanTouch = true;

    public int chanceComCoup=3;
    public int chanceComPiege = 5;
    public int chanceFouleCoup = 3;
    public int chanceFoulePiege=5;

    private float x = 0;
    private float xAerial = 0;
    private float yAerial = 0;

    private float xDash = 0;
    private float yDash = 0;

    private int chanceCommentateur = 0;
    private int chanceFoule = 0;


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
    //[HideInInspector]
    public bool canParry = true;
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
    private List<ParticleSystem> playerHeavyVFXRight;
    [SerializeField]
    private List<ParticleSystem> playerHeavyVFXLeft;    
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

    
    private GameObject soundManager;

    private GameObject menuPause;
    private GameObject UICombat;
    private Image timer;

    
    public SetPlayerIcone playerNameIcone;

    private GameObject XKey;

    [SerializeField]
    private PlayerInput opposingPlayerInput;

    public PlayerController otherPlayer;

    [SerializeField]
    private MMF_Player cesarVictory;
    [SerializeField]
    private MMF_Player dianeVictory;

    [Space(20)]
    [Header("UI feedback")]
    public MMF_Player heavyUIFeedback;
    public MMF_Player mediumUIFeedback;
    public MMF_Player lightUIFeedback;
    public MMF_Player charaUIFeedback;
    public MMF_Player skullUIFeedbackStart;
    public MMF_Player skullUIFeedbackLoop1;
    public MMF_Player skullUIFeedbackLoop2;
    public MMF_Player YUIFeedbackLoop;
    public MMF_Player BUIFeedbackLoop;

    [Space(20)]
    [Header("Damaged feedback")]
    [SerializeField]
    public MMF_Player damagedLightFeedbacks;
    [SerializeField]
    public MMF_Player damagedMediumFeedbacks;
    [SerializeField]
    public MMF_Player damagedHeavyFeedbacks;
    [SerializeField]
    public MMF_Player damagedTrapSawFeedbacks;

    /*[Space(20)]
    [Header("Damaged SFX")]
    [SerializeField]
    public MMF_Player DamagedCesarLightSound;
    [SerializeField]
    public MMF_Player DamagedCesarMediumSound;
    [SerializeField]
    public MMF_Player DamagedCesarHeavySound;

    [Space(10)]
    [SerializeField]
    public MMF_Player DamagedDianeLightSound;
    [SerializeField]
    public MMF_Player DamagedDianeMediumSound;
    [SerializeField]
    public MMF_Player DamagedDianeHeavySound;*/

    #region Movement Variable


    [Space(20)]

    [Header("Mouvement")]
    [SerializeField]
    private float moveForceNotCollide;
    [SerializeField]
    private float moveForceCollide;
    private float moveForce;
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

    [SerializeField]
    private float dashTime;
    [SerializeField]
    private float GoDownPlatformTime;
    [SerializeField]
    private float stopDashTime;
    
    


    [Space(20)]

    [Header("Friction")]
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float groundDrag;


    [SerializeField]
    private LayerMask playerLayer;
    #endregion



    public void victoryCesar()
    {
        cesarVictory.PlayFeedbacks();
    }

    public void victoryDiane()
    {
        dianeVictory.PlayFeedbacks();
    }


    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        nbJump = maxNbJumpInAir;
        hp = maxHp;
        moveForce = moveForceNotCollide;
        dashForceVal = dashForce;
        soundManager = GameObject.Find("Game_Final_SFXManager");
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycastHit, GetFighterData().playerHeight * 0.5f + 0.2f, groundLayer);

        animator.SetBool("IsAttacking", isAttacking);
        animator.SetBool("IsDashing", isDashing);

        if (isGrounded && isRunning && !isStun)
            animator.SetBool("Run", true);
        animator.SetBool("In Air", !isGrounded);

        if (raycastHit.transform != null && raycastHit.transform.gameObject.CompareTag("Plateform"))
            isOnPlateform = true;
        else
            isOnPlateform = false;

        if (!isAttacking && !isParrying)
        {
            RotateComponent();
        }

        if (canDecreaseRedHpBarre)
        {
            if (currentRedCell > whiteCellIndex)
            {
                redHpBarre[currentRedCell].fillAmount -= rateRedHpBarre;
                if (redHpBarre[currentRedCell].fillAmount == 0)
                    currentRedCell -= 1;
            }
            else if (currentRedCell == whiteCellIndex && redHpBarre[currentRedCell].fillAmount > whiteHpBarre[whiteCellIndex].fillAmount)
                redHpBarre[currentRedCell].fillAmount -= rateRedHpBarre;
            else
                canDecreaseRedHpBarre = false;
        }

        if (!isResetRumble && Gamepad.all.Count > 1)
            StartCoroutine(LaunchResetRumble());

        SpeedController();
    }

    public IEnumerator LaunchResetRumble() {
        isResetRumble = true;
        GetComponent<PlayerInput>().GetDevice<Gamepad>().SetMotorSpeeds(0, 0);
        yield return new WaitForSeconds(1);
        isResetRumble = false;
    }

    private void RotateComponent()
    {
        if (lastDirection > 0)
        {
            animator.SetBool("Mirror", false);
            hitBoxs.transform.localRotation = Quaternion.Euler(0f, 0, 0f);
        }
        if (lastDirection < 0)
        {
            animator.SetBool("Mirror", true);
            hitBoxs.transform.localRotation = Quaternion.Euler(0f, 180, 0f);
        }
    }

    void FixedUpdate()
    {
        if (isGrounded && rb.velocity.y <= 0)
        {
            rb.drag = groundDrag;
            nbJump = maxNbJumpInAir;
        }
        else
        {
            rb.drag = 0;
        }

        if (!isAttacking && !isStun && !isParrying)
            Move();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            moveForce = moveForceCollide;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            moveForce = moveForceNotCollide;
    }


    #region Event Input System
    #region Map Player
    public void OnMoveX(InputAction.CallbackContext context)
    {
        x = context.ReadValue<float>();
        xDash = x;
        SetLastDirection(x);
        isRunning = true;
        if (context.canceled)
        {
            StartCoroutine(SetIsRunningFalse());
        }
    }

    public IEnumerator SetIsRunningFalse()
    {
        yield return new WaitForSeconds(0.1f);
        if (x == 0)
        {
            isRunning = false;
            animator.SetBool("Run", false);

        }
    }

    private void SetLastDirection(float val)
    {
        if (val > 0)
            val = 1;
        else if (val < 0)
            val = -1;
        else
            val = 0;

        if (val != 0)
            lastDirection = val;
    }

    public void OnMoveY(InputAction.CallbackContext context)
    {
        if (!isStun)
        {
            yAerial = context.ReadValue<float>();
            yDash = yAerial;
        }
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking && !isStun)
        {
            isAttacking = true;
            if (isGrounded) {
                HeavyEffect();
                animator.SetTrigger("HeavyAttack");
            }
            else if (!isGrounded)
                LaunchAerialAttack(x, yAerial);
        }
    }

    public void OnMiddleAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking && !isStun)
        {
            isAttacking = true;
            if (isGrounded)
            {
                animator.SetTrigger("MiddleAttack");
            }
            else if (!isGrounded)
                LaunchAerialAttack(x, yAerial);
        }
    }
    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking && !isStun)
        {
            isAttacking = true;
            if (isGrounded)
            {
                animator.SetTrigger("LightAttack");
            }
            else if (!isGrounded)
                LaunchAerialAttack(x, yAerial);
        }

    }

    public void OnRightStick(InputAction.CallbackContext context)
    {
        if (context.performed && !isStun && !isGrounded && !isAttacking)
        {
            isAttacking = true;

            xAerial = context.ReadValue<Vector2>()[0];
            yAerial = context.ReadValue<Vector2>()[1];

            SetLastDirection(xAerial);
            RotateComponent();

            LaunchAerialAttack(xAerial, yAerial);
        }
    }

    public void OnParry(InputAction.CallbackContext context)
    {
        if (context.performed && canParry && !isStun && !isDashing && !isDashDown)
        {
            isParrying = true;
            Debug.Log(gameObject.name + " Parry");
            animator.SetTrigger("Parry");
            canParry = false;
        }
    }

    IEnumerator LaunchCoolDownParry() {
        yield return new WaitForSeconds(1);
        canParry = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!isStun && !isAttacking && !isParrying && context.performed)
        {
            Jump();

        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {

        if (!isAttacking && !isParrying && !isStun && canDash && context.performed)
        {
            isDashing = true;
            Dash();
        }
    }
    public void OnGoDownPlateform(InputAction.CallbackContext context)
    {
        if (!isStun && isGrounded && isOnPlateform && !isAttacking && !isParrying && context.performed)
        {
            isDashDown = true;
            SpeedDown();
            playerDashVFX.SetActive(true);
        }
    }


    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UICombat.SetActive(false);
            timer.enabled = false;
            menuPause.SetActive(true);
            gameManager.DisplayBlurEffect();
            Time.timeScale = 0;
            GameManager.SetActionMap("OptionSwap");
        }
    }
    #endregion

    #region Map Options Swapper
    public void OnChangePannel(InputAction.CallbackContext context)
    {
        if (context.performed)
            menuPause.GetComponent<OptionsSwapper>().OnChangePannel(context);
    }

    public void OnReturn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!menuPause.GetComponent<OptionsSwapper>().options.activeSelf)
                menuPause.GetComponent<MenuPause>().Resume();
            else
                menuPause.GetComponent<OptionsSwapper>().OnReturn(context);
        }
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        if (context.performed && !menuPause.GetComponent<OptionsSwapper>().options.activeSelf)
            menuPause.GetComponent<MenuPause>().Resume();
    }

    #endregion
    #endregion

    #region Player Movement
    public void Jump()
    {
        if (nbJump > 0)
        {
            if (isGrounded)
                animator.SetTrigger("Jump");
            else
                animator.SetTrigger("Salto");
            rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            nbJump -= 1;
        }
    }

    public void Move()
    {
        Vector3 move = new(x, 0, 0);


        if (isGrounded)
        {
            rb.AddForce(moveForce * groundControl * playerSpeed * move, ForceMode.Force);
        }

        else if (!isGrounded)
            rb.AddForce(moveForce * airControl * playerSpeed * move, ForceMode.Force);

    }

    public void SpeedController()
    {
        Vector3 flatVelocity = new(rb.velocity.x, 0f, 0f);

        if (flatVelocity.magnitude > playerSpeed)
        {
            Vector3 limitedSpeed = flatVelocity.normalized * playerSpeed;
            rb.velocity = new Vector3(limitedSpeed.x, rb.velocity.y, 0f);
        }
    }

    public void Dash()
    {

        //
        if ((xDash < -0.4 || xDash > 0.4) && (yDash < -0.4 || yDash > 0.4))
        {
            dashForce = Mathf.Sqrt(dashForce * dashForce / 2);
            animator.SetTrigger("DashDiag");
        }

        if (xDash < -0.4)
        {

            if (transform.position.x - dashForce < upperLeftLimit.transform.position.x)
            {
                transform.DOMoveX(upperLeftLimit.transform.position.x, dashTime);
                canDash = false;
            }
            else
            {
                transform.DOMoveX(transform.position.x - dashForce, dashTime);
                canDash = false;
            }
            animator.SetTrigger("Dash");

        }
        //
        else if (xDash > 0.4)
        {

            if (transform.position.x + dashForce > lowerRightLimit.transform.position.x)
            {
                transform.DOMoveX(lowerRightLimit.transform.position.x, dashTime);
                canDash = false;
            }
            else
            {
                transform.DOMoveX(transform.position.x + dashForce, dashTime);
                canDash = false;
            }
            animator.SetTrigger("Dash");

        }
        //
        if (yDash < -0.4)
        {
            if (transform.position.y - dashForce < lowerRightLimit.transform.position.y)
            {
                transform.DOMoveY(lowerRightLimit.transform.position.y, dashTime);
                canDash = false;
            }
            else
            {
                transform.DOMoveY(transform.position.y - dashForce, dashTime);
                canDash = false;
            }
            animator.SetTrigger("DashDown");

        }
        else if (yDash > 0.4)
        {

            if (transform.position.y + dashForce > upperLeftLimit.transform.position.y)
            {
                transform.DOMoveY(upperLeftLimit.transform.position.y, dashTime);
                canDash = false;
            }
            else
            {
                transform.DOMoveY(transform.position.y + dashForce, dashTime);
                canDash = false;
            }
            animator.SetTrigger("DashUp");
        }
        if (canDash == false)
        {
            playerDashVFX.SetActive(true);
        }

        dashForce = dashForceVal;

        StartCoroutine(DashCoolDown());
        StartCoroutine(StopDash());
    }
    public void SpeedDown()
    {
        gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        if (transform.position.y - downForcePlateforme < lowerRightLimit.transform.position.y)
        {
            transform.DOMoveY(lowerRightLimit.transform.position.y, GoDownPlatformTime);
        }
        else
        {
            transform.DOMoveY(transform.position.y - downForcePlateforme, GoDownPlatformTime);
        }
        StartCoroutine(StopDash());


    }

    public IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(stopDashTime);
        canDash = true;

    }

    public IEnumerator StopDash()
    {
        if (isDashDown)
        {
            yield return new WaitForSeconds(GoDownPlatformTime);
            isDashDown = false;
        }
        else
        {
            yield return new WaitForSeconds(dashTime);
        }
        moveForce = moveForceNotCollide;
        isDashing = false;
        playerDashVFX.SetActive(false);

    }

    public IEnumerator StunCoolDown(float time)
    {
        isStun = true;
        yield return new WaitForSeconds(time);
        animator.SetTrigger("ReturnIdle");
        isStun = false;
        isAttacking = false;
        isParrying = false;
    }
    #endregion

    #region Player Fonctions
    public void TakeDamage(float percentageDamage, HitBox.HitBoxType type, string trapName = null, string opponentName = null)
    {
        print(type);
        print(trapName);
        if (isInvicible)
            return;
        //GamepadRumbler.SetCurrentGamepad(opposingPlayerInput.GetDevice<Gamepad>().deviceId);
        switch (type)
        {
            case HitBox.HitBoxType.Heavy:
                if (hp <= 20.0f * maxHp / 100.0f)
                    PlayerDie();
                else
                    hp -= percentageDamage * maxHp / 100.0f;

                chanceCommentateur = Random.Range(0, chanceComCoup);
                if (chanceCommentateur == 1)
                {
                    soundManager.GetComponent<Commentateur>().CommentateurCoups();
                }
                chanceFoule = Random.Range(0, chanceFouleCoup);
                if (chanceFoule == 1)
                {
                    soundManager.GetComponent<Foule>().FouleScream();
                }
                GamepadRumbler.SetCurrentGamepad(opposingPlayerInput.GetDevice<Gamepad>().deviceId);
                damagedHeavyFeedbacks.PlayFeedbacks();

                /*Debug.Log("avant");
                if (opponentName == ("Diane"))
                {
                    DamagedDianeHeavySound.PlayFeedbacks();
                }
                else if (opponentName == ("Cesar"))
                {
                    DamagedCesarHeavySound.PlayFeedbacks();
                }
                Debug.Log("après");*/
                heavyUIFeedback.InitialDelay = GetFighterData().heavyAttack.hitFreezeTime;
                heavyUIFeedback.PlayFeedbacks();
                charaUIFeedback.InitialDelay = GetFighterData().heavyAttack.hitFreezeTime;
                charaUIFeedback.PlayFeedbacks();
                break;
            case HitBox.HitBoxType.Middle:
                if (hp <= 10.0f * maxHp / 100.0f)
                    PlayerDie();
                else
                    hp -= percentageDamage * maxHp / 100.0f;
                damagedMediumFeedbacks.PlayFeedbacks();
                /*if (opponentName.Equals("Diane"))
                {
                    DamagedDianeMediumSound.PlayFeedbacks();
                }
                else if (opponentName.Equals("Cesar"))
                {
                    DamagedCesarMediumSound.PlayFeedbacks();
                }*/
                mediumUIFeedback.InitialDelay = GetFighterData().middleAttack.hitFreezeTime;
                mediumUIFeedback.PlayFeedbacks();
                charaUIFeedback.InitialDelay = GetFighterData().middleAttack.hitFreezeTime;
                charaUIFeedback.PlayFeedbacks();
                break;
            case HitBox.HitBoxType.Aerial:
                if (hp <= 10.0f * maxHp / 100.0f)
                    PlayerDie();
                else
                    hp -= percentageDamage * maxHp / 100.0f;
                damagedMediumFeedbacks.PlayFeedbacks();

                /*if (opponentName.Equals("Diane"))
                {
                    DamagedDianeMediumSound.PlayFeedbacks();
                }
                else if (opponentName.Equals("Cesar"))
                {
                    DamagedCesarMediumSound.PlayFeedbacks();
                }*/
                mediumUIFeedback.InitialDelay = GetFighterData().aerialAttack.hitFreezeTime;
                mediumUIFeedback.PlayFeedbacks();
                charaUIFeedback.InitialDelay = GetFighterData().aerialAttack.hitFreezeTime;
                charaUIFeedback.PlayFeedbacks();
                break;
            case HitBox.HitBoxType.Trap:
                if(trapName != "Geyser")
                {
                    hp -= percentageDamage * maxHp / 100.0f;
                    damagedTrapSawFeedbacks.PlayFeedbacks();
                    lightUIFeedback.InitialDelay = 0;
                    lightUIFeedback.PlayFeedbacks();
                    charaUIFeedback.InitialDelay = 0;
                    charaUIFeedback.PlayFeedbacks();
                }
                
                chanceCommentateur = Random.Range(0, chanceComPiege);
                if (chanceCommentateur == 1)
                {
                    soundManager.GetComponent<Commentateur>().CommentateurPiege(trapName);
                }
                chanceFoule = Random.Range(0, chanceFoulePiege);
                if (chanceFoule == 1)
                {
                    soundManager.GetComponent<Foule>().FouleScream();
                }
                break;
            default:
                if (lightAttackCanTouch)
                {
                    damagedLightFeedbacks.PlayFeedbacks();
                    /*if (opponentName.Equals("Diane"))
                    {
                        DamagedDianeLightSound.PlayFeedbacks();
                    }
                    else if (opponentName.Equals("Cesar"))
                    {
                        DamagedCesarLightSound.PlayFeedbacks();
                    }
                    */
                    lightUIFeedback.InitialDelay = GetFighterData().lightAttack.hitFreezeTime;
                    lightUIFeedback.PlayFeedbacks();
                    charaUIFeedback.InitialDelay = GetFighterData().lightAttack.hitFreezeTime;
                    charaUIFeedback.PlayFeedbacks();
                }
                hp -= percentageDamage * maxHp / 100.0f;
                break;
        }

        if (!isDie && hp <= 0)
            hp = 1;
        if ((hp / maxHp) * 100 <= 20 && (hp / maxHp) * 100 > 10)
        {


            bool playSkullLoop1Other = false;
            bool playSkullLoop2Other = false;
            bool playYLoopOther = false;
            bool playBLoopOther = false;

            skullUIFeedbackLoop1.InitialDelay = 0;
            skullUIFeedbackLoop1.StopFeedbacks();
            skullUIFeedbackLoop1.RestoreInitialValues();

            skullUIFeedbackLoop2.InitialDelay = 0;
            skullUIFeedbackLoop2.StopFeedbacks();
            skullUIFeedbackLoop2.RestoreInitialValues();

            YUIFeedbackLoop.InitialDelay = 0;
            YUIFeedbackLoop.StopFeedbacks();
            YUIFeedbackLoop.RestoreInitialValues();

            BUIFeedbackLoop.InitialDelay = 0;
            BUIFeedbackLoop.StopFeedbacks();
            BUIFeedbackLoop.RestoreInitialValues();

            if (otherPlayer.skullUIFeedbackLoop1.IsPlaying)
            {
                otherPlayer.skullUIFeedbackLoop1.StopFeedbacks();
                otherPlayer.skullUIFeedbackLoop1.RestoreInitialValues();
                otherPlayer.skullUIFeedbackLoop1.InitialDelay = skullUIFeedbackStart.TotalDuration;

                otherPlayer.BUIFeedbackLoop.StopFeedbacks();
                otherPlayer.BUIFeedbackLoop.RestoreInitialValues();
                otherPlayer.BUIFeedbackLoop.InitialDelay = skullUIFeedbackStart.TotalDuration;

                playBLoopOther = true;
                playSkullLoop1Other = true;
            }
            else if (otherPlayer.skullUIFeedbackLoop2.IsPlaying)
            {
                otherPlayer.skullUIFeedbackLoop2.StopFeedbacks();
                otherPlayer.skullUIFeedbackLoop2.RestoreInitialValues();
                otherPlayer.skullUIFeedbackLoop2.InitialDelay = skullUIFeedbackStart.TotalDuration;

                otherPlayer.BUIFeedbackLoop.StopFeedbacks();
                otherPlayer.BUIFeedbackLoop.RestoreInitialValues();
                otherPlayer.BUIFeedbackLoop.InitialDelay = skullUIFeedbackStart.TotalDuration;

                otherPlayer.YUIFeedbackLoop.StopFeedbacks();
                otherPlayer.YUIFeedbackLoop.RestoreInitialValues();
                otherPlayer.YUIFeedbackLoop.InitialDelay = skullUIFeedbackStart.TotalDuration;

                playYLoopOther = true;
                playBLoopOther = true;
                playSkullLoop2Other = true;
            }

            skullUIFeedbackStart.PlayFeedbacks();
            skullUIFeedbackLoop1.InitialDelay = skullUIFeedbackStart.TotalDuration;
            BUIFeedbackLoop.InitialDelay = skullUIFeedbackStart.TotalDuration;
            if (playYLoopOther)
                otherPlayer.YUIFeedbackLoop.PlayFeedbacks();
            if (playBLoopOther)
                otherPlayer.BUIFeedbackLoop.PlayFeedbacks();
            if (playSkullLoop1Other)
                otherPlayer.skullUIFeedbackLoop1.PlayFeedbacks();
            if (playSkullLoop2Other)
                otherPlayer.skullUIFeedbackLoop2.PlayFeedbacks();
            
            BUIFeedbackLoop.PlayFeedbacks();
            skullUIFeedbackLoop1.PlayFeedbacks();
        }

        if ((hp / maxHp) * 100 <= 10)
        {
            XKey.GetComponent<DarkenKey>().DarkenXKey();
            lightAttackCanTouch = false;

            bool playSkullLoop1Other = false;
            bool playSkullLoop2Other = false;
            bool playYLoopOther = false;
            bool playBLoopOther = false;

            skullUIFeedbackLoop1.InitialDelay = 0;
            skullUIFeedbackLoop1.StopFeedbacks();
            skullUIFeedbackLoop1.RestoreInitialValues();

            skullUIFeedbackLoop2.InitialDelay = 0;
            skullUIFeedbackLoop2.StopFeedbacks();
            skullUIFeedbackLoop2.RestoreInitialValues();

            YUIFeedbackLoop.InitialDelay = 0;
            YUIFeedbackLoop.StopFeedbacks();
            YUIFeedbackLoop.RestoreInitialValues();

            BUIFeedbackLoop.InitialDelay = 0;
            BUIFeedbackLoop.StopFeedbacks();
            BUIFeedbackLoop.RestoreInitialValues();



            if (otherPlayer.skullUIFeedbackLoop1.IsPlaying)
            {
                otherPlayer.skullUIFeedbackLoop1.StopFeedbacks();
                otherPlayer.skullUIFeedbackLoop1.RestoreInitialValues();
                otherPlayer.skullUIFeedbackLoop1.PlayFeedbacks();
                otherPlayer.YUIFeedbackLoop.StopFeedbacks();
                otherPlayer.YUIFeedbackLoop.RestoreInitialValues();
                playSkullLoop1Other = true;
                playBLoopOther = true;

            }
            else if (otherPlayer.skullUIFeedbackLoop2.IsPlaying)
            {
                otherPlayer.skullUIFeedbackLoop2.StopFeedbacks();
                otherPlayer.skullUIFeedbackLoop2.RestoreInitialValues();

                otherPlayer.BUIFeedbackLoop.StopFeedbacks();
                otherPlayer.BUIFeedbackLoop.RestoreInitialValues();

                otherPlayer.YUIFeedbackLoop.StopFeedbacks();
                otherPlayer.YUIFeedbackLoop.RestoreInitialValues();

                playSkullLoop2Other = true;
                playYLoopOther = true;
                playBLoopOther = true;
            }

            if (playYLoopOther)
                otherPlayer.YUIFeedbackLoop.PlayFeedbacks();
            if (playBLoopOther)
                otherPlayer.BUIFeedbackLoop.PlayFeedbacks();
            if (playSkullLoop1Other)
                otherPlayer.skullUIFeedbackLoop1.PlayFeedbacks();
            if (playSkullLoop2Other)
                otherPlayer.skullUIFeedbackLoop2.PlayFeedbacks();
            YUIFeedbackLoop.PlayFeedbacks();
            BUIFeedbackLoop.PlayFeedbacks();
            skullUIFeedbackLoop2.PlayFeedbacks();

        }


        StartCoroutine(UpdateHpBarre());

    }


    public IEnumerator UpdateHpBarre()
    {
        int currentWhiteCell;
        float percentageHP = hp / maxHp;

        float nbCellToUpdate = percentageHP * whiteHpBarre.Count;
        int nbFullCell = (int)nbCellToUpdate;
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

    public void PlayerDie()
    {
        Debug.Log("T MORT !!!!!");
        hp = 0;
        isDie = true;
        gameManager.DisablePlayerIcone();
        gameManager.EndRound(playerName);
    }

    public void ResetFighter(int lastDirection)
    {
        this.lastDirection = lastDirection;
        isAttacking = false;
        isParrying = false;
        isRunning = false;
        canDash = true;
        isDie = false;
        gameManager.EnablePlayerIcone();
        lightAttackCanTouch = true;
        moveForce = moveForceNotCollide;

        x = 0;

        animator.SetTrigger("ReturnIdle");

        fighterCam.SetActive(false);

        hp = maxHp;
        for (int i = 0; i < whiteHpBarre.Count; i += 1)
        {
            whiteHpBarre[i].fillAmount = 1;
            redHpBarre[i].fillAmount = 1;
        }

        currentRedCell = redHpBarre.Count - 1;
        XKey.GetComponent<DarkenKey>().ResetColorSprite();

        nbJump = maxNbJumpInAir;
    }

    public void ApplyKnockback(float knockbackForce, Vector2 knockbackDirection)
    {
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }

    public void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }


    public void LaunchAerialAttack(float x, float y)
    {
        Vector2 coordinate = new(Mathf.Abs(x), y);
        coordinate = coordinate.normalized;

        if (coordinate.x <= 0.9f && coordinate.y > 0)
        {
            animator.SetTrigger("Aerial Up");
        }

        if (coordinate.x > 0.9f)
        {
            animator.SetTrigger("Aerial Middle");
        }

        if (coordinate.x <= 0.9f && coordinate.y < 0)
        {
            animator.SetTrigger("Aerial Down");
        }

        if (x == 0 && y == 0)
        {
            Debug.Log("No Aerial Direction");
            isAttacking = false;
            return;
        }
    }

    public void EnableBurnEffect(float timeBurn)
    {
        StartCoroutine(BurnEffect(timeBurn));
    }

    public IEnumerator BurnEffect(float timeBurn)
    {
        EnableBurnEffect();
        yield return new WaitForSeconds(timeBurn);
        DisableBurnEffect();
    }

    public void EnableBurnEffect()
    {
        Debug.Log("enableBurn");
        foreach (GameObject burnEffect in playerBurnVFX)
            burnEffect.SetActive(true);
    }
    public void DisableBurnEffect()
    {
        Debug.Log("disableBurn");
        foreach (GameObject burnEffect in playerBurnVFX)
            burnEffect.SetActive(false);
    }

    public void HeavyEffect()
    {
       if(lastDirection > 0)
            foreach (ParticleSystem VFX in playerHeavyVFXRight)
                VFX.Play();
        if(lastDirection < 0)
            foreach (ParticleSystem VFX in playerHeavyVFXLeft)
                VFX.Play();
    }



    #endregion


    #region Set Variable With Animation
    public void SetTriggerStun(float time)
    {
        if (!isDie)
        {
            StartCoroutine(StunCoolDown(time));
            animator.SetTrigger("Stun");
        }

    }

    public void SetIsAttackingFalse()
    {
        isAttacking = false;
    }


    public void SetIsParryingTrue()
    {
        isParrying = true;
        canDash = false;
    }
    public void SetIsParryingFalse()
    {
        isParrying = false;
        canDash = true;
        StartCoroutine(LaunchCoolDownParry());
    }


    public void SetIsStunFalse()
    {
        isStun = false;

    }
    #endregion

    public void SetIsGrounded(bool val)
    {
        isGrounded = val;
    }

    public void SetUIFighter(List<Image> whiteHpBarreInGame, List<Image> redHpBarreInGame, GameObject menuPauseInGame, GameObject UICombatInGame, GameObject XKeyInGame, Image imageTimer, string playerName)
    {
        whiteHpBarre = whiteHpBarreInGame;
        redHpBarre = redHpBarreInGame;
        currentRedCell = redHpBarre.Count - 1;

        playerNameIcone.Init(playerName);

        menuPause = menuPauseInGame;
        UICombat = UICombatInGame;
        XKey = XKeyInGame;
        timer = imageTimer;
    }

    public void SetPlayerInput(PlayerInput playerInput) {
        opposingPlayerInput = playerInput;
    }

    public void SetArenaLimit(GameObject upperLeftLimit, GameObject lowerRightLimit)
    {
        this.upperLeftLimit = upperLeftLimit;
        this.lowerRightLimit = lowerRightLimit;
    }

    public void SetParryColor(Color color)
    {
        playerParryVFX.GetComponent<Shield>().SetShieldColor(color);
    }
    public void SetDashColor(Color color)
    {
        playerDashVFX.GetComponent<ParticleSystem>().startColor = color * 8;
        var trails = playerDashVFX.GetComponent<ParticleSystem>().trails;
        trails.colorOverLifetime = color * 10;
    }
    public void SetHeavyColor(Color color)
    {
        foreach (ParticleSystem VFX in playerHeavyVFXRight){
            VFX.startColor = color * 8;
            var trails = VFX.trails;
            trails.colorOverLifetime = color * 10;
        }
        foreach (ParticleSystem VFX in playerHeavyVFXLeft){
            VFX.startColor = color * 8;
            var trails = VFX.trails;
            trails.colorOverLifetime = color * 10;    
        }
    }
    public void ShieldOnOff()
    {
        playerParryVFX.GetComponent<Shield>().OpenCloseShield();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * (fighterData.playerHeight * 0.5f + 0.2f));
    }
}
