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

        p1.user.UnpairDevices();
        p2.user.UnpairDevices();

        InputUser.PerformPairingWithDevice(Gamepad.all[0], p1.user);
        if(Gamepad.all.Count < 2)
            InputUser.PerformPairingWithDevice(Keyboard.current, p2.user);
        else
            InputUser.PerformPairingWithDevice(Gamepad.all[1], p2.user);

    }
}
