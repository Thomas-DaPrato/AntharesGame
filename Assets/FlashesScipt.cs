using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashesScipt : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] flashes;
    [SerializeField]
    private float frequencyMin;
    [SerializeField]
    private float frequencyMax;
    [SerializeField]
    private float timeApparition;
    [SerializeField]
    private float timeDisparition;
    private void Start()
    {
        for (int i = 0; i < flashes.Length; i++)
        {
            StartCoroutine(DoAfterDelay(Random.Range(frequencyMin, frequencyMax), FadeFlash, flashes[i]));
        }
    }
    public void FadeFlash(SpriteRenderer toFade)
    {
        toFade.material.DOFade(1, timeApparition).OnComplete(() =>
        {
            toFade.material.DOFade(0, Random.Range(0f, timeDisparition)).OnComplete(() => StartCoroutine(DoAfterDelay(Random.Range(frequencyMin, frequencyMax), FadeFlash, toFade)));
        });
    }
    public IEnumerator DoAfterDelay(float delaySeconds, System.Action<SpriteRenderer> thingToDo, SpriteRenderer toFade)
    {
        yield return new WaitForSeconds(delaySeconds);
        thingToDo(toFade);
    }
}
