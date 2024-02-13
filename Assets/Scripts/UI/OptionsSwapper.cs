using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OptionsSwapper : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> optionsPannel;

    public GameObject options;

    [SerializeField]
    private GameObject menuPause;

    [SerializeField]
    private EventSystem eventSystem;

    [SerializeField]
    private GameObject objectSelectedOnReturn;

    private int currentPannel;

    private void Awake() {
        currentPannel = 0;   
    }

    public void OnChangePannel(InputAction.CallbackContext context) {
        if (context.performed) {
            optionsPannel[currentPannel].SetActive(false);
            if (context.ReadValue<float>() > 0) {
                currentPannel += 1;
                if (currentPannel >= optionsPannel.Count)
                    currentPannel = 0;
            }
            if (context.ReadValue<float>() < 0) {
                currentPannel -= 1;
                if (currentPannel < 0)
                    currentPannel = optionsPannel.Count - 1;
            }
            optionsPannel[currentPannel].SetActive(true);
        }
    }

   public void OnReturn(InputAction.CallbackContext context) {
        if (context.performed && options.activeSelf) {
            menuPause.SetActive(true);
            options.SetActive(false);
            eventSystem.SetSelectedGameObject(objectSelectedOnReturn);
        }
    }

   
}
