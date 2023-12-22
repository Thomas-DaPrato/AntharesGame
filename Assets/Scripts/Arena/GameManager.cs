using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private CinemachineTargetGroup targetsGroup;
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private Freezer freezer;

    
    private Transform spawnP1;
    private Transform spawnP2;

    
    private GameObject upperLeftLimit;
    private GameObject lowerRightLimit;

    private static PlayerInput fighter1;
    private static PlayerInput fighter2;

    #region UI Variable
    public GameObject UI;
    private GameObject menuPause;
    private GameObject menuEndFight;
    private GameObject fightTransition;
    private GameObject timer;
    #endregion

    private static int nbRoundP1;
    private static int nbRoundP2;

    public bool onSceneTest;

    
    private void Start() {
        if(onSceneTest)
            SetPlayerPrefToFighterBourrin();
        InitGameManager();
        SpawnPlayers();
    }

    public void InitGameManager() {
        targetsGroup = GameObject.Find("TargetGroup").GetComponent<CinemachineTargetGroup>();
        virtualCamera = GameObject.Find("VirtualCam").GetComponent<CinemachineVirtualCamera>();

        spawnP1 = GameObject.Find("SpawnP1").transform;
        spawnP2 = GameObject.Find("SpawnP2").transform;

        upperLeftLimit = GameObject.Find("UpLimit");
        lowerRightLimit = GameObject.Find("DownLimit");

        Instantiate(UI);

        menuPause = GameObject.Find("MenuPause");
        menuPause.SetActive(false);

        menuEndFight = GameObject.Find("MenuEndFight");
        menuEndFight.SetActive(false);

        fightTransition = GameObject.Find("FightTransition");

        timer = GameObject.Find("Timer");
    }

    public void SpawnPlayers() {
        fighter1 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].prefab, spawnP1, 1, GameObject.Find("HpBarreP1").GetComponent<HpBarre>().whiteHpBarre, GameObject.Find("HpBarreP1").GetComponent<HpBarre>().redHpBarre, "P1", Gamepad.all[0]);
        targetsGroup.AddMember(fighter1.transform, 1, 0);
        nbRoundP1 = 0;

        if(Gamepad.all.Count == 1)
            fighter2 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].prefab, spawnP2, -1, GameObject.Find("HpBarreP2").GetComponent<HpBarre>().whiteHpBarre, GameObject.Find("HpBarreP2").GetComponent<HpBarre>().redHpBarre, "P2", Keyboard.current);
        else
            fighter2 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].prefab, spawnP2, -1, GameObject.Find("HpBarreP2").GetComponent<HpBarre>().whiteHpBarre, GameObject.Find("HpBarreP2").GetComponent<HpBarre>().redHpBarre, "P2", Gamepad.all[1]);
        
        targetsGroup.AddMember(fighter2.transform, 1, 0);
        nbRoundP2 = 0;

        fighter1.transform.SetParent(GameObject.Find("Fighters").transform);
        fighter2.transform.SetParent(GameObject.Find("Fighters").transform);

        if (PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1) == PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)) {
            if(fighter2.GetComponentInChildren<SkinnedMeshRenderer>() != null)
                fighter2.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].skinMirrorMatch;

            if (fighter2.GetComponentInChildren<MeshRenderer>() != null) {
                fighter2.GetComponentInChildren<MeshRenderer>().sharedMaterial = Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].skinMirrorMatch;
            }
        }
    }

    public void SetPlayerPrefToFighterBourrin() {
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1, 0);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2, 0);
    }
    
    public void SetPlayerPrefToFighterAmongUs() {
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1, 2);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2, 2);
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
        //fighter.GetComponent<PlayerController>().deadCam.SetActive(false);


        return fighter;
    }

    public void EndRound(string looser) {
        Debug.Log("End Round");
        if (looser.Equals("P1"))
            nbRoundP2 += 1;
        if (looser.Equals("P2"))
            nbRoundP1 += 1;

        Debug.Log("P1 Round " + nbRoundP1);
        Debug.Log("P2 Round " + nbRoundP2);

        StartCoroutine(Transition(looser));

        if(nbRoundP1 == 3 || nbRoundP2 == 3) {
            Debug.Log("P1 win " + nbRoundP1 + " rounds, P2 win " + nbRoundP2 + " rounds");
            GameObject.Find("HpBarre").SetActive(false);
            menuEndFight.SetActive(true);
            fighter1.GetComponent<PlayerInput>().enabled = false;
            fighter2.GetComponent<PlayerInput>().enabled = false;
            EventSystem.current.SetSelectedGameObject(GameObject.Find("RematchButton"));
        }

    }

    public IEnumerator Transition(string looser) {
        SetFighterStun();

        if (looser.Equals("P1")) {
            //fighter1.GetComponent<PlayerController>().deadCam.SetActive(true);
            fighter1.GetComponent<Animator>().SetTrigger("End Round Death");
        }
        if (looser.Equals("P2")) {
            //fighter2.GetComponent<PlayerController>().deadCam.SetActive(true);
            fighter2.GetComponent<Animator>().SetTrigger("End Round Death");
        }

        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(0.5f);

        Time.timeScale = 1f;

        
        fightTransition.GetComponent<Animator>().SetTrigger("Close");
        yield return new WaitForSeconds(1);
        ResetFight();
        yield return new WaitForSeconds(1);
        fightTransition.GetComponent<Animator>().SetTrigger("Open");
        SetFighterNotStun();
    }

    public void ResetFight() {
        fighter1.GetComponent<PlayerController>().ResetFighter(1);
        //fighter1.GetComponent<PlayerController>().deadCam.SetActive(false);
        fighter1.transform.position = spawnP1.position;
        
        fighter2.GetComponent<PlayerController>().ResetFighter(-1);
        //fighter2.GetComponent<PlayerController>().deadCam.SetActive(false);
        fighter2.transform.position = spawnP2.position;
        
    }
    
    public static void DisableFighterAnimator() {
        fighter1.GetComponent<Animator>().enabled = false;
        fighter2.GetComponent<Animator>().enabled = false;
    }
    public static void EnableFighterAniamtor() {
        fighter1.GetComponent<Animator>().enabled = true;
        fighter2.GetComponent<Animator>().enabled = true;
    }

    public static void SetFighterNotStun() {
        fighter1.GetComponent<PlayerController>().isStun = false;
        fighter2.GetComponent<PlayerController>().isStun = false;
    }
    public static void SetFighterStun() {
        fighter1.GetComponent<PlayerController>().isStun = true;
        fighter2.GetComponent<PlayerController>().isStun = true;
    }

    public void DoFreeze(float duration) {
        StartCoroutine(freezer.Freeze(duration));
    }

    public void DoShake(float duration) {
        StartCoroutine(virtualCamera.GetComponent<CameraShake>().Shake(duration));
    }

}
