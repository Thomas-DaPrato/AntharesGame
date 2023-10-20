using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoixMap : MonoBehaviour
{

    public void Start()
    {
        PlayerPrefs.SetInt("Map", 0);
    }

    public void SceneChoixPersonnage()
    {

        SceneManager.LoadScene("ChoixPerso");

    }
    public void SceneFight()
    {
        if (PlayerPrefs.GetInt("Map") == 0)
        {
            Debug.Log("Choisissez une map");
        }
        else
        {
            SceneManager.LoadScene("Fight");
        }
        

        Debug.Log(PlayerPrefs.GetInt("Map"));

    }

    public void ChoixMapB(string map)
    {
        
        
        Debug.Log(map);

        PlayerPrefs.SetInt("Map", int.Parse(map));
        
        

    }


}
