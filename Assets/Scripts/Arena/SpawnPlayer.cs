using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    public Transform spawnP1;
    public Transform spawnP2;

    [SerializeField]
    private List<GameObject> fighters = new List<GameObject>();
    
    private void Awake() {
        var fighter1 = PlayerInput.Instantiate(fighters[PlayerPrefs.GetInt("ChooseFighterP1")],controlScheme:"controller",pairWithDevice : Gamepad.all[0]); 
        fighter1.transform.position = spawnP1.position;

        var fighter2 = PlayerInput.Instantiate(fighters[PlayerPrefs.GetInt("ChooseFighterP2")], controlScheme: "controller", pairWithDevice: Keyboard.current);
        fighter2.transform.position = spawnP2.position;
    }
}
