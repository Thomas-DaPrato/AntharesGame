using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class InitializationScript : MonoBehaviour
{
    [SerializeField]
    private LocalizeMenu3DManager localizeMenu3DManager;

    [SerializeField]
    private GameObject[] slidersAudio;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        foreach (GameObject slider in slidersAudio)
        {
            slider.GetComponent<MMSoundManagerTrackVolumeSlider>().UpdateVolume(PlayerPrefs.GetFloat($"volume{slider.GetComponent<SliderUpdateValue>().selectedOption}"));
        }
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
        if (PlayerPrefs.GetInt("QualityValue") == 0 && QualitySettings.GetQualityLevel() == 2)
        {
            PlayerPrefs.SetInt("QualityValue", QualitySettings.GetQualityLevel());
        }
        else
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityValue"));
        }

        if (PlayerPrefs.GetInt("HapticSettings") == 0)
        {
            PlayerPrefs.SetInt("HapticSettings", 1);
        }
    }

}
