using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{

    public GameObject menuPause;
    private void OnEnable() {
        Debug.Log("menuPause " + menuPause.activeSelf);
    }

}
