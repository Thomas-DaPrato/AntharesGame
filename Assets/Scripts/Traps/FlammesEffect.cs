using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlammesEffect : MonoBehaviour
{
    public float timeBurn;
    public float knockbackForce = 15;
    public float damage = 15;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(collision.gameObject.GetComponent<PlayerController>().BurnEffect(timeBurn));
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage, HitBox.HitBoxType.Trap);
            collision.gameObject.GetComponent<PlayerController>().ApplyKnockback(knockbackForce, new Vector2(collision.gameObject.GetComponent<PlayerController>().lastDirection * -1, 1));

        }
    }
    
}
