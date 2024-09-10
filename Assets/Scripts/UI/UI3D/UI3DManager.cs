using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using MoreMountains.Feedbacks;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

[System.Serializable]
public class ButtonsList
{
    public string lang;
    public List<GameObject> buttons;
}
public class UI3DManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> buttons;
    [SerializeField]
    private List<ButtonsList> buttonsLang;
    [Header("Material")]
    [SerializeField]
    private Material selectedButton;
    [SerializeField]
    private Material notSelectedButton;
    [SerializeField]
    private Material blackButton;

    [SerializeField]
    private DisplayUIButtonManager UIManager;

    [Header("Camera")]
    [SerializeField]
    private GameObject vcMenu;
    [SerializeField]
    private GameObject vcBat;


    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip audioButtonSwap;
    public AudioClip audioButtonClick;

    [SerializeField]
    private MMF_Player launchingMenuMusic;


    private int currentButtonSelected = 0;
    private List<GameObject> currentList;
    public bool on3DMenu = true;

    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;

        foreach (ButtonsList buttonList in buttonsLang)
        {
            if (buttonList.lang == LocalizationSettings.SelectedLocale.Identifier.Code)
            {
                currentList = buttonList.buttons;
                break;
            }
        }
        launchingMenuMusic.PlayFeedbacks();
        if (PlayerPrefs.GetInt("chooseFighter") == 1)
        {
            GetComponent<PlayerInput>().enabled = false;
            foreach (GameObject button in currentList)
            {
                button.GetComponent<MeshRenderer>().sharedMaterial = blackButton;
            }
            /*foreach (GameObject button in buttonsLang)
                button.GetComponent<MeshRenderer>().sharedMaterial = blackButton;*/
            //buttons[currentButtonSelected].GetComponent<Click3DButton>().DisplayPanel();
            currentList[currentButtonSelected].GetComponent<Click3DButton>().DisplayPanel();
            vcMenu.SetActive(false);
            vcBat.SetActive(true);
        }
        else
        {
            ChangeMaterial(currentList[0], selectedButton);
        }
        //ChangeMaterial(buttons[0], selectedButton);
        audioSource = GameObject.Find("MMSoundManager").GetComponent<AudioSource>();
        PlayerPrefs.SetInt("chooseFighter", 0);
    }

    public void OnSwapButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            audioSource.PlayOneShot(audioButtonSwap);

            //ChangeMaterial(buttons[currentButtonSelected], notSelectedButton);
            ChangeMaterial(currentList[currentButtonSelected], notSelectedButton);
            if (context.ReadValue<float>() < 0)
            {
                currentButtonSelected += 1;
                if (currentButtonSelected >= currentList.Count)
                    currentButtonSelected = 0;
            }
            if (context.ReadValue<float>() > 0)
            {
                currentButtonSelected -= 1;
                if (currentButtonSelected < 0)
                    currentButtonSelected = currentList.Count - 1;
            }
            //ChangeMaterial(buttons[currentButtonSelected], selectedButton);
            ChangeMaterial(currentList[currentButtonSelected], selectedButton);
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            audioSource.PlayOneShot(audioButtonClick);

            //if (!buttons[currentButtonSelected].GetComponent<Click3DButton>().isQuitButton)
            if (!currentList[currentButtonSelected].GetComponent<Click3DButton>().isQuitButton)
            {
                GetComponent<PlayerInput>().enabled = false;
                UIManager.DisableValidateButton();
                /*foreach (GameObject button in buttons)
                    button.GetComponent<MeshRenderer>().sharedMaterial = blackButton;*/
                foreach (GameObject button in currentList)
                {
                    button.GetComponent<MeshRenderer>().sharedMaterial = blackButton;
                }
                //buttons[currentButtonSelected].GetComponent<Click3DButton>().DisplayPanel();
                currentList[currentButtonSelected].GetComponent<Click3DButton>().DisplayPanel();
                vcMenu.SetActive(false);
                vcBat.SetActive(true);
                UIManager.EnableReturnButton();
                on3DMenu = false;

            }
            else
                Application.Quit();
        }
    }

    public void ChangeMaterial(GameObject button, Material material)
    {
        button.GetComponent<MeshRenderer>().sharedMaterial = material;
    }

    public void ResetMaterial()
    {
        for (int i = 0; i < currentList.Count; i += 1)
        {
            /*if (i != currentButtonSelected)
                buttons[i].GetComponent<MeshRenderer>().sharedMaterial = notSelectedButton;
            else
                buttons[i].GetComponent<MeshRenderer>().sharedMaterial = selectedButton;*/
            if (i != currentButtonSelected)
                currentList[i].GetComponent<MeshRenderer>().sharedMaterial = notSelectedButton;
            else
                currentList[i].GetComponent<MeshRenderer>().sharedMaterial = selectedButton;
        }
    }
    public void ChangeCurrentList()
    {
        foreach (ButtonsList buttonList in buttonsLang)
        {
            if (buttonList.lang == LocalizationSettings.SelectedLocale.Identifier.Code)
            {
                currentList = buttonList.buttons;
                break;
            }
        }
        foreach (GameObject button in currentList)
        {
            button.GetComponent<MeshRenderer>().sharedMaterial = blackButton;
        }
        if (on3DMenu)
        {
            ResetMaterial();
        }
        //ResetMaterial();
        //ChangeMaterial(currentList[currentButtonSelected], selectedButton);
    }
}
