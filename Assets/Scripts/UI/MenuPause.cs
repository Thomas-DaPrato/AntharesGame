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
        SceneManager.LoadScene("Menu");
    }   

    public void Resume() {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        GameManager.SetActionMap("Player");
    }
}
