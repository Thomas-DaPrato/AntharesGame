using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player1;
    public Transform player2;


    void FixedUpdate() {
        gameObject.transform.position = CameraMovement();
    }

    public Vector3 CameraMovement() {
        double distanceBetweenPlayer = Math.Sqrt(Math.Pow(player2.position.x - player1.position.x, 2) + Math.Pow(player2.position.y - player1.position.y, 2));
        double cameraZ;
        if (distanceBetweenPlayer <= 8)
            cameraZ = -5;
        else
            cameraZ = -1 - distanceBetweenPlayer / 2;
        return new Vector3((player2.position.x + player1.position.x)/2, (player2.position.y + player1.position.y)/2, (float) cameraZ);
    }
}
