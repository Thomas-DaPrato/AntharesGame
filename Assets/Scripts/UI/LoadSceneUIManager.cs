using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneUIManager : MonoBehaviour
{
    public GameObject pannelLoadMenu;
    public GameObject pannelLoadfight;
    
    public Image fighterLeft; 
    public Image fighterRight;


    private void Start() {
        if (PlayerPrefs.GetString("SceneToLoad").Equals("Menu")) {
            pannelLoadMenu.SetActive(true);
            pannelLoadfight.SetActive(false);
        }
        if(PlayerPrefs.GetString("SceneToLoad").Equals("Game_Final")) {
            pannelLoadMenu.SetActive(false);
            pannelLoadfight.SetActive(true);
        }


        //fighter left
        if ((ColorFighter.ColorType) PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1 + "color") == ColorFighter.ColorType.Original)
            fighterLeft.sprite = Characters.instance.fightersData[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].orginalSkinLoadingScene;
        else
            fighterLeft.sprite = Characters.instance.fightersData[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].mirrorSkinLoadingScene;

        //fighter right
        if ((ColorFighter.ColorType) PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2 + "color") == ColorFighter.ColorType.Original)
            fighterRight.sprite = Characters.instance.fightersData[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].orginalSkinLoadingScene;
        else
            fighterRight.sprite = Characters.instance.fightersData[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].mirrorSkinLoadingScene;

    }
}
