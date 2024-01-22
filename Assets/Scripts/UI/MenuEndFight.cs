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
        SceneManager.LoadScene("Menu");
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
    }
}
