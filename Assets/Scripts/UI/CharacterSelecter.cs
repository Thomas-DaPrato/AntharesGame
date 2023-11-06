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
    private GameObject menu;
    [SerializeField]
    private GameObject characterSelecter;

    [SerializeField]
    private Image support;

    [SerializeField]
    private GameObject infos;

    [SerializeField]
    private GameObject ready;

    [SerializeField]
    private GameObject visualHold;
    private bool isHolding = false;
    private float startTime;
    private float duration = 1;


    private void Awake() {
        PlayerPrefs.SetInt("ChooseFighterP1", -1);
        PlayerPrefs.SetInt("ChooseFighterP2", -1);
        support.sprite = Characters.GetFighters()[0].sprite;
        currentFighter = 0;
    }

    private void Update() {
        if (isHolding) {
            visualHold.GetComponent<Image>().fillAmount = (Time.time - startTime) / (duration);
        }
        else
            visualHold.GetComponent<Image>().fillAmount = 0;
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
            Debug.Log("currentFighter " + currentFighter);
            support.sprite = Characters.GetFighters()[currentFighter].sprite;
        }
    }

    public void OnOpenInformation(InputAction.CallbackContext context) {
        if (context.started) {
            isHolding = true;
            startTime = Time.time;
            visualHold.SetActive(true);
        }
        if (context.performed) {
            infos.SetActive(true);
            FillInfos();
        }
        if (context.canceled) {
            isHolding = false;
            visualHold.SetActive(false);
        }
    }

    public void OnReturn(InputAction.CallbackContext context) {
        if (context.performed) {
            if (infos.activeSelf)
                infos.SetActive(false);
            else if (haveChooseFighter) {
                haveChooseFighter = false;
                ready.SetActive(false);
                PlayerPrefs.SetInt(gameObject.name, -1);
            }
            else {
                characterSelecter.SetActive(false);
                menu.SetActive(true);
            }

        }
    }

    public void OnValidateCharacter(InputAction.CallbackContext context) {
        if (context.performed) {
            PlayerPrefs.SetInt(gameObject.name, currentFighter);
            ready.SetActive(true);
            haveChooseFighter = true;
        }
    }

    public void OnStartFight(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log(PlayerPrefs.GetInt("ChooseFighterP1") != -1 && PlayerPrefs.GetInt("ChooseFighterP2") != -1);
            if (PlayerPrefs.GetInt("ChooseFighterP1") != -1 && PlayerPrefs.GetInt("ChooseFighterP2") != -1)
                SceneManager.LoadScene("Game");
        }
    }


    public void FillInfos() {
        Transform stats = infos.transform.Find("Stats");
        for(int i = 0; i < Characters.GetFighters()[currentFighter].stats.Length; i+=1) {
            stats.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = Characters.GetFighters()[currentFighter].stats[i].nameStat;
            
            //Reset color stat
            for (int j = 0; j < 3; j += 1)
                stats.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.black;

            //Set color stat
            for (int j = 0; j < Characters.GetFighters()[currentFighter].stats[i].value; j += 1)
                stats.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.yellow;
        }
    }
}
