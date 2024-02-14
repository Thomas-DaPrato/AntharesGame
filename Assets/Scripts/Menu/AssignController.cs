using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class AssignController : MonoBehaviour
{
    public PlayerInput p1; 
    public PlayerInput p2; 

    private void OnEnable() {
        Debug.Log("assign");

        p1.user.UnpairDevices();
        p2.user.UnpairDevices();

        InputUser.PerformPairingWithDevice(Gamepad.all[0], p1.user);
        if (Gamepad.all.Count == 1) {
            Debug.Log("keyBoard");
            InputUser.PerformPairingWithDevice(Keyboard.current, p2.user);
        }
        else
            InputUser.PerformPairingWithDevice(Gamepad.all[1], p2.user);
    }

}
