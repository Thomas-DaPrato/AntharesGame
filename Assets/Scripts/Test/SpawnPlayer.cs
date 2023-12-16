using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    public Transform playerSpawn;
    public GameObject fighterPrefab;
    private GameObject fighter;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerInput fighter = PlayerInput.Instantiate(fighterPrefab, controlScheme: "controller", pairWithDevice: Gamepad.all[0]);
        fighter = Instantiate(fighterPrefab);
        fighter.transform.position = playerSpawn.position;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            fighter.GetComponent<Animator>().Rebind();
        }
    }
}
