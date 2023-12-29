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




    private int currentButtonSelected = 0;

    private void Start() {
        ChangeMaterial(buttons[0], selectedButton);
    }

    public void OnSwapButton(InputAction.CallbackContext context) {

        if (context.performed) {
            
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
            if (!buttons[currentButtonSelected].GetComponent<Click3DButton>().isQuitButton) {
                gameObject.SetActive(false);
                buttons[currentButtonSelected].GetComponent<Click3DButton>().DisplayPanel();
                //vcMenu.SetActive(false);
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
