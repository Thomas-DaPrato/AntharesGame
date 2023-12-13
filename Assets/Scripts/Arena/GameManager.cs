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

    public GameObject UI;
    private GameObject menuPause;
    private GameObject menuEndFight;

    private static int nbRoundP1;
    private static int nbRoundP2;

    public bool onSceneTest;

    
    private void Awake() {
        if(onSceneTest)
            SetPlayerPrefToFighterAmongUS();
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
    }

    public void SpawnPlayers() {
        fighter1 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1)].prefab, spawnP1, GameObject.Find("Player1Hp").GetComponent<Image>(), "P1", Gamepad.all[0]);
        targetsGroup.AddMember(fighter1.transform, 1, 0);
        nbRoundP1 = 0;

        if(Gamepad.all.Count == 1)
            fighter2 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].prefab, spawnP2, GameObject.Find("Player2Hp").GetComponent<Image>(), "P2", Keyboard.current);
        else
            fighter2 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2)].prefab, spawnP2, GameObject.Find("Player2Hp").GetComponent<Image>(), "P2", Gamepad.all[1]);
        
        fighter2.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
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

    public void SetPlayerPrefToFighterAmongUS() {
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP1, 2);
        PlayerPrefs.SetInt(PlayerPrefConst.GetInstance().playerPrefFighterP2, 2);
    }

    public PlayerInput InitFighter(GameObject prefab, Transform position, Image hpBarre, string playerName, InputDevice controller) {
        PlayerInput fighter = PlayerInput.Instantiate(prefab, controlScheme: "controller", pairWithDevice: controller);
        fighter.transform.position = position.position;
        fighter.GetComponent<PlayerController>().SetHpBarre(hpBarre);
        fighter.GetComponent<PlayerController>().SetMenuPause(menuPause);
        fighter.GetComponent<PlayerController>().playerName = playerName;
        fighter.GetComponent<PlayerController>().gameManager = this;
        fighter.GetComponent<PlayerController>().SetArenaLimit(upperLeftLimit, lowerRightLimit);
        fighter.GetComponent<PlayerController>().isStun = true;


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

        ResetFight();

        if(nbRoundP1 == 3 || nbRoundP2 == 3) {
            Debug.Log("P1 win " + nbRoundP1 + " rounds, P2 win " + nbRoundP2 + " rounds");
            GameObject.Find("HpBarre").SetActive(false);
            menuEndFight.SetActive(true);
            fighter1.GetComponent<PlayerInput>().enabled = false;
            fighter2.GetComponent<PlayerInput>().enabled = false;
            EventSystem.current.SetSelectedGameObject(GameObject.Find("RematchButton"));
        }

    }

    public void ResetFight() {
        fighter1.GetComponent<PlayerController>().ResetFighter();
        fighter1.transform.position = spawnP1.position;
        
        fighter2.GetComponent<PlayerController>().ResetFighter();
        fighter2.transform.position = spawnP2.position;
        fighter2.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        
    }

    public static void SetFighterNotStun() {
        fighter1.GetComponent<PlayerController>().isStun = false;
        fighter2.GetComponent<PlayerController>().isStun = false;
    }

    public void DoFreeze(float duration) {
        StartCoroutine(freezer.Freeze(duration));
    }

    public void DoShake(float duration) {
        StartCoroutine(virtualCamera.GetComponent<CameraShake>().Shake(duration));
    }

}
