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
    private Image[] player1Image;

    [SerializeField]
    private Image[] player2Image;

    [SerializeField]
    private float delay = 2.5f;

    [SerializeField]
    private float speedAppearance = 0.5f;

    [SerializeField]
    private Vector3 posUp;
    [SerializeField]
    private Vector3 posMid;
    [SerializeField]
    private Vector3 posDown;
    void Start()
    {
        
    }

    public IEnumerator DoAfterDelay(float delaySeconds, System.Action thingToDo)
    {
        yield return new WaitForSeconds(delaySeconds);
        thingToDo();
    }

    private void Player1Disapear()
    {
        foreach (Image image in player1Image)
        {
            image.DOFillAmount(0, speedAppearance).OnComplete(() =>
            {
                image.gameObject.SetActive(false);
                Player2Appear();
            });
        }
        StartCoroutine(DoAfterDelay(delay, Player2Disapear));
    }
    private void Player1Appear()
    {
        foreach (Image image in player1Image)
        {
            image.gameObject.SetActive(true);
            image.DOFillAmount(1, speedAppearance);
        }
    }
    private void Player2Disapear()
    {
        foreach (Image image in player2Image)
        {
            image.DOFillAmount(0, speedAppearance).OnComplete(() =>
            {
                image.gameObject.SetActive(false);
                Player1Appear();
            });
        }
        StartCoroutine(DoAfterDelay(delay, Player1Disapear));
    }
    private void Player2Appear()
    {
        foreach (Image image in player2Image)
        {
            image.gameObject.SetActive(true);
            image.DOFillAmount(1, speedAppearance);
        }
    }
}
