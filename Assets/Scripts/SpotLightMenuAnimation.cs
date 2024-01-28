using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class SpotLightMenuAnimation : MonoBehaviour
{
    [SerializeField]
    private float timeToMove;
    [SerializeField]
    private Vector3 angleToGo1;
    [SerializeField]
    private Vector3 angleToGo2;
    // -27 74 -36
    //-22 -74 130
    private bool toAdd = true;
    private void Start()
    {
        PanLight();
    }

    public void PanLight()
    {
        if (toAdd)
        {
            transform.DORotate(angleToGo1, timeToMove).OnComplete(() =>
            {
                toAdd = false;
                PanLight();
            });
        }
        else
        {
            transform.DORotate(angleToGo2, timeToMove).OnComplete(() =>
            {
                toAdd = true;
                PanLight();
            });
        }
    }
}
