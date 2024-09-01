using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class InitializationScript : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("ChoosedLang")];


    }

}
