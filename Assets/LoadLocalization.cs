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
    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        _hommage.DOFade(1, 3).OnComplete(LaunchGame);
    }

    private void LaunchGame()
    {
        _hommage.DOFade(0, 3).OnComplete(() => SceneManager.LoadScene("Menu"));
    }

}
