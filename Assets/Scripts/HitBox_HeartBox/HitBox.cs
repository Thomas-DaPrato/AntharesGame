using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public enum HitBoxType {
        Heavy,
        Middle,
        Light
    }

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private HitBoxType type;

    
    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("HeartBox") && !other.GetComponentInParent<PlayerController>().isParrying) {
            switch (type) {
                case HitBoxType.Heavy:
                    other.GetComponent<HeartBox>().TakeDamage(playerController.GetFighterData().heavyAttackDamage);
                    break;
                case HitBoxType.Middle:
                    other.GetComponent<HeartBox>().TakeDamage(playerController.GetFighterData().middleAttackDamage);
                    break;
                case HitBoxType.Light:
                    other.GetComponent<HeartBox>().TakeDamage(playerController.GetFighterData().lightAttackDamage);
                    break;
                default:
                    Debug.Log("ERRORR : type " + type + " is not recognized");
                    break;
            }
        }
        else if (other.tag.Equals("HeartBox") && other.GetComponentInParent<PlayerController>().isParrying)
            Debug.Log("My ennemy...y...y is parrying");
    }
}
