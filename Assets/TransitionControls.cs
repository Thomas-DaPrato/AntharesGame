using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class TransitionControls : MonoBehaviour
{
    [SerializeField]
    private RectTransform player1Screen;

    [SerializeField]
    private RectTransform player2Screen;

    [SerializeField]
    private float delay = 2.5f;

    [SerializeField]
    private float speedAppearance = 0.5f;

    [SerializeField]
    private Vector2 posUp;
    [SerializeField]
    private Vector2 posMid;
    [SerializeField]
    private Vector2 posDown;
    [SerializeField]
    private Ease ease;
    void Start()
    {
        StartCoroutine(DoAfterDelay(delay, MoveToP2));
    }

    public IEnumerator DoAfterDelay(float delaySeconds, System.Action thingToDo)
    {
        yield return new WaitForSeconds(delaySeconds);
        thingToDo();
    }

    private void MoveToP2()
    {
        //player1Screen.DOMoveY(posUp.y, speedAppearance);
        //player2Screen.DOMoveY(posMid.y, speedAppearance);
        player1Screen.DOAnchorPosY(posDown.y, speedAppearance).SetEase(ease);
        player2Screen.DOAnchorPosY(posMid.y, speedAppearance).SetEase(ease);

        StartCoroutine(DoAfterDelay(delay, MoveToP1));
    }
    private void MoveToP1()
    {
        //player1Screen.DOMoveY(posMid.y, speedAppearance);
        //player2Screen.DOMoveY(posDown.y, speedAppearance);
        player1Screen.DOAnchorPosY(posMid.y, speedAppearance).SetEase(ease);
        player2Screen.DOAnchorPosY(posUp.y, speedAppearance).SetEase(ease);
        StartCoroutine(DoAfterDelay(delay, MoveToP2));
    }
}
