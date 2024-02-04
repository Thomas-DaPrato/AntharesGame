using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

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
    private VisualEffect playerBonk;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private HitBoxType type;

    [SerializeField]
    private GameObject heartBoxPlayer;

    private void OnEnable() {
        FighterData fighterData = playerController.GetFighterData();
        switch (type) {
            case HitBoxType.Heavy:
                playerController.PlayOneShot(fighterData.heavyAttack.SFX);
                break;
            case HitBoxType.Middle:
                playerController.PlayOneShot(fighterData.middleAttack.SFX);
                break;
            case HitBoxType.Light:
                playerController.PlayOneShot(fighterData.lightAttack.SFX);
                break;
            case HitBoxType.Aerial:
                playerController.PlayOneShot(fighterData.aerialAttack.SFX);
                break;
            default:
                Debug.Log("<color=red>ERRORR : type " + type + " is not recognized</color>");
                break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        FighterData fighterData = playerController.GetFighterData();
        if (other.tag.Equals("HeartBox") && other.gameObject != heartBoxPlayer && !other.GetComponentInParent<PlayerController>().isParrying) {
            switch (type) {
                case HitBoxType.Heavy:
                    playerController.HeavyEffect();
                    AttackManager(fighterData.heavyAttack ,other);
                    break;
                case HitBoxType.Middle:
                    AttackManager(fighterData.middleAttack, other);
                    break;
                case HitBoxType.Light:
                    AttackManager(fighterData.lightAttack, other);
                    break;
                case HitBoxType.Aerial:
                    AttackManager(fighterData.aerialAttack, other);
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
                other.GetComponentInParent<PlayerController>().ApplyKnockback(5, new Vector2(other.GetComponentInParent<PlayerController>().lastDirection * -1,1));
            }
            else
                playerController.SetTriggerStun(fighterData.stunTimeParry);
        }
    }

    private void AttackManager(Attack attack, Collider collider) {
        collider.GetComponent<HeartBox>().TakeDamage(attack.percentageDamage, type);
        collider.GetComponentInParent<Rigidbody>().mass = 1;
        collider.GetComponentInParent<PlayerController>().ApplyKnockback(attack.knockback, new Vector2(playerController.lastDirection,1));
        collider.GetComponentInParent<PlayerController>().SetTriggerStun(attack.stunTime);
        playerController.PlayOneShot(attack.SFX);
        playerBonk.Play();
        if (type == HitBoxType.Light && !collider.GetComponentInParent<PlayerController>().lightAttackCanTouch)
            return;
        GameObject.Find("GameManager").GetComponent<GameManager>().DoFreeze(attack.hitFreezeTime);
        GameObject.Find("GameManager").GetComponent<GameManager>().DoShake(attack.shakeScreenIntensity,attack.shakeScreenTime);
    }
}
