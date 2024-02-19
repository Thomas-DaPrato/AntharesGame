using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    [SerializeField]
    private GameObject backgroundMenuPause;

    [SerializeField]
    private GameObject UICombat;

    [SerializeField]
    private Image timer;

    public void MainMenu() {
        Time.timeScale = 1;
        PlayerPrefs.SetString("SceneToLoad", "Menu");
        SceneManager.LoadScene("LoadScene");
    }   

    public void Resume() {
        UICombat.SetActive(true);
        timer.enabled = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().HideBlurEffect();
        backgroundMenuPause.SetActive(false);
        Time.timeScale = 1;
        GameManager.SetActionMap("Player");
    }
}
