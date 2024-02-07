using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    //https://dotween.demigiant.com/documentation.php
    void OpenDoors()
    {
        door1.DORotate(rotationToDo, timeToOpen).SetEase(Ease.OutSine);
        door2.DORotate(rotationToDo * -1, timeToOpen).SetEase(Ease.OutSine);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("DoorOpener"))
        {
            OpenDoors();
        }
    }
}
