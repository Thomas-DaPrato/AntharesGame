using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBox : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    public void TakeDamage(int damage) {
        playerController.TakeDamage(damage);
    }
}
