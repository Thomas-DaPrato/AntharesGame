using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    private CinemachineBasicMultiChannelPerlin cbmcp;

    private void Awake() {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start() {
        StopShake();
    }

    public void StartShake(float shakeIntensity) {
        cbmcp = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = shakeIntensity;
    }

    public void StopShake() {
        cbmcp = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cbmcp.m_AmplitudeGain = 0;

    }

    public IEnumerator Shake(float shakeIntensity,float duration) {
        StartShake(shakeIntensity);
        yield return new WaitForSeconds(duration);
        StopShake();
    }

   
}
