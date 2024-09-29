using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsQuality : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    void Start()
    {
        resolutionDropdown.value = QualitySettings.GetQualityLevel();
    }
    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(resolutionDropdown.value);
        PlayerPrefs.SetInt("QualityValue",QualitySettings.GetQualityLevel());
    }
}
