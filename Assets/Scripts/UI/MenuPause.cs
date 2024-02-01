using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPause : MonoBehaviour
{
    [SerializeField]
    private GameObject backgroundMenuPause;
    public void MainMenu() {
        Time.timeScale = 1;
        PlayerPrefs.SetString("SceneToLoad", "Menu");
        SceneManager.LoadScene("LoadScene");
    }   

    public void Resume() {
        GameObject.Find("GameManager").GetComponent<GameManager>().HideBlurEffect();
        backgroundMenuPause.SetActive(false);
        Time.timeScale = 1;
        GameManager.SetActionMap("Player");
    }
}
