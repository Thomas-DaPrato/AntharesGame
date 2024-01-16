using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MovingCrew : MonoBehaviour
{
    public float offset;
    private float time;

    private float initY;


    public float minTime;
    public float maxTime;
    
    private bool isMoving = false;


    private void Start() {
        initY = transform.position.y;

        time = Random.Range(minTime, maxTime);
    }

    private void Update() {
        if (!isMoving)
            StartCoroutine(CrewMovement());
            
    }

    public IEnumerator CrewMovement() {
        isMoving = true;
        transform.DOMoveY((initY + offset), time);
        yield return new WaitForSeconds(time);
        transform.DOMoveY((initY), time);
        yield return new WaitForSeconds(time);
        isMoving = false;
    }

}
