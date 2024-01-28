using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeOnTriggerEnter : MonoBehaviour
{
    public string val;
    private void OnTriggerEnter(Collider other) {
        Debug.Log(val);
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("exit");
    }
}
