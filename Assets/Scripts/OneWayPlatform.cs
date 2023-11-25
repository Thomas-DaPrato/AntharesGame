using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OneWayPlatform : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.position.y < this.transform.position.y+0.5 && other.tag.Equals("Player"))
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
   

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().y < 0)
            {

                other.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            }
        }
        
    }

    


}
