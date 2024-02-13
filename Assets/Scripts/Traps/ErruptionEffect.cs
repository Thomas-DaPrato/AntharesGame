using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErruptionEffect : MonoBehaviour
{
    public float ejectionForce = 10;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            var dir = transform.up;
            other.gameObject.GetComponent<Rigidbody>().AddForce(dir * ejectionForce);
            
            
            
        }
    }

    
}
