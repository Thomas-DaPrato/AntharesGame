using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlammesEffect : MonoBehaviour
{
    public float timeBurn;
    public float knockbackForce = 15;
    public float damage = 15;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().EnableBurnEffect(timeBurn);
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage, HitBox.HitBoxType.Trap);
            other.gameObject.GetComponent<PlayerController>().ApplyKnockback(knockbackForce, new Vector2(other.gameObject.GetComponent<PlayerController>().lastDirection * -1, 1));

        }
    }

}
