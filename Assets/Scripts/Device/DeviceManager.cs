using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.Localization.Settings;

[Serializable]
public enum Controller
{
    Xbox,
    PlayStation,
    Switch,
    PC
}
[Serializable]
public enum Player
{
    P1,
    P2
}
[Serializable]
public enum State
{
    Menu,
    Fight
}
public class DeviceManager : MonoBehaviour
{
    //*****
    // Singleton pattern
    //*****
    private static DeviceManager _instance;
    public static DeviceManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    //*****
    // Singleton pattern
    //*****

    //public Dictionary<int, Controller> connectedControllers = new Dictionary<int, Controller>();
    public Action<Controller> connectedController1Actions;
    public Action<Controller> connectedController2Actions;

    public Controller Player1Controller;

    public Controller Player2Controller;

    public bool fr = false;
    public List<DeviceImageHandler> allDeviceImageHandlerMenu;
    public List<DeviceImageHandler> allDeviceImageHandlerFight;
    public List<DeviceColorHandler> allDeviceColorHandlerFight;
    public State state = State.Menu;

    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocaleChanged += RefreshImageLocal;
        RefreshImageLocal(LocalizationSettings.SelectedLocale);
    }

    private void OnEnable()
    {
        switch (state)
        {
            case State.Menu:
                foreach (DeviceImageHandler deviceImageHandlerhandler in allDeviceImageHandlerMenu)
                {
                    deviceImageHandlerhandler.ToDoOnEnable();
                }
                break;
            case State.Fight:
                foreach (DeviceImageHandler deviceImageHandlerhandler in allDeviceImageHandlerFight)
                {
                    deviceImageHandlerhandler.ToDoOnEnable();
                }
                foreach (DeviceColorHandler deviceColorHandlerhandler in allDeviceColorHandlerFight)
                {
                    deviceColorHandlerhandler.ToDoOnEnable();
                }
                break;
        }

    }
    private void OnDisable()
    {
        switch (state)
        {
            case State.Menu:
                foreach (DeviceImageHandler deviceImageHandlerhandler in allDeviceImageHandlerMenu)
                {
                    deviceImageHandlerhandler.ToDoOnDisable();
                }
                break;
            case State.Fight:
                foreach (DeviceImageHandler deviceImageHandlerhandler in allDeviceImageHandlerFight)
                {
                    deviceImageHandlerhandler.ToDoOnDisable();
                }
                foreach (DeviceColorHandler deviceColorHandlerhandler in allDeviceColorHandlerFight)
                {
                    deviceColorHandlerhandler.ToDoOnDisable();
                }
                break;
        }
    }

    public void ChangeStateToMenu()
    {
        OnDisable();
        state = State.Menu;
    }

    public void ChangeStateToFight()
    {
        OnDisable();
        state = State.Fight;
    }

    public void ChangeController(Player player, Controller controller)
    {
        switch (player)
        {
            case Player.P1:
                Player1Controller = controller;
                connectedController1Actions.Invoke(Player1Controller);
                break;

            case Player.P2:
                Player2Controller = controller;
                connectedController2Actions.Invoke(Player2Controller);
                break;
        }
    }
    private void RefreshImageLocal(UnityEngine.Localization.Locale newLocal)
    {
        if (newLocal.Identifier.Code.ToString() == "fr")
        {
            fr = true;
        }
        else
        {
            fr = false;
        }
        ChangeController(Player.P1, Player1Controller);
        ChangeController(Player.P2, Player2Controller);
    }

    /*void Update()
    {
        string[] controllers = Input.GetJoystickNames();
        for (int i = 0; i < controllers.Length; i++)
            print(controllers[i]);
        
        for (int i = 0; i < controllers.Length; i++)
        {
            string controllerName = controllers[i];

            if (!string.IsNullOrEmpty(controllerName) && controllerName.Length > 5)
            {
                if (!connectedControllers.ContainsKey(i))
                {
                    Debug.Log("Controller " + (i + 1) + ": " + controllerName + " connected.");

                    if (controllerName.ToLower().Contains("xbox"))
                    {
                        Debug.Log("Detected Xbox Controller");
                        connectedControllers[i] = Controller.Xbox;
                    }
                    else if (controllerName.ToLower().Contains("dualshock") || controllerName.ToLower().Contains("dual sense"))
                    {
                        Debug.Log("Detected PlayStation Controller");
                        connectedControllers[i] = Controller.PlayStation;
                    }
                    else
                    {
                        Debug.Log("Detected Other Controller: " + controllerName);
                        connectedControllers[i] = Controller.Other;
                    }
                    connectedController1Actions?.Invoke(connectedControllers[1]);
                    connectedController2Actions?.Invoke(connectedControllers[2]);
                }
            }
            else
            {
                if (connectedControllers.ContainsKey(i))
                {
                    Debug.Log("Controller " + (i + 1) + ": " + connectedControllers[i] + " disconnected.");
                    connectedControllers.Remove(i);
                }
            }
        }

        List<int> toRemove = new List<int>();
        foreach (var controller in connectedControllers)
        {
            if (controller.Key >= controllers.Length || string.IsNullOrEmpty(controllers[controller.Key]))
            {
                Debug.Log("Controller " + (controller.Key + 1) + ": " + controller.Value + " disconnected.");
                toRemove.Add(controller.Key);
            }
        }

        foreach (var index in toRemove)
        {
            connectedControllers.Remove(index);
        }
    }*/
}
