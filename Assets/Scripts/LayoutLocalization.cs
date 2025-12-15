using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LayoutLocalization : MonoBehaviour
{
    [SerializeField]
    private HorizontalLayoutGroup horizontalLayoutGroup;
    [SerializeField]
    private int spacingFR;
    [SerializeField]
    private int spacingEN;
    [SerializeField]
    private int paddingLeftFR;
    [SerializeField]
    private int paddingLeftEN;
    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocaleChanged += (newLocal) =>
        {
            ChangeHorizontaleLayoutGroup(newLocal);
        };
        ChangeHorizontaleLayoutGroup(LocalizationSettings.SelectedLocale);
    }

    private void ChangeHorizontaleLayoutGroup(Locale newLocal)
    {
        if (newLocal.Identifier.Code.ToString() == "fr")
        {
            horizontalLayoutGroup.spacing = spacingFR;
            horizontalLayoutGroup.padding.left = paddingLeftFR;
        }
        else if (newLocal.Identifier.Code.ToString() == "en")
        {
            horizontalLayoutGroup.spacing = spacingEN;
            horizontalLayoutGroup.padding.left = paddingLeftEN;
        }
    }
}
