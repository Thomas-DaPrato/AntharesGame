using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeOnTriggerEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log("entry");
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("exit");
    }
}
