using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;

public class UiInGameManager : MonoBehaviour
{
    public GameObject UICombat;

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

    [Space(10)]
    [Header("UI Feedback Player 1")]
    public MMF_Player heavyUIFeedBackPlayer1;
    public MMF_Player mediumUIFeedBackPlayer1;
    public MMF_Player lightUIFeedBackPlayer1;
    public MMF_Player charaUIFeedbackPlayer1;
    public MMF_Player skullUIFeedbackStartPlayer1;
    public MMF_Player skullUIFeedbackLoop1Player1;
    public MMF_Player skullUIFeedbackLoop2Player1;
    public MMF_Player YUIFeedbackLoopPlayer1;
    public MMF_Player BUIFeedbackLoopPlayer1;

    
    [Space(10)]
    [Header("UI Feedback Player 2")]
    public MMF_Player heavyUIFeedBackPlayer2;
    public MMF_Player mediumUIFeedBackPlayer2;
    public MMF_Player lightUIFeedBackPlayer2;
    public MMF_Player charaUIFeedbackPlayer2;
    public MMF_Player skullUIFeedbackStartPlayer2;
    public MMF_Player skullUIFeedbackLoop1Player2;
    public MMF_Player skullUIFeedbackLoop2Player2;
    public MMF_Player YUIFeedbackLoopPlayer2;
    public MMF_Player BUIFeedbackLoopPlayer2;
    private void Start()
    {


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
