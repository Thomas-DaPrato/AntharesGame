using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInGameManager : MonoBehaviour
{
    [Header("HP Barre")]
    public GameObject hpBarres;
    public GameObject hpBarreP1;
    public GameObject XKeyP1;
    public GameObject hpBarreP2;
    public GameObject XKeyP2;

    [Space(10)]
    [Header("Round Win Barre")]
    public Image player1Image; 
    public GameObject roundWinP1;
    public Image player2Image;
    public GameObject roundWinP2;

    [Space(10)]
    [Header("Menu")]
    public GameObject menuPause;
    public GameObject menuEndOfFight;

    [Space(10)]
    public GameObject fightTransition;

    [Space(10)]
    public GameObject timer;

    [Space(10)]
    public GameObject rematchButton;

    private void Start() {
        //fighter left
        if ((Characters.ColorType)PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1 + "color") == Characters.ColorType.Original)
            player1Image.sprite = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].orginalSkinLoadingScene;
        else
            player1Image.sprite = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].mirrorSkinLoadingScene;

        //fighter right
        if ((Characters.ColorType)PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2 + "color") == Characters.ColorType.Original)
            player2Image.sprite = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].orginalSkinLoadingScene;
        else
            player2Image.sprite = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].mirrorSkinLoadingScene;
    }
}
