using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBox : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    public void TakeDamage(float percentageDamage, HitBox.HitBoxType type) {
        playerController.TakeDamage(percentageDamage, type);
    }
}
