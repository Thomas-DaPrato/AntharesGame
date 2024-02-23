using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.Users;

public class CharacterSelecter : MonoBehaviour
{

    private int currentFighter;
    private bool haveChooseFighter = false;
    private bool haveLaunchAnimation = false;
    private Characters.ColorType colorType;

    [SerializeField]
    [Tooltip("use the same name as define in PlayerPrefConst")]
    private string playerPrefPlayerName;

    [SerializeField]
    private bool isFirstPlayer;

    [SerializeField]
    private Animator animatorFadeIn;
    [SerializeField]
    private Animator animatorBackground;

    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject characterSelecter;

    [Header("Virtual Cam")]
    [SerializeField]
    private GameObject vcMenu;
    [SerializeField]
    private GameObject vcBat;
    [SerializeField]
    private GameObject vcRecul;
    [SerializeField]
    private GameObject vcEnter;

    [SerializeField]
    private AudioSource audioSource;
    public AudioClip audioBack;

    [SerializeField]
    private Image support;

    [SerializeField]
    private TextMeshProUGUI nickName;
    [SerializeField]
    private GameObject iconInfos;
    [SerializeField]
    private GameObject panelInfos;
    [SerializeField]
    private GameObject stats;
    [SerializeField]
    private TextMeshProUGUI lore;

    [SerializeField]
    public MeshRenderer[] neonReady;
    [SerializeField]
    private Material readyMaterial;
    [SerializeField]
    private Material notReadyMaterial;
    [SerializeField]
    private CharacterSelecter otherCharacterSelecter;

    [Header("UI")]
    [SerializeField]
    private DisplayUIButtonManager UIManager;


    public PlayerInput playerInput;

    [SerializeField]
    private GameObject ready;


