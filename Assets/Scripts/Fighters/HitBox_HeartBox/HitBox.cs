using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public enum HitBoxType {
        Heavy,
        Middle,
        Light,
        Aerial,
        Trap
    }

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private HitBoxType type;

    [SerializeField]
    private float percentageDamage;

    
    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("HeartBox") && !other.GetComponentInParent<PlayerController>().isParrying) {
            FighterData fighterData = playerController.GetFighterData();
            switch (type) {
                case HitBoxType.Heavy:

                    AttackManager(percentageDamage, fighterData.heavyAttack.knockback, fighterData.heavyAttack.stunTime ,other);
                    break;
                case HitBoxType.Middle:
                    AttackManager(percentageDamage, fighterData.middleAttack.knockback, fighterData.middleAttack.stunTime, other);
                    break;
                case HitBoxType.Light:
                    AttackManager(percentageDamage, fighterData.lightAttack.knockback, fighterData.lightAttack.stunTime, other);
                    break;
                case HitBoxType.Aerial:
                    AttackManager(percentageDamage, fighterData.aerialsAttack.knockback, fighterData.aerialsAttack.stunTime, other);
                    break;
                default:
                    Debug.Log("<color=red>ERRORR : type " + type + " is not recognized</color>");
                    break;
            }
        }
        else if (other.tag.Equals("HeartBox") && other.GetComponentInParent<PlayerController>().isParrying) { 
            Debug.Log(gameObject.transform.parent.parent.name + " My ennemy...y...y is parrying");
            if (type == HitBoxType.Heavy) {
                playerController.ApplyKnockback(5, new Vector2(playerController.lastDirection * -1, 1));
                playerController.SetTriggerStun(2);
                other.GetComponentInParent<PlayerController>().ApplyKnockback(5, new Vector2(other.GetComponentInParent<PlayerController>().lastDirection * -1,1));
                other.GetComponentInParent<PlayerController>().SetTriggerStun(2);
            }
            else
                playerController.SetTriggerStun(1);
        }
    }

    private void AttackManager(float percentageDamage, float knockbackForce, float timeStun, Collider collider) {
        collider.GetComponentInParent<PlayerController>().SetTriggerStun(timeStun);
        collider.GetComponent<HeartBox>().TakeDamage(percentageDamage, type);
        collider.GetComponentInParent<PlayerController>().ApplyKnockback(knockbackForce, new Vector2(playerController.lastDirection,1));
    }
}
