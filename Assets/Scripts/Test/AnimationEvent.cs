using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public GameObject cube;

    public void DisplayCube() {
        cube.SetActive(true);
    }
}
