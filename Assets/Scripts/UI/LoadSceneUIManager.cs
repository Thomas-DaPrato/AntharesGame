using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneUIManager : MonoBehaviour
{
    public Image fighterLeft; 
    public Image fighterRight;


    private void Start() {
        //fighter left
        if ((Characters.ColorType) PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1 + "color") == Characters.ColorType.Original)
            fighterLeft.sprite = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].orginalSkinLoadingScene;
        else
            fighterLeft.sprite = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].mirrorSkinLoadingScene;

        //fighter right
        if ((Characters.ColorType) PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2 + "color") == Characters.ColorType.Original)
            fighterRight.sprite = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].orginalSkinLoadingScene;
        else
            fighterRight.sprite = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].orginalSkinLoadingScene;

    }
}
