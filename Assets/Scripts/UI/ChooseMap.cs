using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseMap : MonoBehaviour
{
    private void Start() {
        PlayerPrefs.SetString("mapName", "null");
    }

    public void LaunchLoadScene(string nameMap) {
        PlayerPrefs.SetString("mapName", nameMap);
        SceneManager.LoadScene("LoadScene");
    }  
}
