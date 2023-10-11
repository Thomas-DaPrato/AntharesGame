using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public enum HitBoxType {
        Heavy,
        Middle,
        Light,
        Aerial
    }

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private HitBoxType type;

    
    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("HeartBox") && !other.GetComponentInParent<PlayerController>().isParrying) {
            switch (type) {
                case HitBoxType.Heavy:
                    other.GetComponent<HeartBox>().TakeDamage(playerController.GetFighterData().heavyAttack.damage);
                    other.GetComponentInParent<PlayerController>().ApplyKnockback(playerController.GetFighterData().heavyAttack.knockback, playerController.lastDirection);
                    break;
                case HitBoxType.Middle:
                    other.GetComponent<HeartBox>().TakeDamage(playerController.GetFighterData().middleAttack.damage);
                    other.GetComponentInParent<PlayerController>().ApplyKnockback(playerController.GetFighterData().middleAttack.knockback, playerController.lastDirection);
                    break;
                case HitBoxType.Light:
                    other.GetComponent<HeartBox>().TakeDamage(playerController.GetFighterData().lightAttack.damage);
                    other.GetComponentInParent<PlayerController>().ApplyKnockback(playerController.GetFighterData().lightAttack.knockback, playerController.lastDirection);
                    break;
                case HitBoxType.Aerial:
                    other.GetComponent<HeartBox>().TakeDamage(playerController.GetFighterData().aerialsAttack.damage);
                    other.GetComponentInParent<PlayerController>().ApplyKnockback(playerController.GetFighterData().aerialsAttack.knockback, playerController.lastDirection);
                    break;
                default:
                    Debug.Log("ERRORR : type " + type + " is not recognized");
                    break;
            }
        }
        else if (other.tag.Equals("HeartBox") && other.GetComponentInParent<PlayerController>().isParrying) { 
            Debug.Log(gameObject.transform.parent.parent.name + " My ennemy...y...y is parrying");
            playerController.SetTriggerInterrupt();
        }
    }
}
