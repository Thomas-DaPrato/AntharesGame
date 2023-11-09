using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEndFight : MonoBehaviour
{
   public void Rematch() {
        SceneManager.LoadScene("Game");
   }

    public void ChooseFighters() {
        SceneManager.LoadScene("Menu");
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
    }
}
