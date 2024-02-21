using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OneWayPlatform : MonoBehaviour
{
    public bool isDownFlamme = false;
    public bool flammesActif = false;

    private void OnTriggerEnter(Collider other)
    {
        float dir = other.gameObject.GetComponent<Rigidbody>().velocity.y;
        if ((dir> 0 && other.tag.Equals("Player"))||(isDownFlamme && flammesActif && other.tag.Equals("Player")))
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
