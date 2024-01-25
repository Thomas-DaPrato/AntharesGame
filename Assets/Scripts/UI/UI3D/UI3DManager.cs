using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UI3DManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> buttons;

    [Header("Material")]
    [SerializeField]
    private Material selectedButton;
    [SerializeField]
    private Material notSelectedButton;
    [SerializeField]
    private Material blackButton;

    [SerializeField]
    private GameObject returnButtonUI;
    [SerializeField]
    private GameObject navigateButtonUI;
    [SerializeField]
    private GameObject validateButtonUI;

    [Header("Camera")]
    [SerializeField]
    private GameObject vcMenu;
    [SerializeField]
    private GameObject vcBat;


    [Header("Audio")]
    public AudioSource audioSource; 
    public AudioClip audioButtonSwap;
    public AudioClip audioButtonClick;


    private int currentButtonSelected = 0;

    private void Start() {
        if(PlayerPrefs.GetInt("chooseFighter") == 1) {
            GetComponent<PlayerInput>().enabled = false;
            foreach (GameObject button in buttons)
                button.GetComponent<MeshRenderer>().sharedMaterial = blackButton;
            buttons[currentButtonSelected].GetComponent<Click3DButton>().DisplayPanel();
            vcMenu.SetActive(false);
            vcBat.SetActive(true);
        }
        else 
            ChangeMaterial(buttons[0], selectedButton);
        PlayerPrefs.SetInt("chooseFighter", 0);
    }

    public void OnSwapButton(InputAction.CallbackContext context) {
        if (context.performed) {
            audioSource.PlayOneShot(audioButtonSwap);

            ChangeMaterial(buttons[currentButtonSelected], notSelectedButton);
            if (context.ReadValue<float>() < 0) {
                currentButtonSelected += 1;
                if (currentButtonSelected >= buttons.Count)
                    currentButtonSelected = 0;
            }
            if (context.ReadValue<float>() > 0) {
                currentButtonSelected -= 1;
                if (currentButtonSelected < 0)
                    currentButtonSelected = buttons.Count - 1;
            }
            ChangeMaterial(buttons[currentButtonSelected], selectedButton);
        }
    }

    public void OnClick(InputAction.CallbackContext context) {
        if (context.performed) {
            audioSource.PlayOneShot(audioButtonClick);
                
            if (!buttons[currentButtonSelected].GetComponent<Click3DButton>().isQuitButton) {
                GetComponent<PlayerInput>().enabled = false;
                validateButtonUI.SetActive(false);
                foreach (GameObject button in buttons)
                    button.GetComponent<MeshRenderer>().sharedMaterial = blackButton;
                buttons[currentButtonSelected].GetComponent<Click3DButton>().DisplayPanel();
                vcMenu.SetActive(false);
                vcBat.SetActive(true);
                returnButtonUI.SetActive(true);
                navigateButtonUI.SetActive(true);
                
            }
            else
                Application.Quit();
        }
    }

    public void ChangeMaterial(GameObject button, Material material) {
        button.GetComponent<MeshRenderer>().sharedMaterial = material;
    }

    public void ResetMaterial() {
        for(int i = 0; i < buttons.Count; i+=1) {
            if (i != currentButtonSelected)
                buttons[i].GetComponent<MeshRenderer>().sharedMaterial = notSelectedButton;
            else
                buttons[i].GetComponent<MeshRenderer>().sharedMaterial = selectedButton;
        }
    }
}
