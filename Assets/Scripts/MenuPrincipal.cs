using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
   
    public void SceneChoixPersonnage () {

        SceneManager.LoadScene("ChoixPerso");

    }


    public void Quiter()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }

}
