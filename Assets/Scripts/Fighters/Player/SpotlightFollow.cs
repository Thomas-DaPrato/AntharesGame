using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightFollow : MonoBehaviour
{
    public GameObject Fighter;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Fighter.transform);
        
        // test a oublier
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }
}
