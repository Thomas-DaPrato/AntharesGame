using System;
using UnityEngine;


public class Click3DButton : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject returnButtonUI;
    [SerializeField]
    private ControllerManager controllerManager;

    public bool isQuitButton;

    public void DisplayPanel()
    {
        panel.SetActive(true);
        returnButtonUI.SetActive(true);
        controllerManager.AutoSelect();
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }
}
