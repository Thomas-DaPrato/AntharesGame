using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelecter : MonoBehaviour
{

    [SerializeField]
    private List<FighterData> fighters = new List<FighterData>();
    private int currentFighter;
    private bool haveChooseFighter = false;


    [SerializeField]
    private Image support;

    [SerializeField]
    private GameObject info;

    [SerializeField]
    private GameObject ready;

    private void Awake() {
        PlayerPrefs.SetInt("ChooseFighterP1", -1);
        PlayerPrefs.SetInt("ChooseFighterP2", -1);
        support.sprite = fighters[0].sprite;
        currentFighter = 0;
    }

    public void OnCharacterSwap(InputAction.CallbackContext context) {
        if (context.performed && !haveChooseFighter) {
            if (context.ReadValue<float>() > 0) {
                currentFighter += 1;
                if (currentFighter >= fighters.Count)
                    currentFighter = 0;
            }
            if (context.ReadValue<float>() < 0) {
                currentFighter -= 1;
                if (currentFighter < 0)
                    currentFighter = fighters.Count - 1;
            }
            Debug.Log("currentFighter " + currentFighter);
            support.sprite = fighters[currentFighter].sprite;
        }
    }

    public void OnOpenInformation(InputAction.CallbackContext context) {
        if(context.performed)
            info.SetActive(true);
    }

    public void OnReturn(InputAction.CallbackContext context) {
        if (context.performed) {
            if (info.activeSelf)
                info.SetActive(false);
            else if (haveChooseFighter) {
                haveChooseFighter = false;
                ready.SetActive(false);
                PlayerPrefs.SetInt(gameObject.name, -1);
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
            if (PlayerPrefs.GetInt("ChooseFighterP1") != -1 && PlayerPrefs.GetInt("ChooseFighterP2") != -1)
                SceneManager.LoadScene("Game");
        }
    }


    public void SceneMenuPrincipal()
    {

        SceneManager.LoadScene("MenuPrincipal");

    }
    public void SceneChoixMap()
    {
        if (PlayerPrefs.GetInt("Joueur1") == 0 || PlayerPrefs.GetInt("Joueur2") == 0) {

            Debug.Log("choisissez votre personnage");
        } else
        {
            SceneManager.LoadScene("ChoixMap");
        }
        

    }


    public void ChoixJoueur1(int Perso)
    {

        PlayerPrefs.SetInt("Player1", Perso);



    }
    public void ChoixJoueur2(int Perso)
    {

        PlayerPrefs.SetInt("Player2", Perso);



    }

}
