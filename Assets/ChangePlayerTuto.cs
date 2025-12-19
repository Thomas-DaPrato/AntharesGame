using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerTuto : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image p1Indicator;
    [SerializeField] private Image p2Indicator;
    [SerializeField] private GameObject p1Bar;
    [SerializeField] private GameObject p2Bar;

    [Header("Sprite")]
    [SerializeField] private Sprite indicated;
    [SerializeField] private Sprite notIndicated;

    [Header("Value")]
    [SerializeField] private float delay;
    void Start()
    {
        StartCoroutine(DoAfterDelay(delay, ChangeToP2));
    }
    public IEnumerator DoAfterDelay(float delaySeconds, System.Action thingToDo)
    {
        yield return new WaitForSecondsRealtime(delaySeconds);
        thingToDo();
    }
    private void ChangeToP1()
    {
        p2Bar.SetActive(false);
        p1Bar.SetActive(true);
        p1Indicator.sprite = indicated;
        p2Indicator.sprite = notIndicated;
        StartCoroutine(DoAfterDelay(delay, ChangeToP2));
    }
    private void ChangeToP2()
    {
        p2Bar.SetActive(true);
        p1Bar.SetActive(false);
        p1Indicator.sprite = notIndicated;
        p2Indicator.sprite = indicated;
        StartCoroutine(DoAfterDelay(delay, ChangeToP1));
    }
}
