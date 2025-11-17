using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToDeviceManager : MonoBehaviour
{
    //*****
    // Singleton pattern
    //*****
    private static AddToDeviceManager _instance;
    public static AddToDeviceManager Instance { get { return _instance; } }

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
    public List<DeviceImageHandler> allDeviceImageHandlerFight;
    public List<DeviceColorHandler> allDeviceColorHandlerFight;

    private void OnEnable()
    {
        foreach (DeviceImageHandler deviceImageHandlerhandler in allDeviceImageHandlerFight)
        {
            deviceImageHandlerhandler.ToDoOnEnable();
        }
        foreach (DeviceColorHandler deviceColorHandlerhandler in allDeviceColorHandlerFight)
        {
            deviceColorHandlerhandler.ToDoOnEnable();
        }
    }
    private void OnDisable()
    {
        foreach (DeviceImageHandler deviceImageHandlerhandler in allDeviceImageHandlerFight)
        {
            deviceImageHandlerhandler.ToDoOnDisable();
        }
        foreach (DeviceColorHandler deviceColorHandlerhandler in allDeviceColorHandlerFight)
        {
            deviceColorHandlerhandler.ToDoOnDisable();
        }
    }

}
