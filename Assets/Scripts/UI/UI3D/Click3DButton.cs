using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click3DButton : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject returnButtonUI;

    public bool isQuitButton;

    public void DisplayPanel() {
        panel.SetActive(true);
        returnButtonUI.SetActive(true);
    }

    public void HidePanel() {
        panel.SetActive(false);
    }
}
