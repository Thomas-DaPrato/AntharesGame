using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceImageHandler : MonoBehaviour
{
    [SerializeField]
    private bool P1;

    [SerializeField]
    public Image image;

    [SerializeField]
    private Sprite spritePlaystation;
    [SerializeField]
    private Vector2 sizePlaystation;
    [SerializeField]
    private Sprite spriteXbox;
    [SerializeField]
    private Vector2 sizeXbox;
    [SerializeField]
    private Sprite spritePC;
    [SerializeField]
    private Vector2 sizePC;
    [SerializeField]
    private Sprite spriteSwitch;
    [SerializeField]
    private Vector2 sizeSwitch;

    private void OnEnable()
    {
        if (P1)
            DeviceManager.Instance.connectedController1Actions += ChangeImage;
        else
            DeviceManager.Instance.connectedController2Actions += ChangeImage;
    }
    private void OnDisable()
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
                image.sprite = spritePC;
                image.rectTransform.sizeDelta = sizePC;
                break;
        }
    }
}
