using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class LoadLocalization : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _hommage;
    [SerializeField]
    private float appearing = 10f;
    [SerializeField]
    private float disappearing = 3f;
    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("ChoosedLang")];
        yield return new WaitForSeconds(2f);
        _hommage.DOFade(1, appearing).OnComplete(LaunchGame);
    }

    private void LaunchGame()
    {
        _hommage.DOFade(0, disappearing).OnComplete(() => SceneManager.LoadScene("Menu"));
    }

}
