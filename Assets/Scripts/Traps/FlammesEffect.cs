using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammesEffect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().TakeDamage(15.0f, HitBox.HitBoxType.Trap);
            other.GetComponent<PlayerController>().ApplyKnockback(25, new Vector2(other.GetComponent<PlayerController>().lastDirection * -1, 1));
            
        }
    }
}
