using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    private void Awake() {
        PlayerPrefs.SetInt("Joueur1", 0);
        PlayerPrefs.SetInt("Joueur2", 0);
    }


    public void Quiter()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }

}
