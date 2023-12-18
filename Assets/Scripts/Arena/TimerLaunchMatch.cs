using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerLaunchMatch : MonoBehaviour
{
    [SerializeField]
    private GameObject support;

    private bool animatorIsReset = false;
    private bool firstLoop = true;

    private void Start() {
        support.SetActive(true);
        StartCoroutine(LaunchTimer());
    }

    public IEnumerator LaunchTimer() {
        for(int i = 3; i > 0; i -= 1) {
            support.GetComponent<TextMeshProUGUI>().text = i.ToString();
            if (!animatorIsReset) {
                if (firstLoop) {
                    GameManager.DisableFighterAnimator();
                    firstLoop = false;
                }
                else {
                    GameManager.EnableFighterAniamtor();
                    animatorIsReset = true;
                }
            }

            yield return new WaitForSeconds(1);
        }
        support.GetComponent<TextMeshProUGUI>().text = "GO !!";
        GameManager.SetFighterNotStun();
        yield return new WaitForSeconds(1);
        support.SetActive(false);
    }
}
