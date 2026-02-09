using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceGreyImageHandler : MonoBehaviour
{    [SerializeField] public bool P1;

    [SerializeField] public Image image;

    [SerializeField] protected Sprite spritePlaystation;
    [SerializeField] protected Vector2 sizePlaystation;
    [SerializeField] protected Sprite spriteXbox;
    [SerializeField] protected Vector2 sizeXbox;
    [SerializeField] protected Sprite spritePCAzer;
    [SerializeField] protected Sprite spritePCQwer;
    [SerializeField] protected Vector2 sizePC;
    [SerializeField] protected Sprite spriteSwitch;
    [SerializeField] protected Vector2 sizeSwitch;
    [SerializeField] private bool isGrey = false;
    [SerializeField] protected Sprite spriteGreyPlaystation;
    [SerializeField] protected Sprite spriteGreyXbox;
    [SerializeField] protected Sprite spriteGreyPCAzer;
    [SerializeField] protected Sprite spriteGreyPCQwer;
    [SerializeField] protected Sprite spriteGreySwitch;

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

    public void SetGreyBool(bool isGrey)
    {
        this.isGrey = isGrey;
        if(P1)
            ChangeImage(DeviceManager.Instance.Player1Controller);
        else
            ChangeImage(DeviceManager.Instance.Player2Controller);
    }

    public void ChangeImage(Controller device)
    {
        switch (device)
        {
            case Controller.Xbox:
                if (isGrey)
                    image.sprite = spriteGreyXbox;
                else
                    image.sprite = spriteXbox;
                image.rectTransform.sizeDelta = sizeXbox;
                break;
            case Controller.PlayStation:
                if (isGrey)
                    image.sprite = spriteGreyPlaystation;
                else
                    image.sprite = spritePlaystation;
                image.rectTransform.sizeDelta = sizePlaystation;
                break;
            case Controller.Switch:
                if (isGrey)
                    image.sprite = spriteGreySwitch;
                else
                    image.sprite = spriteSwitch;
                image.rectTransform.sizeDelta = sizeSwitch;
                break;
            case Controller.PC:
                if (DeviceManager.Instance.fr && spritePCAzer != null)
                {
                    if (isGrey)
                        image.sprite = spriteGreyPCAzer;
                    else
                        image.sprite = spritePCAzer;
                }
                else
                {
                    if (isGrey)
                        image.sprite = spriteGreyPCQwer;
                    else
                        image.sprite = spritePCQwer;
                }

                image.rectTransform.sizeDelta = sizePC;
                break;
        }
    }
}