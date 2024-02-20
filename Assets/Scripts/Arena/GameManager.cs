using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
using MoreMountains.Feedbacks;

public class GameManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField]
    private CinemachineTargetGroup targetsGroup;
    [SerializeField]
    private CinemachineVirtualCamera mainVirtualCamera;

    [Header("Camera Effect")]
    [SerializeField]
    private Freezer freezer;
    [SerializeField]
    private GameObject blurEffect;


    [Header("Fighters")]
    [SerializeField]
    private Transform spawnP1;
    [SerializeField]
    private Transform spawnP2;
    [SerializeField]
    private Transform endMatchPodium;
    [Header("Traps")]
    [SerializeField]
    private GameObject trap;


    [Header("Limit Arena")]
    [SerializeField]
    private GameObject upperLeftLimit;
    [SerializeField]
    private GameObject lowerRightLimit;

    [Space(10)]
    [SerializeField]
    private GameObject fighters;
    [SerializeField]
    private Material forceShieldFighter1;
    [SerializeField]
    private Material forceShieldFighter2;


    public static PlayerInput fighter1;
    public static PlayerInput fighter2;

    #region UI Variable
    public GameObject UI;
    private GameObject UICombat;
    private GameObject menuPause;
    private GameObject menuEndFight;
    private GameObject fightTransition;
    private GameObject timer;
    private UiInGameManager uiInGameManager;
    #endregion

    private static int nbRound;
    private static int nbRoundWinP1;
    private static int nbRoundWinP2;

    public bool onSceneTest;
    public bool useOnlyCesar;
    public bool useOnlyDiane;


    private void Start()
    {
        if (onSceneTest)
        {
            if (useOnlyCesar)
                SetPlayerPrefToFighterCesar();
            else if (useOnlyDiane)
                SetPlayerPrefToFighterDiane();
            else
                SetPlayerPrefToFighter();
        }

        InitGameManager();
        SpawnPlayers();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            StartCoroutine(RoundTransition("P2"));
    }

    public void InitGameManager()
    {
        nbRound = 1;

        uiInGameManager = Instantiate(UI).GetComponent<UiInGameManager>();

        UICombat = uiInGameManager.UICombat;

        menuPause = uiInGameManager.menuPause;

        menuEndFight = uiInGameManager.menuEndOfFight;

        fightTransition = uiInGameManager.fightTransition;

        timer = uiInGameManager.timer;
    }

    public void SpawnPlayers()
    {
        List<Image> whiteHpBarreP1 = uiInGameManager.hpBarreP1.GetComponent<HpBarre>().whiteHpBarre;
        List<Image> redHpBarreP1 = uiInGameManager.hpBarreP1.GetComponent<HpBarre>().redHpBarre;

        fighter1 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].prefab, spawnP1, 1, whiteHpBarreP1, redHpBarreP1, "P1", uiInGameManager.XKeyP2, Gamepad.all[0]);
        targetsGroup.AddMember(fighter1.transform, 1, 5);
        nbRoundWinP1 = 0;


        List<Image> whiteHpBarreP2 = uiInGameManager.hpBarreP2.GetComponent<HpBarre>().whiteHpBarre;
        List<Image> redHpBarreP2 = uiInGameManager.hpBarreP2.GetComponent<HpBarre>().redHpBarre;

        if (Gamepad.all.Count == 1)
            fighter2 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].prefab, spawnP2, -1, whiteHpBarreP2, redHpBarreP2, "P2", uiInGameManager.XKeyP1, Keyboard.current);
        else
            fighter2 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].prefab, spawnP2, -1, whiteHpBarreP2, redHpBarreP2, "P2", uiInGameManager.XKeyP1, Gamepad.all[1]);

        targetsGroup.AddMember(fighter2.transform, 1, 5);
        nbRoundWinP2 = 0;

        fighter1.transform.SetParent(fighters.transform);
        fighter2.transform.SetParent(fighters.transform);
        fighter1.GetComponent<PlayerController>().lightUIFeedback = uiInGameManager.lightUIFeedBackPlayer1;
        fighter1.GetComponent<PlayerController>().mediumUIFeedback = uiInGameManager.mediumUIFeedBackPlayer1;
        fighter1.GetComponent<PlayerController>().heavyUIFeedback = uiInGameManager.heavyUIFeedBackPlayer1;
        fighter1.GetComponent<PlayerController>().charaUIFeedback = uiInGameManager.charaUIFeedbackPlayer1;
        fighter1.GetComponent<PlayerController>().skullUIFeedbackStart = uiInGameManager.skullUIFeedbackStartPlayer1;
        fighter1.GetComponent<PlayerController>().skullUIFeedbackLoop1 = uiInGameManager.skullUIFeedbackLoop1Player1;
        fighter1.GetComponent<PlayerController>().skullUIFeedbackLoop2 = uiInGameManager.skullUIFeedbackLoop2Player1;
        fighter1.GetComponent<PlayerController>().YUIFeedbackLoop = uiInGameManager.YUIFeedbackLoopPlayer1;
        fighter1.GetComponent<PlayerController>().BUIFeedbackLoop = uiInGameManager.BUIFeedbackLoopPlayer1;
        fighter1.GetComponent<PlayerController>().otherPlayer = fighter2.GetComponent<PlayerController>();


        fighter2.GetComponent<PlayerController>().lightUIFeedback = uiInGameManager.lightUIFeedBackPlayer2;
        fighter2.GetComponent<PlayerController>().mediumUIFeedback = uiInGameManager.mediumUIFeedBackPlayer2;
        fighter2.GetComponent<PlayerController>().heavyUIFeedback = uiInGameManager.heavyUIFeedBackPlayer2;
        fighter2.GetComponent<PlayerController>().charaUIFeedback = uiInGameManager.charaUIFeedbackPlayer2;
        fighter2.GetComponent<PlayerController>().skullUIFeedbackStart = uiInGameManager.skullUIFeedbackStartPlayer2;
        fighter2.GetComponent<PlayerController>().skullUIFeedbackLoop1 = uiInGameManager.skullUIFeedbackLoop1Player2;
        fighter2.GetComponent<PlayerController>().skullUIFeedbackLoop2 = uiInGameManager.skullUIFeedbackLoop2Player2;
        fighter2.GetComponent<PlayerController>().YUIFeedbackLoop = uiInGameManager.YUIFeedbackLoopPlayer2;
        fighter2.GetComponent<PlayerController>().BUIFeedbackLoop = uiInGameManager.BUIFeedbackLoopPlayer2;
        fighter2.GetComponent<PlayerController>().otherPlayer = fighter1.GetComponent<PlayerController>();


        //manage mirror match
        //fighter 1
        if ((Characters.ColorType)PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1 + "color") == Characters.ColorType.Mirror) {
            fighter1.GetComponentInChildren<SkinnedMeshRenderer>().material = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].skinMirrorMatch;
            fighter1.GetComponent<PlayerController>().SetParryColor(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].mirrorColor);
        }
        else
            fighter1.GetComponent<PlayerController>().SetParryColor(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].originalColor);

        //fighter 2
        if ((Characters.ColorType)PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2 + "color") == Characters.ColorType.Mirror) {
            fighter2.GetComponentInChildren<SkinnedMeshRenderer>().material = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].skinMirrorMatch;
            fighter2.GetComponent<PlayerController>().SetParryColor(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].mirrorColor);
        }
        else
            fighter2.GetComponent<PlayerController>().SetParryColor(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].originalColor);
    }

    public void SetPlayerPrefToFighterCesar()
    {
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1, 0);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1 + "color", 0);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2, 0);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2 + "color", 1);
    }

    public void SetPlayerPrefToFighterDiane()
    {
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1, 1);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1 + "color", 0);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2, 1);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2 + "color", 1);
    }

    public void SetPlayerPrefToFighter()
    {
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1, 0);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1 + "color", 0);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2, 1);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2 + "color", 0);
    }


    public PlayerInput InitFighter(GameObject prefab, Transform position, int lastDirection, List<Image> whiteHpBarre, List<Image> redHpBarre, string playerName, GameObject Xkey, InputDevice controller)
    {
        PlayerInput fighter = PlayerInput.Instantiate(prefab, controlScheme: "controller", pairWithDevice: controller);
        fighter.transform.position = position.position;
        fighter.GetComponent<PlayerController>().SetUIFighter(whiteHpBarre, redHpBarre, menuPause, UICombat, Xkey, timer.GetComponent<Image>(), playerName);
        fighter.GetComponent<PlayerController>().playerName = playerName;
        fighter.GetComponent<PlayerController>().gameManager = this;
        fighter.GetComponent<PlayerController>().SetArenaLimit(upperLeftLimit, lowerRightLimit);
        fighter.GetComponent<PlayerController>().isStun = true;
        fighter.GetComponent<PlayerController>().lastDirection = lastDirection;
        return fighter;
    }

    public void EndRound(string looser)
    {

        trap.GetComponent<TrapController>().ResetTrap();

        nbRound += 1;

        if (looser.Equals("P1"))
            nbRoundWinP2 += 1;
        if (looser.Equals("P2"))
            nbRoundWinP1 += 1;   

        fighter1.GetComponent<PlayerController>().skullUIFeedbackLoop1.GetFeedbackOfType<MMF_Looper>().InfiniteLoop = false;
        fighter1.GetComponent<PlayerController>().skullUIFeedbackLoop2.GetFeedbackOfType<MMF_Looper>().InfiniteLoop = false;

        fighter2.GetComponent<PlayerController>().skullUIFeedbackLoop1.GetFeedbackOfType<MMF_Looper>().InfiniteLoop = false;
        fighter2.GetComponent<PlayerController>().skullUIFeedbackLoop2.GetFeedbackOfType<MMF_Looper>().InfiniteLoop = false;
        print(fighter2.GetComponent<PlayerController>().skullUIFeedbackLoop2.IsPlaying);        
        UpdateRounBarre();


        if (nbRoundWinP1 == 3 || nbRoundWinP2 == 3)
            StartCoroutine(EndMatch(looser));
        else
            StartCoroutine(RoundTransition(looser));


    }

    private void UpdateRounBarre()
    {
        if (nbRoundWinP1 > 0)
            uiInGameManager.roundWinP1.GetComponent<RoundWinBarre>().roundWinCell[nbRoundWinP1 - 1].sprite = uiInGameManager.roundWinP1.GetComponent<RoundWinBarre>().fullCell;

        if (nbRoundWinP2 > 0)
            uiInGameManager.roundWinP2.GetComponent<RoundWinBarre>().roundWinCell[nbRoundWinP2 - 1].sprite = uiInGameManager.roundWinP2.GetComponent<RoundWinBarre>().fullCell;

    }

    public IEnumerator EndMatch(string looser)
    {
        SetFighterStun();
        mainVirtualCamera.gameObject.SetActive(false);

        if (looser.Equals("P1"))
        {
            SetFighterEndAnimation(fighter1, "End Match Death");
        }
        if (looser.Equals("P2"))
        {
            SetFighterEndAnimation(fighter2, "End Match Death");
        }

        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1f);

        Time.timeScale = 1f;

        yield return new WaitForSeconds(1f);

        fightTransition.GetComponent<Animator>().SetTrigger("Close");

        yield return new WaitForSeconds(0.5f);

        if (looser.Equals("P1"))
        {
            fighter2.transform.position = endMatchPodium.position;
            fighter2.GetComponent<PlayerController>().fighterCam.SetActive(true);
        }
        if (looser.Equals("P2"))
        {
            fighter1.transform.position = endMatchPodium.position;
            fighter1.GetComponent<PlayerController>().fighterCam.SetActive(true);
        }


        trap.SetActive(false);

        uiInGameManager.hpBarres.SetActive(false);
        yield return new WaitForSeconds(1f);
        fightTransition.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);

        if (looser.Equals("P1"))
            fighter2.GetComponent<Animator>().SetTrigger("Victory");
        if (looser.Equals("P2"))
            fighter1.GetComponent<Animator>().SetTrigger("Victory");

        yield return new WaitForSeconds(2f);

        menuEndFight.SetActive(true);
        EventSystem.current.SetSelectedGameObject(uiInGameManager.rematchButton);

    }

    public IEnumerator RoundTransition(string looser)
    {
        SetFighterStun();
        mainVirtualCamera.gameObject.SetActive(false);
        if (looser.Equals("P1"))
        {
            SetFighterEndAnimation(fighter1, "End Round Death");
        }
        if (looser.Equals("P2"))
        {
            SetFighterEndAnimation(fighter2, "End Round Death");
        }

        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1f);

        Time.timeScale = 1f;

        yield return new WaitForSeconds(1f);

        
        fighter1.GetComponent<PlayerController>().skullUIFeedbackLoop1.StopFeedbacks();        
        fighter1.GetComponent<PlayerController>().skullUIFeedbackLoop2.StopFeedbacks();

        fighter2.GetComponent<PlayerController>().skullUIFeedbackLoop1.StopFeedbacks();        
        fighter2.GetComponent<PlayerController>().skullUIFeedbackLoop2.StopFeedbacks();

        fighter1.GetComponent<PlayerController>().skullUIFeedbackLoop1.GetFeedbackOfType<MMF_Looper>().InfiniteLoop = true;
        fighter1.GetComponent<PlayerController>().skullUIFeedbackLoop2.GetFeedbackOfType<MMF_Looper>().InfiniteLoop = true;

        fighter2.GetComponent<PlayerController>().skullUIFeedbackLoop1.GetFeedbackOfType<MMF_Looper>().InfiniteLoop = true;
        fighter2.GetComponent<PlayerController>().skullUIFeedbackLoop2.GetFeedbackOfType<MMF_Looper>().InfiniteLoop = true;

        fightTransition.GetComponent<Animator>().SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        ResetFight();
        yield return new WaitForSeconds(0.5f);
        fightTransition.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(timer.GetComponent<Timer>().TransitionRoundTimer(nbRound));
    }

    private void SetFighterEndAnimation(PlayerInput fighter, string trigger)
    {
        fighter.GetComponent<PlayerController>().fighterCam.SetActive(true);
        fighter.GetComponent<Animator>().SetTrigger(trigger);
    }

    public void ResetFight()
    {
        mainVirtualCamera.gameObject.SetActive(true);

        fighter1.GetComponent<PlayerController>().ResetFighter(1);
        fighter1.transform.position = spawnP1.position;

        fighter2.GetComponent<PlayerController>().ResetFighter(-1);
        fighter2.transform.position = spawnP2.position;

    }


    public static void SetFighterNotStun()
    {
        fighter1.GetComponent<PlayerController>().isStun = false;
        fighter2.GetComponent<PlayerController>().isStun = false;
    }
    public static void SetFighterStun()
    {
        fighter1.GetComponent<PlayerController>().isStun = true;
        fighter2.GetComponent<PlayerController>().isStun = true;
    }

    public static void SetActionMap(string actionMap)
    {
        fighter1.SwitchCurrentActionMap(actionMap);
        fighter2.SwitchCurrentActionMap(actionMap);
    }

    public void DisplayBlurEffect()
    {
        blurEffect.SetActive(true);
    }

    public void HideBlurEffect()
    {
        blurEffect.SetActive(false);
    }

    public void DoFreeze(float duration)
    {
        StartCoroutine(freezer.Freeze(duration));
    }

    public void DoShake(float intensity, float time)
    {
        StartCoroutine(mainVirtualCamera.GetComponent<CameraShake>().Shake(intensity, time));
    }

}
