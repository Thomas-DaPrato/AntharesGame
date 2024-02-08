using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public Test1 test1;

    private void Start() {
        StartCoroutine(test1.Test());
    }
}
