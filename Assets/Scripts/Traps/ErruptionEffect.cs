using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErruptionEffect : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            other.GetComponent<PlayerController>().ApplyKnockback(30, new Vector2(other.GetComponent<PlayerController>().lastDirection * -1, 1));
            
        }
    }

    
}
