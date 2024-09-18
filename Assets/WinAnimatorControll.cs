using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class WinAnimatorControll : MonoBehaviour
{
    [SerializeField]
    private MMF_Player victoryMusic; 
    public void PauseAnimation()
    {
        GetComponent<Animator>().speed = 0;
    }
    public void Victory()
    {
        victoryMusic.PlayFeedbacks();
    }
}
