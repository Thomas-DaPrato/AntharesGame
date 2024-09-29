using System.Collections;
using System.Collections.Generic;
using Lofelt.NiceVibrations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VibrationScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private Image checkmark;
    [SerializeField]
    private Image bg;
    [SerializeField]
    private Color notSelectColor = Color.white;
    [SerializeField]
    private Color selectColor = new Color(1, 0.8235f, 0);

    public void Start()
    {
        if (PlayerPrefs.GetInt("HapticSettings") == 1)
        {
            toggle.isOn = true;
            HapticController.hapticsEnabled = true;
        }
        else if (PlayerPrefs.GetInt("HapticSettings") == -1)
        {
            toggle.isOn = false;
            HapticController.hapticsEnabled = false;
        }
        print(PlayerPrefs.GetInt("HapticSettings"));
        print(toggle.isOn);

    }
    public void ChangeVibration()
    {
        if (toggle.isOn)
        {
            HapticController.hapticsEnabled = true;
            PlayerPrefs.SetInt("HapticSettings", 1);
        }
        else
        {
            HapticController.hapticsEnabled = false;
            PlayerPrefs.SetInt("HapticSettings", -1);
        }
        print(HapticController.hapticsEnabled);
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        checkmark.color = selectColor;
        bg.color = selectColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        checkmark.color = notSelectColor;
        bg.color = notSelectColor;
    }
}
