using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeviceColorTextHandler : MonoBehaviour
{
    [SerializeField]
    public bool P1;
    [SerializeField]
    private TMP_Text textToChangeColor;

    [Header("Color PS")]
    [SerializeField]
    protected List<Color> colorsPlaystation;

    [Header("Color Xbox")]
    [SerializeField]
    protected List<Color> colorsXbox;

    [Header("Color PC")]
    [SerializeField]
    protected List<Color> colorsPC;

    [Header("Color Switch")]
    [SerializeField]
    protected List<Color> colorsSwitch;
    Regex rgx = new Regex(@"(?<=<color=)([^>]+)(?=>)");
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
        int colorIndex = 0;
        string newText = "";
        switch (device)
        {
            case Controller.Xbox:
                newText = rgx.Replace(textToChangeColor.text, (Match m) =>
                {
                    string newColorHex = "#" + colorsXbox[colorIndex].ToHexString();
                    colorIndex++;

                    return newColorHex;
                });
                textToChangeColor.SetText(newText);
                /*foreach (Color color in colorsXbox)
                {
                    textToChangeColor.SetText(rgx.Replace(textToChangeColor.text, "#" + color.ToHexString(), 1));
                }*/
                break;
            case Controller.PlayStation:
                newText = rgx.Replace(textToChangeColor.text, (Match m) =>
                {
                    string newColorHex = "#" + colorsPlaystation[colorIndex].ToHexString();
                    colorIndex++;

                    return newColorHex;
                });
                textToChangeColor.SetText(newText);
                break;
            case Controller.Switch:
                newText = rgx.Replace(textToChangeColor.text, (Match m) =>
                {
                    string newColorHex = "#" + colorsSwitch[colorIndex].ToHexString();
                    colorIndex++;

                    return newColorHex;
                });
                textToChangeColor.SetText(newText);
                break;
            case Controller.PC:
                newText = rgx.Replace(textToChangeColor.text, (Match m) =>
                {
                    string newColorHex = "#" + colorsPC[colorIndex].ToHexString();
                    colorIndex++;

                    return newColorHex;
                });
                textToChangeColor.SetText(newText);
                break;
        }
    }

}
