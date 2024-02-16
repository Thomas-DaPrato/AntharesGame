using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public static List<int> listTest = new List<int>();


    private void Start() {
        listTest.Add(0);
        listTest.Add(1);

        int i = 2; 

        for(int j = 0; j < listTest.Count; j+=1) {
            Debug.Log(listTest[j]);
            listTest.Add(i++);
            if (i > 10)
                break;
        }
    }

}
