using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammesEffect : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(15.0f, HitBox.HitBoxType.Trap);
            collision.gameObject.GetComponent<PlayerController>().ApplyKnockback(25, new Vector2(collision.gameObject.GetComponent<PlayerController>().lastDirection * -1, 1));

        }
    }
    
}
