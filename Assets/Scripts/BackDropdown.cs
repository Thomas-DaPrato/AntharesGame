using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BackDropdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown[] dropdowns;
    [SerializeField]
    private GameObject panelOptions;
    [SerializeField]
    private GameObject CMMenu;
    [SerializeField]
    private GameObject CMBatiment;
    [SerializeField]
    private PlayerInput UI3DPlayerInput;
    [SerializeField]
    private UI3DManager UI3DManager;
    [SerializeField]
    private DisplayUIButtonManager LeftButton;
    [SerializeField]
    private ControllerManager UI3D;


    public void BackDropdownFunction(InputAction.CallbackContext context)
    {
        TMP_Dropdown tmp_Dropdown = null;
        foreach (TMP_Dropdown dropdown in dropdowns)
        {
            if (dropdown.IsExpanded)
            {
                tmp_Dropdown = dropdown;
            }
        }

        if (tmp_Dropdown != null)
        {
            tmp_Dropdown.Hide();
        }
        else
        {
            panelOptions.SetActive(false);
            CMMenu.SetActive(true);
            CMBatiment.SetActive(false);
            UI3DPlayerInput.enabled = true;
            UI3DManager.ResetMaterial();
            LeftButton.ActiveButtonMenu();
            UI3D.AssignController();
            EventSystem.current.SetSelectedGameObject(null);
        }

    }
}
