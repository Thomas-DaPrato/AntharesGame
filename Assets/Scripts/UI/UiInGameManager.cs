using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInGameManager : MonoBehaviour
{
    [Header("HP Barre")]
    public GameObject hpBarres;
    public GameObject hpBarreP1;
    public GameObject hpBarreP2;

    [Space(10)]
    [Header("Round Win Barre")]
    public GameObject roundWinP1;
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
}
