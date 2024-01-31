using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanneauAffichageScript : MonoBehaviour
{
    [SerializeField]
    private Ease ease;
    [SerializeField]
    private Material material;
    Vector2 vecStart = new Vector2(0.64f, 0);
    Vector2 vecPresented = new Vector2(0.34f, 0);
    Vector2 vecTalos = new Vector2(0.03f, 0);
    Vector2 vecEnd = new Vector2(-0.36f, 0);

    [SerializeField]
    private float timeToWait;
    [SerializeField]
    private float timeToGoToNextPoint;

    void Start()
    {
        material.SetVector("_OffsetOnStop", vecStart);
        StartCoroutine(DoAfterDelay(timeToWait, GoToPresented));
    }

    public void GoToPresented()
    {
        material.DOVector(vecPresented, "_OffsetOnStop", timeToGoToNextPoint).SetEase(ease).OnComplete(() =>
                {
                    StartCoroutine(DoAfterDelay(timeToWait, GoToTalos));
                });
    }

    public void GoToTalos()
    {
        material.DOVector(vecTalos, "_OffsetOnStop", timeToGoToNextPoint).SetEase(ease).OnComplete(() =>
                {
                    StartCoroutine(DoAfterDelay(timeToWait, GoToAnthares));
                });
    }

    public void GoToAnthares()
    {
        material.DOVector(vecEnd, "_OffsetOnStop", timeToGoToNextPoint).SetEase(ease).OnComplete(() =>
                {
                    material.SetVector("_OffsetOnStop", vecStart);
                    StartCoroutine(DoAfterDelay(timeToWait, GoToPresented));
                });
    }

    public IEnumerator DoAfterDelay(float delaySeconds, System.Action thingToDo)
    {
        yield return new WaitForSeconds(delaySeconds);
        thingToDo();
    }
}
