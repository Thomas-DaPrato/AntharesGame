using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HitBox;

public class HeartBox : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    /*public enum PlayerName
    {
        Cesar,
        Diane
    }

    [SerializeField]
    private PlayerName playerName;*/

    public void TakeDamage(float percentageDamage, HitBox.HitBoxType type) {
        playerController.TakeDamage(percentageDamage, type);
    }
}
