using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI3DManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> buttons;

    [SerializeField]
    private Material selectedButton;
    [SerializeField]
    private Material notSelectedButton;

    [SerializeField]
    private GameObject vcMenu;
    [SerializeField]
    private GameObject vcBat;


    public AudioSource audioSource; 
    public List<AudioClip> audioButtonSwap;
    public List<AudioClip> audioButtonClick;
    int r;
    private float dureeAudio = 0;




    private int currentButtonSelected = 0;

    private void Start() {
        ChangeMaterial(buttons[0], selectedButton);
    }

    public void OnSwapButton(InputAction.CallbackContext context) {

        if (context.performed) {

            //son du swap button
            if (audioButtonSwap.Count == 0)
            {
                Debug.Log("pas de son swap");
            }
            else
            {
                r = Random.Range(0, audioButtonSwap.Count - 1);
                AudioClip clip = audioButtonSwap[r];
                dureeAudio = clip.length;
                audioSource.clip = clip;
                audioSource.Play();
            }


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

            //son du click button
            if (audioButtonClick.Count == 0)
            {
                Debug.Log("pas de son click");
            }
            else
            {
                r = Random.Range(0, audioButtonClick.Count - 1);
                AudioClip clip = audioButtonClick[r];
                dureeAudio = clip.length;
                audioSource.clip = clip;
                audioSource.Play();
            }
            

            if (!buttons[currentButtonSelected].GetComponent<Click3DButton>().isQuitButton) {
                gameObject.SetActive(false);
                buttons[currentButtonSelected].GetComponent<Click3DButton>().DisplayPanel();
                vcMenu.SetActive(false);
                vcBat.SetActive(true);
                
            }
            else
                Application.Quit();
        }
    }

    public void ChangeMaterial(GameObject button, Material material) {
        button.GetComponent<MeshRenderer>().sharedMaterial = material;
    }

   
}
