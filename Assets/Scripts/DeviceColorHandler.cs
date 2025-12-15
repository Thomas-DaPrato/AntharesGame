using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceColorHandler : MonoBehaviour
{
    [SerializeField]
    public bool P1;

    [SerializeField]
    public Image image;

    [SerializeField]
    protected Color colorPlaystation;

    [SerializeField]
    protected Color colorXbox;

    [SerializeField]
    protected Color colorPC;

    [SerializeField]
    protected Color colorSwitch;


    public void ToDoOnEnable()
    {
        if (P1)
            DeviceManager.Instance.connectedController1Actions += ChangeColor;
        else
            DeviceManager.Instance.connectedController2Actions += ChangeColor;
    }

    public void ToDoOnDisable()
    {
        if (P1)
            DeviceManager.Instance.connectedController1Actions -= ChangeColor;
        else
            DeviceManager.Instance.connectedController2Actions -= ChangeColor;
    }
    
    public void ChangeColor(Controller device)
    {
        switch (device)
        {
            case Controller.Xbox:
                image.color = colorXbox;
                break;
            case Controller.PlayStation:
                image.color = colorPlaystation;
                break;
            case Controller.Switch:
                image.color = colorSwitch;
                break;
            case Controller.PC:
                image.color = colorPC;
                break;
        }
    }
}
