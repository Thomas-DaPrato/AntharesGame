using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

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

    private void Start() {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context) {
        direction = context.ReadValue<float>();
    }

    public void OnPunch(InputAction.CallbackContext context) {
        StartCoroutine(PunchAnimation());
    }

    public void OnJump(InputAction.CallbackContext context) {
        jump = context.action.triggered;
    }

    void Update() {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 move = new Vector2(direction, 0);
        if (move.x > 0)
            gameObject.transform.localRotation = Quaternion.Euler(0f,0f,0f);
        if (move.x < 0)
            gameObject.transform.localRotation = Quaternion.Euler(0f,180f,0f);
        controller.Move(move * Time.deltaTime * playerSpeed);

            

        // Changes the height position of the player..
        if (jump && groundedPlayer)
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public IEnumerator PunchAnimation() {
        animator.SetBool("Punch", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("Punch", false);
    }
}
