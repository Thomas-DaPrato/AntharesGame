using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private GameObject support;

    private bool animatorIsReset = false;
    private bool firstLoop = true;

    private void Start() {
        support.SetActive(true);
        StartCoroutine(MatchStartTimer());
    }

    public IEnumerator MatchStartTimer() {
        for(int i = 3; i > 0; i -= 1) {
            support.GetComponent<TextMeshProUGUI>().text = i.ToString();
            //if (!animatorIsReset) {
            //    if (firstLoop) {
            //        GameManager.DisableFighterAnimator();
            //        firstLoop = false;
            //    }
            //    else {
            //        GameManager.EnableFighterAniamtor();
            //        animatorIsReset = true;
            //    }
            //}

            yield return new WaitForSeconds(1);
        }
        support.GetComponent<TextMeshProUGUI>().text = "GO !!";
        GameManager.SetFighterNotStun();
        yield return new WaitForSeconds(1);
        support.SetActive(false);
    }

    public IEnumerator TransitionRoundTimer(int nbRound) {
        support.SetActive(true);
        support.GetComponent<TextMeshProUGUI>().text = "Round " + nbRound;
        support.GetComponent<TextMeshProUGUI>().fontSize = 300;
        
        
        yield return new WaitForSeconds(1.5f);

        support.GetComponent<TextMeshProUGUI>().text = "GO !!";
        support.GetComponent<TextMeshProUGUI>().fontSize = 500;
        GameManager.SetFighterNotStun();
        yield return new WaitForSeconds(1);
        support.SetActive(false);
    }
}
