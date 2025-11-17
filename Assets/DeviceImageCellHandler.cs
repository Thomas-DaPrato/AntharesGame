using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceImageCellHandler : DeviceImageHandler
{
    [SerializeField]
    private Sprite darkenSpritePlaystation;
    [SerializeField]
    private Sprite darkenSpriteXbox;
    [SerializeField]
    private Sprite darkenSpritePCAzer;
    [SerializeField]
    private Sprite darkenSpritePCQwer;
    [SerializeField]
    private Sprite darkenSpriteSwitch;

    public Sprite GetNotDarken(Controller device)
    {
        switch (device)
        {
            case Controller.Xbox:
                return spriteXbox;
            case Controller.PlayStation:
                return spritePlaystation;
            case Controller.Switch:
                return spriteSwitch;
            case Controller.PC:
                if (DeviceManager.Instance.fr && spritePCAzer != null)
                {
                    return spritePCAzer;
                }
                else
                {
                    return spritePCQwer;
                }
            default:
                return null;
        }
    }
    public Sprite GetDarken(Controller device)
    {
        switch (device)
        {
            case Controller.Xbox:
                return darkenSpriteXbox;
            case Controller.PlayStation:
                return darkenSpritePlaystation;
            case Controller.Switch:
                return darkenSpriteSwitch;
            case Controller.PC:
                if (DeviceManager.Instance.fr && spritePCAzer != null)
                {
                    return darkenSpritePCAzer;
                }
                else
                {
                    return darkenSpritePCQwer;
                }
            default:
                return null;
        }
    }
}
