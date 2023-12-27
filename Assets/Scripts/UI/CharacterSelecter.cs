using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CharacterSelecter : MonoBehaviour
{

    private int currentFighter;
    private bool haveChooseFighter = false;

    [SerializeField]
    [Tooltip("use the same name as define in PlayerPrefConst")]
    private string playerPrefPlayerName;

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
    private Image support;

    [SerializeField]
    private GameObject infos;
    [SerializeField]
    private GameObject stats;
    [SerializeField]
    private GameObject lore;
    

    [SerializeField]
    private GameObject ready;




    private void Awake() {
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1, -1);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2, -1);
        support.sprite = Characters.GetFighters()[0].spriteNotSelected;
        currentFighter = 0;
    }

    private void OnEnable() {
        haveChooseFighter = false;
        ready.SetActive(false);
        support.sprite = Characters.GetFighters()[currentFighter].spriteNotSelected;
        animatorBackground.SetBool("isSelected", false);
        PlayerPrefs.SetInt(gameObject.name, -1);
    }


    public void OnCharacterSwap(InputAction.CallbackContext context) {
        if (context.performed && !haveChooseFighter) {
            infos.SetActive(false);
            if (context.ReadValue<float>() > 0) {
                currentFighter += 1;
                if (currentFighter >= Characters.GetFighters().Count)
                    currentFighter = 0;
            }
            if (context.ReadValue<float>() < 0) {
                currentFighter -= 1;
                if (currentFighter < 0)
                    currentFighter = Characters.GetFighters().Count - 1;
            }
            support.sprite = Characters.GetFighters()[currentFighter].spriteNotSelected;
        }
    }

    public void OnOpenInformation(InputAction.CallbackContext context) {
        if (context.performed) {
            if (!infos.activeSelf) {
                infos.SetActive(true);
                FillStats();
                FillLore();
                lore.SetActive(false);
            }
            else {
                stats.SetActive(true);
                lore.SetActive(false);
                infos.SetActive(false);
            }
        }
    }

    public void OnChangeInfoPannel(InputAction.CallbackContext context) {
        if (infos.activeSelf && context.performed) {
            stats.SetActive(!stats.activeSelf);
            lore.SetActive(!lore.activeSelf);
        }
    }

    public void OnReturn(InputAction.CallbackContext context) {
        if (context.performed) {
            if (infos.activeSelf)
                infos.SetActive(false);
            else if (haveChooseFighter) {
                haveChooseFighter = false;
                ready.SetActive(false);
                support.sprite = Characters.GetFighters()[currentFighter].spriteNotSelected;
                animatorBackground.SetBool("isSelected", false);
                PlayerPrefs.SetInt(gameObject.name, -1);
            }
            else {
                characterSelecter.SetActive(false);
                menu.SetActive(true);
                vcMenu.SetActive(true);
                vcBat.SetActive(false);
            }

        }
    }

    public void OnValidateCharacter(InputAction.CallbackContext context) {
        if (context.performed) {
            PlayerPrefs.SetInt(playerPrefPlayerName, currentFighter);
            support.sprite = Characters.GetFighters()[currentFighter].spriteSelected;
            animatorBackground.SetBool("isSelected", true);
            ready.SetActive(true);
            haveChooseFighter = true;
        }
    }

    public void OnStartFight(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log(PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1) != -1 && PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2) != -1);
            Debug.Log("Player Pref 1 : " + PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1));
            Debug.Log("Player Pref 2 : " + PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2));
            if (PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1) != -1 && PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2) != -1)
                StartCoroutine(StartFight());
                
        }
    }

    public IEnumerator StartFight() {
        vcBat.SetActive(false);
        vcRecul.SetActive(true);
        yield return new WaitForSeconds(2);
        vcRecul.SetActive(false);
        vcEnter.SetActive(true);
        yield return new WaitForSeconds(1);
        animatorFadeIn.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("LoadScene");
    }


    public void FillStats() {
        for(int i = 0; i < Characters.GetFighters()[currentFighter].stats.Length; i+=1) {
            stats.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = Characters.GetFighters()[currentFighter].stats[i].nameStat;
            
            //Reset color stat
            for (int j = 0; j < 3; j += 1)
                stats.transform.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.black;

            //Set color stat
            for (int j = 0; j < Characters.GetFighters()[currentFighter].stats[i].value; j += 1)
                stats.transform.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.yellow;
        }
    }

    public void FillLore() {
        //TO DO
        Debug.Log("FillLore");
    }
}
