using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;

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
    [SerializeField]
    private GameObject spotLightP1;
    [SerializeField]
    private GameObject spotLightP2;

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

    private static PlayerInput fighter1;
    private static PlayerInput fighter2;

    #region UI Variable
    public GameObject UI;
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
    public bool useCesar;

    
    private void Start() {
        if (onSceneTest) {
            if (useCesar)
                SetPlayerPrefToFighterCesar();
            else
                SetPlayerPrefToFighterDiane();
        }

        InitGameManager();
        SpawnPlayers();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C))
            StartCoroutine(RoundTransition("P2"));
    }

    public void InitGameManager() {
        nbRound = 1;

        uiInGameManager = Instantiate(UI).GetComponent<UiInGameManager>();

        menuPause = uiInGameManager.menuPause;

        menuEndFight = uiInGameManager.menuEndOfFight;

        fightTransition = uiInGameManager.fightTransition;

        timer = uiInGameManager.timer;
    }

    public void SpawnPlayers() {
        List<Image> whiteHpBarreP1 = uiInGameManager.hpBarreP1.GetComponent<HpBarre>().whiteHpBarre;
        List<Image> redHpBarreP1 = uiInGameManager.hpBarreP1.GetComponent<HpBarre>().redHpBarre;
        
        fighter1 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].prefab, spawnP1, 1, whiteHpBarreP1, redHpBarreP1, "P1", Gamepad.all[0]);
        targetsGroup.AddMember(fighter1.transform, 1, 0);
        nbRoundWinP1 = 0;
        spotLightP1.GetComponent<SpotlightFollow>().fighter = fighter1.gameObject;


        List<Image> whiteHpBarreP2 = uiInGameManager.hpBarreP2.GetComponent<HpBarre>().whiteHpBarre;
        List<Image> redHpBarreP2 = uiInGameManager.hpBarreP2.GetComponent<HpBarre>().redHpBarre;

        if (Gamepad.all.Count == 1)
            fighter2 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].prefab, spawnP2, -1, whiteHpBarreP2, redHpBarreP2, "P2", Keyboard.current);
        else
            fighter2 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].prefab, spawnP2, -1, whiteHpBarreP2, redHpBarreP2, "P2", Gamepad.all[1]);
        
        targetsGroup.AddMember(fighter2.transform, 1, 0);
        nbRoundWinP2 = 0;
        spotLightP2.GetComponent<SpotlightFollow>().fighter = fighter2.gameObject;

        fighter1.transform.SetParent(fighters.transform);
        fighter2.transform.SetParent(fighters.transform);

        //manage mirror match
        //fighter 1
        if ((Characters.ColorType) PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1 + "color") == Characters.ColorType.Mirror)
            fighter1.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].skinMirrorMatch;
        //fighter 2
        if ((Characters.ColorType) PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2 + "color") == Characters.ColorType.Mirror)
            fighter2.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].skinMirrorMatch;

    }

    public void SetPlayerPrefToFighterCesar() {
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1, 0);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1 + "color", 0);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2, 0);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2 + "color", 1);
    }

    public void SetPlayerPrefToFighterDiane() {
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1, 1);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1 + "color", 0);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2, 1);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2 + "color", 1);
    }


    public PlayerInput InitFighter(GameObject prefab, Transform position, int lastDirection, List<Image> whiteHpBarre, List<Image> redHpBarre, string playerName, InputDevice controller) {
        PlayerInput fighter = PlayerInput.Instantiate(prefab, controlScheme: "controller", pairWithDevice: controller);
        fighter.transform.position = position.position;
        fighter.GetComponent<PlayerController>().SetHpBarre(whiteHpBarre,redHpBarre);
        fighter.GetComponent<PlayerController>().SetMenuPause(menuPause);
        fighter.GetComponent<PlayerController>().playerName = playerName;
        fighter.GetComponent<PlayerController>().gameManager = this;
        fighter.GetComponent<PlayerController>().SetArenaLimit(upperLeftLimit, lowerRightLimit);
        fighter.GetComponent<PlayerController>().isStun = true;
        fighter.GetComponent<PlayerController>().lastDirection = lastDirection;

        return fighter;
    }

    public void EndRound(string looser) {

        trap.GetComponent<TrapManager>().ResetTrap();

        nbRound += 1;

        Debug.Log("End Round");
        if (looser.Equals("P1"))
            nbRoundWinP2 += 1;
        if (looser.Equals("P2"))
            nbRoundWinP1 += 1;

        UpdateRounBarre();

        Debug.Log("P1 Round " + nbRoundWinP1);
        Debug.Log("P2 Round " + nbRoundWinP2);


        if (nbRoundWinP1 == 3 || nbRoundWinP2 == 3)
            StartCoroutine(EndMatch(looser));
        else
            StartCoroutine(RoundTransition(looser));


    }

    private void UpdateRounBarre() {
        for (int i = 0; i < nbRoundWinP1; i += 1)
            uiInGameManager.roundWinP1.GetComponent<RoundWinBarre>().roundWinCell[i].color = Color.yellow;
        for (int i = 0; i < nbRoundWinP2; i += 1)
            uiInGameManager.roundWinP2.GetComponent<RoundWinBarre>().roundWinCell[i].color = Color.yellow;
        
    }

    public IEnumerator EndMatch(string looser) {
        SetFighterStun();
        mainVirtualCamera.gameObject.SetActive(false);

        if (looser.Equals("P1")) {
            SetFighterEndAnimation(fighter1, "End Match Death");
        }
        if (looser.Equals("P2")) {
            SetFighterEndAnimation(fighter2, "End Match Death");
        }

        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1f);

        Time.timeScale = 1f;

        yield return new WaitForSeconds(1f);

        fightTransition.GetComponent<Animator>().SetTrigger("Close");
        
        yield return new WaitForSeconds(0.5f);

        if (looser.Equals("P1")) {
            fighter2.transform.position = endMatchPodium.position;
            fighter2.GetComponent<PlayerController>().fighterCam.SetActive(true);
        }
        if (looser.Equals("P2")) {
            fighter1.transform.position = endMatchPodium.position;
            fighter1.GetComponent<PlayerController>().fighterCam.SetActive(true);
        }
        
        yield return new WaitForSeconds(1f);
        fightTransition.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);

        if (looser.Equals("P1"))
            fighter2.GetComponent<Animator>().SetTrigger("Victory");
        if (looser.Equals("P2"))
            fighter1.GetComponent<Animator>().SetTrigger("Victory");

        yield return new WaitForSeconds(2f);

        Debug.Log("P1 win " + nbRoundWinP1 + " rounds, P2 win " + nbRoundWinP2 + " rounds");
        uiInGameManager.hpBarres.SetActive(false);
        menuEndFight.SetActive(true);
        EventSystem.current.SetSelectedGameObject(uiInGameManager.rematchButton);

    }

    public IEnumerator RoundTransition(string looser) {
        SetFighterStun();
        mainVirtualCamera.gameObject.SetActive(false);
        if (looser.Equals("P1")) {
            SetFighterEndAnimation(fighter1,"End Round Death");
        }
        if (looser.Equals("P2")) {
            SetFighterEndAnimation(fighter2, "End Round Death");
        }

        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1f);

        Time.timeScale = 1f;

        yield return new WaitForSeconds(1f);


        fightTransition.GetComponent<Animator>().SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        ResetFight();
        yield return new WaitForSeconds(0.5f);
        fightTransition.GetComponent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(timer.GetComponent<Timer>().TransitionRoundTimer(nbRound));
    }

    private void SetFighterEndAnimation(PlayerInput fighter, string trigger) {
        fighter.GetComponent<PlayerController>().fighterCam.SetActive(true);
        fighter.GetComponent<Animator>().SetTrigger(trigger);
    }

    public void ResetFight() {
        mainVirtualCamera.gameObject.SetActive(true);

        fighter1.GetComponent<PlayerController>().ResetFighter(1);
        fighter1.transform.position = spawnP1.position;
        
        fighter2.GetComponent<PlayerController>().ResetFighter(-1);
        fighter2.transform.position = spawnP2.position;
        
    }
    

    public static void SetFighterNotStun() {
        fighter1.GetComponent<PlayerController>().isStun = false;
        fighter2.GetComponent<PlayerController>().isStun = false;
    }
    public static void SetFighterStun() {
        fighter1.GetComponent<PlayerController>().isStun = true;
        fighter2.GetComponent<PlayerController>().isStun = true;
    }

    public static void SetActionMap(string actionMap) {
        fighter1.SwitchCurrentActionMap(actionMap);
        fighter2.SwitchCurrentActionMap(actionMap);
    }

    public void DisplayBlurEffect() {
        blurEffect.SetActive(true);
    }

    public void HideBlurEffect() {
        blurEffect.SetActive(false);
    }

    public void DoFreeze(float duration) {
        StartCoroutine(freezer.Freeze(duration));
    }

    public void DoShake(float intensity) {
        StartCoroutine(mainVirtualCamera.GetComponent<CameraShake>().Shake(intensity));
    }

}
