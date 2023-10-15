using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OneWayPlatform : MonoBehaviour
{

    float x = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.position.y < this.transform.position.y && other.tag.Equals("Player"))
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
        x = 1;
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("done");
                other.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            }
        }
        
    }

    


}
