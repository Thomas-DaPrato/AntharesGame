using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private GameObject support;

    [SerializeField]
    private Animator animator;


    private void Start() {
        support.SetActive(true);
        StartCoroutine(MatchStartTimer());
    }

    public IEnumerator MatchStartTimer() {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(3);
        GameManager.SetFighterNotStun();
        yield return new WaitForSeconds(1);
        support.SetActive(false);
    }

    public IEnumerator TransitionRoundTimer(int nbRound) {
        support.SetActive(true);
        animator.SetTrigger("Round"+nbRound);
        yield return new WaitForSeconds(1.5f);
        GameManager.SetFighterNotStun();
        yield return new WaitForSeconds(1);
        support.SetActive(false);
    }
}
