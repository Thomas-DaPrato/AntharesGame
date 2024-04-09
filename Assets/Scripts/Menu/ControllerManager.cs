using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class ControllerManager : MonoBehaviour
{
    public PlayerInput p1;
    public PlayerInput p2;

    public bool isSameInput;

    public void OnEnable() {
        AssignController();
    }

    public void AssignController(){
        Debug.Log("Assign");
        int nbController = Gamepad.all.Count;
        switch (nbController){
            case 0 :
                if(isSameInput){
                    Debug.Log("TwoPlayers");
                    p1.SwitchCurrentControlScheme("KeyboardTwoPlayers", Keyboard.current);}
                else{
                    p1.SwitchCurrentControlScheme("KeyboardPlayerLeft", Keyboard.current);
                    //InputUser.PerformPairingWithDevice(Keyboard.current, p1.user);
                    p2.SwitchCurrentControlScheme("KeyboardPlayerRight", Keyboard.current);
                    //InputUser.PerformPairingWithDevice(Keyboard.current, p2.user);
                }
                break;
            case 1 :
                p1.SwitchCurrentControlScheme("KeyboardPlayerLeft",Keyboard.current);
                p2?.SwitchCurrentControlScheme("Controller", Gamepad.all[0]);
                break;
            case 2 : 
                p1.SwitchCurrentControlScheme("Controller", Gamepad.all[0]);
                p2?.SwitchCurrentControlScheme("Controller", Gamepad.all[1]);
                break;
            default :
                Debug.LogError("Too much controller connected");
                break;
        }
    }

}
