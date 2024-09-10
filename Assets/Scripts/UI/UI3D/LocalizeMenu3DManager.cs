using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

[System.Serializable]
public class LangToGameObject
{
    public string lang;
    public GameObject gameObject;
}

public class LocalizeMenu3DManager : MonoBehaviour
{
    [SerializeField]
    private List<LangToGameObject> counterParts;
    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocaleChanged += (newLocal) =>
        {
            for (int i = 0; i < counterParts.Count; i++)
            {
                counterParts[i].gameObject.SetActive(false);
                if (counterParts[i].lang == newLocal.Identifier.Code.ToString())
                {
                    counterParts[i].gameObject.SetActive(true);
                }
            }
            GetComponent<UI3DManager>().ChangeCurrentList();
        };
    }

}
