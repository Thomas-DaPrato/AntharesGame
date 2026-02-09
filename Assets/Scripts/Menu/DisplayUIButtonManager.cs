using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUIButtonManager : MonoBehaviour
{
    public bool P1 = true;
    public GameObject text;
    public GameObject validateButton;
    public GameObject returnButton;
    public GameObject navigateButton;
    public GameObject startButton;
    public GameObject changePanelButton;


    private void Awake() {
        ActiveButtonMenu();
    }


    public void EnableValidateButton() {
        validateButton.SetActive(true);
    }
    public void DisableValidateButton() {
        validateButton.SetActive(false);
    }

    public void EnableReturnButton() {
        returnButton.SetActive(true);
    }
    public void DisableReturnButton() {
        returnButton.SetActive(false);
    }

    public void EnableNavigateButton() {
        navigateButton.SetActive(true);
    }
    public void DisableNavigateButton() {
        navigateButton.SetActive(false);
    }

    public void EnableStartButton() {
        startButton.SetActive(true);
    }
    public void DisableStartButton() {
        startButton.SetActive(false);
    }

    public void EnableChangePanelButton() {
        changePanelButton.SetActive(true);
    }
    public void DisableChangePanelButton() {
        changePanelButton.SetActive(false);
    }


    public void ActiveButtonMenu() {
        validateButton.SetActive(true);
        returnButton.SetActive(false);
        navigateButton.SetActive(true);
        changePanelButton.SetActive(false);
        startButton.SetActive(false);
    }
    
    public void DisableAllButtons() {
        text.SetActive(false);
        validateButton.SetActive(false);
        returnButton.SetActive(false);
        navigateButton.SetActive(false);
        changePanelButton.SetActive(false);
        startButton.SetActive(false);
    }

}
