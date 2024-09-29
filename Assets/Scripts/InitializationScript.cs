using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class InitializationScript : MonoBehaviour
{
    [SerializeField]
    private LocalizeMenu3DManager localizeMenu3DManager;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Wait for the localization system to initialize
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("ChoosedLang")];
        print(LocalizationSettings.SelectedLocale);
        for (int i = 0; i < localizeMenu3DManager.counterParts.Count; i++)
            {
                localizeMenu3DManager.counterParts[i].gameObject.SetActive(false);
                if (localizeMenu3DManager.counterParts[i].lang == LocalizationSettings.SelectedLocale.Identifier.Code.ToString())
                {
                    localizeMenu3DManager.counterParts[i].gameObject.SetActive(true);
                }
            }
    }

}
