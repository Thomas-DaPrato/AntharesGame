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
            var rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(dir * ejectionForce);
            
            
        }
    }

    
}
