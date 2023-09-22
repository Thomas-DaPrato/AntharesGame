using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    public bool isAttacking = false;



    [SerializeField]
    private FighterData fighterData;
    public FighterData GetFighterData() { return fighterData; }

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private Animator animator;



    private float direction = 0;
    private bool jump = false;

    private int hp = 10;

    private void Start() {
        controller = gameObject.GetComponent<CharacterController>();
        
    }

    public void OnMove(InputAction.CallbackContext context) {
        direction = context.ReadValue<float>();
    }

    public void OnHeavyAttack(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking) {
            Debug.Log("Heavy");
            isAttacking = true;
            animator.SetTrigger("HeavyAttack");
        }
    }
   
    public void OnMiddleAttack(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking){
            Debug.Log("Middle");
            isAttacking = true;
            animator.SetTrigger("MiddleAttack");
        }
    }
    public void OnLightAttack(InputAction.CallbackContext context) {
        if (context.performed && !isAttacking){
            Debug.Log("Light");
            isAttacking = true;
            animator.SetTrigger("LightAttack");
        }
        
    }

    public void OnJump(InputAction.CallbackContext context) {
        jump = context.action.triggered;
    }

    void Update() {
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
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    public void TakeDamage(int damage) {
        if ((hp -= damage) <= 0)
            Debug.Log("T MORT !!!!!");
        else
            Debug.Log("hp : " + hp);
    }

    public void SetIsAttackingFalse() {
        isAttacking = false;
    }
    
}
