using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;


public class activateSFX : MonoBehaviour
{

    [SerializeField]
    public MMF_Player attackingLightWoosh;
    [SerializeField]
    public MMF_Player attackingMediumWoosh;
    [SerializeField]
    public MMF_Player attackingHeavyWoosh;

    public void LightAttack()
    {
        attackingLightWoosh.PlayFeedbacks();
    }

    public void MediumAttack()
    {
        attackingMediumWoosh.PlayFeedbacks();
    }

    public void HeavyAttack()
    {
        attackingHeavyWoosh.PlayFeedbacks();
    }
}
