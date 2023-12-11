using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerLaunchMatch : MonoBehaviour
{
    [SerializeField]
    private GameObject support;

    private void Start() {
        support.SetActive(true);
        StartCoroutine(LaunchTimer());
    }

    public IEnumerator LaunchTimer() {
        for(int i = 3; i > 0; i -= 1) {
            support.GetComponent<TextMeshProUGUI>().text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        support.GetComponent<TextMeshProUGUI>().text = "GO !!";
        GameManager.SetFighterNotStun();
        yield return new WaitForSeconds(1);
        support.SetActive(false);
    }
}
