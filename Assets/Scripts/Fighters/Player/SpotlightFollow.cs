using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightFollow : MonoBehaviour
{
    [HideInInspector]
    public GameObject fighter;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, fighter.transform.position , 0.9f);
    }
}
