using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEndFight : MonoBehaviour
{
   public void Rematch() {
        SceneManager.LoadScene("Game_Final");
   }

    public void ChooseFighters() {
        PlayerPrefs.SetInt("chooseFighter", 1);
        PlayerPrefs.SetString("SceneToLoad", "Menu");
        SceneManager.LoadScene("LoadScene");
    }

    public void Menu() {
        PlayerPrefs.SetString("SceneToLoad", "Menu");
        SceneManager.LoadScene("LoadScene");
    }
}
