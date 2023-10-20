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
            FighterData fighterData = playerController.GetFighterData();
            switch (type) {
                case HitBoxType.Heavy:
                    AttackManager(fighterData.heavyAttack.damage, fighterData.heavyAttack.knockback, fighterData.heavyAttack.stunTime ,other);
                    break;
                case HitBoxType.Middle:
                    AttackManager(fighterData.middleAttack.damage, fighterData.middleAttack.knockback, fighterData.middleAttack.stunTime, other);
                    break;
                case HitBoxType.Light:
                    AttackManager(fighterData.lightAttack.damage, fighterData.lightAttack.knockback, fighterData.lightAttack.stunTime, other);
                    break;
                case HitBoxType.Aerial:
                    AttackManager(fighterData.aerialsAttack.damage, fighterData.aerialsAttack.knockback, fighterData.aerialsAttack.stunTime, other);
                    break;
                default:
                    Debug.Log("ERRORR : type " + type + " is not recognized");
                    break;
            }
        }
        else if (other.tag.Equals("HeartBox") && other.GetComponentInParent<PlayerController>().isParrying) { 
            Debug.Log(gameObject.transform.parent.parent.name + " My ennemy...y...y is parrying");
            if (type == HitBoxType.Heavy) {
                playerController.ApplyKnockback(5, playerController.lastDirection * -1);
                playerController.SetTriggerStun(2);
                other.GetComponentInParent<PlayerController>().ApplyKnockback(5, other.GetComponentInParent<PlayerController>().lastDirection * -1);
                other.GetComponentInParent<PlayerController>().SetTriggerStun(2);
            }
            else
                playerController.SetTriggerStun(1);
        }
    }

    private void AttackManager(int damage, float knockbackForce, float timeStun, Collider collider) {
        collider.GetComponentInParent<PlayerController>().SetTriggerStun(timeStun);
        collider.GetComponent<HeartBox>().TakeDamage(damage);
        collider.GetComponentInParent<PlayerController>().ApplyKnockback(knockbackForce,playerController.lastDirection);
    }
}
