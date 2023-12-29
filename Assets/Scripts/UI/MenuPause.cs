using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPause : MonoBehaviour
{
    [SerializeField]
    private GameObject pannelOptions;
    private void Start() {
        pannelOptions.SetActive(false);
    }
    public void MainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }   

    public void Resume() {
        GameObject.Find("GameManager").GetComponent<GameManager>().HideBlurEffect();
        gameObject.SetActive(false);
        Time.timeScale = 1;
        GameManager.SetActionMap("Player");
    }
}
