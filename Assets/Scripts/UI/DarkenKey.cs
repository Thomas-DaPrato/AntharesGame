using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkenKey : MonoBehaviour
{
    public Sprite darkenX;
    public Sprite notDarkenX;

    public Image supportKey;
    [SerializeField]
    private DeviceImageCellHandler deviceImageCellHandler;

    public void DarkenXKey()
    {
        if (deviceImageCellHandler.P1)
        {
            supportKey.sprite = deviceImageCellHandler.GetDarken(DeviceManager.Instance.Player1Controller);
        }
        else
        {
            supportKey.sprite = deviceImageCellHandler.GetDarken(DeviceManager.Instance.Player2Controller);
        }

    }

    public void ResetColorSprite()
    {
        if (deviceImageCellHandler.P1)
        {
            supportKey.sprite = deviceImageCellHandler.GetNotDarken(DeviceManager.Instance.Player1Controller);
        }
        else
        {
            supportKey.sprite = deviceImageCellHandler.GetNotDarken(DeviceManager.Instance.Player2Controller);
        }
    }
}
