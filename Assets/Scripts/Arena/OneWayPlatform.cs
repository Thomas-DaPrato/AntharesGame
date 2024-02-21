using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OneWayPlatform : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        

        if (other.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        }

        
    }
   


    


}
