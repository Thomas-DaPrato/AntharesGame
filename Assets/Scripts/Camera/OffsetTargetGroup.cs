using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OffsetTargetGroup : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;
    public GameObject targetGroup;

    [HideInInspector]
    public GameObject fighter1;
    [HideInInspector]
    public GameObject fighter2;

    public float offset;

    private void Update() {
        GameObject higherFighter = GetHigherFighter();
        virtualCam.GetCinemachineComponent<CinemachineGroupComposer>().m_TrackedObjectOffset.y = higherFighter.transform.position.y - targetGroup.transform.position.y + offset;
    }

    public GameObject GetHigherFighter() {
        if (fighter1.transform.position.y > fighter2.transform.position.y)
            return fighter1;
        else
            return fighter2;
    }
}
