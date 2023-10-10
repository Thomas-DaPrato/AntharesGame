using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoixPerso : MonoBehaviour
{
    public void Start()
    {
        PlayerPrefs.SetInt("Joueur1", 0);
        PlayerPrefs.SetInt("Joueur2", 0);
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

        PlayerPrefs.SetInt("Joueur1", Perso);



    }
    public void ChoixJoueur2(int Perso)
    {

        PlayerPrefs.SetInt("Joueur2", Perso);



    }

}
