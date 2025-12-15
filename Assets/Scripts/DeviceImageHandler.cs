using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class DeviceImageHandler : MonoBehaviour
{
    [SerializeField]
    public bool P1;

    [SerializeField]
    public Image image;

    [SerializeField]
    protected Sprite spritePlaystation;
    [SerializeField]
    private Vector2 sizePlaystation;
    [SerializeField]
    protected Sprite spriteXbox;
    [SerializeField]
    private Vector2 sizeXbox;
    [SerializeField]
    protected Sprite spritePCAzer;
    [SerializeField]
    protected Sprite spritePCQwer;
    [SerializeField]
    private Vector2 sizePC;
    [SerializeField]
    protected Sprite spriteSwitch;
    [SerializeField]
    private Vector2 sizeSwitch;

    public void ToDoOnEnable()
    {
        if (P1)
            DeviceManager.Instance.connectedController1Actions += ChangeImage;
        else
            DeviceManager.Instance.connectedController2Actions += ChangeImage;

        if (P1 && DeviceManager.Instance.Player1Controller == Controller.PC)
        {
            if (DeviceManager.Instance.fr && spritePCAzer != null)
                image.sprite = spritePCAzer;
            else
                image.sprite = spritePCQwer;
        }
        else if (DeviceManager.Instance.Player2Controller == Controller.PC)
        {
            if (DeviceManager.Instance.fr && spritePCAzer != null)
                image.sprite = spritePCAzer;
            else
                image.sprite = spritePCQwer;
        }

    }
    public void ToDoOnDisable()
    {
        if (P1)
            DeviceManager.Instance.connectedController1Actions -= ChangeImage;
        else
            DeviceManager.Instance.connectedController2Actions -= ChangeImage;
    }

    public void ChangeImage(Controller device)
    {
        switch (device)
        {
            case Controller.Xbox:
                image.sprite = spriteXbox;
                image.rectTransform.sizeDelta = sizeXbox;
                break;
            case Controller.PlayStation:
                image.sprite = spritePlaystation;
                image.rectTransform.sizeDelta = sizePlaystation;
                break;
            case Controller.Switch:
                image.sprite = spriteSwitch;
                image.rectTransform.sizeDelta = sizeSwitch;
                break;
            case Controller.PC:
                if (DeviceManager.Instance.fr && spritePCAzer != null)
                {
                    image.sprite = spritePCAzer;
                }
                else
                {
                    image.sprite = spritePCQwer;
                }
                image.rectTransform.sizeDelta = sizePC;
                break;
        }
    }
}
