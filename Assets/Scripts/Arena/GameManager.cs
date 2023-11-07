using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform spawnP1;
    public Transform spawnP2;

    public GameObject UI;

    
    private void Awake() {
        //SetPlayerPrefTo1();
        Instantiate(UI);
        SpawnPlayers();
    }

    public void SpawnPlayers() {
        InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt("ChooseFighterP1")].prefab, spawnP1, GameObject.Find("Player1Hp").GetComponent<Image>(), Gamepad.all[0]);

        PlayerInput fighter2 = InitFighter(Characters.GetFighters()[PlayerPrefs.GetInt("ChooseFighterP2")].prefab, spawnP2, GameObject.Find("Player2Hp").GetComponent<Image>(), Keyboard.current);
        fighter2.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

        GameObject.Find("MenuPause").SetActive(false);
    }

    public void SetPlayerPrefTo1() {
        PlayerPrefs.SetInt("ChooseFighterP1", 0);
        PlayerPrefs.SetInt("ChooseFighterP2", 0);
    }

    public PlayerInput InitFighter(GameObject prefab, Transform position, Image hpBarre, InputDevice controller) {
        PlayerInput fighter = PlayerInput.Instantiate(prefab, controlScheme: "controller", pairWithDevice: controller);
        fighter.transform.position = position.position;
        fighter.GetComponent<PlayerController>().SetHpBarre(hpBarre);
        fighter.GetComponent<PlayerController>().SetMenuPause(GameObject.Find("MenuPause"));

        return fighter;

    }

}
