using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeOnTriggerEnter : MonoBehaviour
{
    bool isGrounded;
    public LayerMask groundLayer;
    public float height;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * (height * 0.5f + 0.2f));
    }

    private void Update() {
        RaycastHit raycastHit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out raycastHit, height * 0.5f + 0.2f, groundLayer);
        if (raycastHit.transform == null)
            Debug.Log("Cube is in the air");
        else
            Debug.Log(raycastHit.transform.gameObject.name);
    }
}
