using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErruptionEffect : MonoBehaviour
{
    public float knockbackForce = 30;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            other.GetComponent<PlayerController>().ApplyKnockback(knockbackForce, new Vector2(other.GetComponent<PlayerController>().lastDirection * -1, 1));
            
        }
    }

    
}
