using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{


    public IEnumerator Test() {
        Debug.Log("hello 1");
        yield return new WaitForSeconds(2);
        Debug.Log("hello 2");
    }

}
