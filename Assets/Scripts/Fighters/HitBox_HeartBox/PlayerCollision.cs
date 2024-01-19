using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField]
    private PlayerController playerController;
    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            if (playerController.isGrounded) {
                other.gameObject.GetComponent<Rigidbody>().mass = 5;
                GetComponentInParent<Rigidbody>().mass = 5;
            }
            else {
                Debug.Log("ignore");
                Physics.IgnoreLayerCollision(7, 7);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag.Equals("Player")) {
            Debug.Log("exit");
            other.gameObject.GetComponent<Rigidbody>().mass = 1;
            GetComponentInParent<Rigidbody>().mass = 1;
            Physics.IgnoreLayerCollision(7, 7, false);
        }
    }
}
