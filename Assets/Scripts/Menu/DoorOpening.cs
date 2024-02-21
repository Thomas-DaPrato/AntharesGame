using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;

public class DoorOpening : MonoBehaviour
{
    [SerializeField]
    private Transform door1;
    [SerializeField]
    private Transform door2;
    [SerializeField]
    private Vector3 rotationToDo;
    [SerializeField]
    private float timeToOpen;
    [SerializeField]
    private GameObject jingleManager;
    [SerializeField]
    private MMF_Player launchingFightFeedback;

    public GameObject sound;

    private void Start()
    {
        GameObject mmsm = GameObject.Find("MMSoundManager");
        launchingFightFeedback = mmsm.GetComponentInChildren<MMF_Player>();
    }

    //https://dotween.demigiant.com/documentation.php
    void OpenDoors()
    {
        //jingleManager.GetComponent<JingleManager>().playSound();
        door1.DORotate(rotationToDo, timeToOpen).SetEase(Ease.OutSine);
        door2.DORotate(rotationToDo * -1, timeToOpen).SetEase(Ease.OutSine);
        launchingFightFeedback.PlayFeedbacks();
        Destroy(sound);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("DoorOpener"))
        {
            OpenDoors();
        }
    }
}
