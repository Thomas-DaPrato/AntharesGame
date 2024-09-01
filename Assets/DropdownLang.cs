using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class DropdownLang : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    void OnEnable()
    {
        dropdown.value = PlayerPrefs.GetInt("ChoosedLang");
    }
    public void ChangeLang()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[dropdown.value];
        PlayerPrefs.SetInt("ChoosedLang", dropdown.value);
    }
}
