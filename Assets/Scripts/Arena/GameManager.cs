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
        SetPlayerPrefTo1();
        Instantiate(UI);
        SpawnPlayers();
    }

    public void SpawnPlayers() {
        var fighter1 = PlayerInput.Instantiate(Characters.GetFighters()[PlayerPrefs.GetInt("ChooseFighterP1")].prefab, controlScheme: "controller", pairWithDevice: Gamepad.all[0]);
        fighter1.transform.position = spawnP1.position;
        fighter1.GetComponent<PlayerController>().SetHpBarre(GameObject.Find("Player1Hp").GetComponent<Image>());

        var fighter2 = PlayerInput.Instantiate(Characters.GetFighters()[PlayerPrefs.GetInt("ChooseFighterP2")].prefab, controlScheme: "controller", pairWithDevice: Keyboard.current);
        fighter2.transform.position = spawnP2.position;
        fighter2.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        fighter2.GetComponent<PlayerController>().SetHpBarre(GameObject.Find("Player2Hp").GetComponent<Image>());
    }

    public void SetPlayerPrefTo1() {
        PlayerPrefs.SetInt("ChooseFighterP1", 0);
        PlayerPrefs.SetInt("ChooseFighterP2", 0);
    }

}