    private void OnEnable(){
        audioSource = GameObject.Find("MMSoundManager").GetComponent<AudioSource>();
        Characters.InitDicoAvailableColor();
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1, -1);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2, -1);
        currentFighter = 0;
        colorType = Characters.ColorType.None;
        haveChooseFighter = false;
        ready.SetActive(false);
        UIManager.EnableValidateButton();
        UIManager.EnableReturnButton();
        UIManager.EnableNavigateButton();
        support.sprite = Characters.GetFighters()[currentFighter].spriteOriginalNotSelected;
        animatorBackground.SetBool("isSelected", false);
    }


    public void OnCharacterSwap(InputAction.CallbackContext context)
    {
        if (context.performed && !haveChooseFighter)
        {
            panelInfos.SetActive(false);
            if (context.ReadValue<float>() > 0)
            {
                currentFighter += 1;
                if (currentFighter >= Characters.GetFighters().Count)
                    currentFighter = 0;
            }
            if (context.ReadValue<float>() < 0)
            {
                currentFighter -= 1;
                if (currentFighter < 0)
                    currentFighter = Characters.GetFighters().Count - 1;
            }
            support.sprite = GetSpriteNotSelected(Characters.availableColorForFighter[currentFighter][0]);
        }
    }

    public void OnOpenInformation(InputAction.CallbackContext context)
    {
        if (context.performed && !haveChooseFighter)
        {
            if (!panelInfos.activeSelf)
            {
                panelInfos.SetActive(true);
                FillStats();
            }
            else
            {
                stats.SetActive(true);
                panelInfos.SetActive(false);
            }
        }
    }


    public void OnReturn(InputAction.CallbackContext context)
    {
        if (context.performed && !haveLaunchAnimation)
        {
            if (panelInfos.activeSelf)
                panelInfos.SetActive(false);
            else if (haveChooseFighter)
            {
                haveChooseFighter = false;
                ready.SetActive(false);
                Characters.availableColorForFighter[currentFighter].Insert(0, colorType);
                if (Characters.availableColorForFighter[currentFighter].Count >= 2)
                    Characters.ResetAvailableColor(currentFighter);
                colorType = Characters.ColorType.None;
                support.sprite = GetSpriteNotSelected(Characters.availableColorForFighter[currentFighter][0]);
                animatorBackground.SetBool("isSelected", false);
                PlayerPrefs.SetInt(playerPrefPlayerName, -1);
                iconInfos.SetActive(true);
                UIManager.DisableStartButton();
                UIManager.EnableValidateButton();
                for (int i = 0; i < neonReady.Length; i++)
                {
                    neonReady[i].material = notReadyMaterial;
                }

            }
            else
            {
                audioSource.PlayOneShot(audioBack);
                characterSelecter.SetActive(false);
                menu.GetComponent<PlayerInput>().enabled = true;
                menu.GetComponent<UI3DManager>().ResetMaterial();
                vcMenu.SetActive(true);
                vcBat.SetActive(false);
                UIManager.ActiveButtonMenu();
                for (int i = 0; i < otherCharacterSelecter.neonReady.Length; i++)
                {
                    otherCharacterSelecter.neonReady[i].material = notReadyMaterial;
                }

                for (int i = 0; i < neonReady.Length; i++)
                {
                    otherCharacterSelecter.neonReady[i].material = notReadyMaterial;
                }
            }

        }
    }

    public void OnValidateCharacter(InputAction.CallbackContext context)
    {
        if (context.performed && !haveChooseFighter)
        {
            PlayerPrefs.SetInt(playerPrefPlayerName, currentFighter);
            colorType = Characters.availableColorForFighter[currentFighter][0];
            PlayerPrefs.SetInt(playerPrefPlayerName + "color", (int)colorType);
            Characters.availableColorForFighter[currentFighter].RemoveAt(0);
            support.sprite = GetSpriteSelected(colorType);
            animatorBackground.SetBool("isSelected", true);
            ready.SetActive(true);
            haveChooseFighter = true;
            panelInfos.SetActive(false);
            iconInfos.SetActive(false);
            for (int i = 0; i < neonReady.Length; i++)
            {
                neonReady[i].material = readyMaterial;
            }
            if (PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1) != -1 && PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2) != -1)
            {
                UIManager.DisableValidateButton();
                UIManager.DisableNavigateButton();
                UIManager.EnableStartButton();
            }
        }
    }

    public void OnStartFight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log(PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1) != -1 && PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2) != -1);
            Debug.Log("Player Pref 1 : " + PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1));
            Debug.Log("Player Pref 2 : " + PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2));
            if (PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1) != -1 && PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2) != -1 && !haveLaunchAnimation) {
                haveLaunchAnimation = true;
                StartCoroutine(StartFight());
            }

        }
    }

    public IEnumerator StartFight()
    {
        UIManager.DisableAllButtons();
        vcBat.SetActive(false);
        vcRecul.SetActive(true);
        yield return new WaitForSeconds(2);
        vcRecul.SetActive(false);
        vcEnter.SetActive(true);
        yield return new WaitForSeconds(1);
        animatorFadeIn.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetString("SceneToLoad", "Game_Final");
        SceneManager.LoadScene("LoadScene");
    }


    public void FillStats()
    {
        FighterData fighter = Characters.GetFighters()[currentFighter];

        nickName.text = fighter.nickName;

        for (int i = 0; i < fighter.stats.Length; i += 1)
        {
            stats.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = fighter.stats[i].nameStat;

            //Reset color stat
            for (int j = 0; j < 3; j += 1)
                stats.transform.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.black;

            //Set color stat
            for (int j = 0; j < Characters.GetFighters()[currentFighter].stats[i].value; j += 1)
                stats.transform.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.yellow;
        }

        lore.text = fighter.lore.text;
    }

    public Sprite GetSpriteNotSelected(Characters.ColorType color)
    {
        if (color == Characters.ColorType.Original)
            return Characters.GetFighters()[currentFighter].spriteOriginalNotSelected;
        else
            return Characters.GetFighters()[currentFighter].spriteMirrorNotSelected;
    }
    public Sprite GetSpriteSelected(Characters.ColorType color)
    {
        if (color == Characters.ColorType.Original)
            return Characters.GetFighters()[currentFighter].spriteOriginalSelected;
        else
            return Characters.GetFighters()[currentFighter].spriteMirrorSelected;
    }
}
