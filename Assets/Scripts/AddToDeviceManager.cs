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
    public List<DeviceGreyImageHandler> allDeviceGreyImageHandlerFight;
    public List<DeviceColorHandler> allDeviceColorHandlerFight;

    private void OnEnable()
    {
        foreach (DeviceImageHandler deviceImageHandler in allDeviceImageHandlerFight)
        {
            deviceImageHandler.ToDoOnEnable();
        }
        foreach (DeviceColorHandler deviceColorHandler in allDeviceColorHandlerFight)
        {
            deviceColorHandler.ToDoOnEnable();
        }
        foreach (DeviceGreyImageHandler deviceGreyImageHandler in allDeviceGreyImageHandlerFight)
        {
            deviceGreyImageHandler.ToDoOnEnable();
        }
        
    }
    private void OnDisable()
    {
        foreach (DeviceImageHandler deviceImageHandler in allDeviceImageHandlerFight)
        {
            deviceImageHandler.ToDoOnDisable();
        }
        foreach (DeviceColorHandler deviceColorHandler in allDeviceColorHandlerFight)
        {
            deviceColorHandler.ToDoOnDisable();
        }
        foreach (DeviceGreyImageHandler deviceGreyImageHandler in allDeviceGreyImageHandlerFight)
        {
            deviceGreyImageHandler.ToDoOnDisable();
        }
    }

}
