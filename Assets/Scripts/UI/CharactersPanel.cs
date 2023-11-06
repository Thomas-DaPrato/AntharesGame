using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharactersPanel : MonoBehaviour
{
    [SerializeField]
    private Image support;

    [SerializeField]
    private GameObject infos;
    
    private int currentFighter;

    private void Awake() {
        currentFighter = 0;
        support.sprite = Characters.GetFighters()[currentFighter].sprite;
        FillInfos();
    }


    public void OnCharacterSwap(InputAction.CallbackContext context) {
        if (context.performed) {
            if (context.ReadValue<float>() > 0) {
                currentFighter += 1;
                if (currentFighter >= Characters.GetFighters().Count)
                    currentFighter = 0;
            }
            if (context.ReadValue<float>() < 0) {
                currentFighter -= 1;
                if (currentFighter < 0)
                    currentFighter = Characters.GetFighters().Count - 1;
            }
            Debug.Log("currentFighter " + currentFighter);
            support.sprite = Characters.GetFighters()[currentFighter].sprite;
            FillInfos();
        }
    }

    public void FillInfos() {
        Transform stats = infos.transform.Find("Stats");
        for (int i = 0; i < Characters.GetFighters()[currentFighter].stats.Length; i += 1) {
            stats.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = Characters.GetFighters()[currentFighter].stats[i].nameStat;

            //Reset color stat
            for (int j = 0; j < 3; j += 1)
                stats.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.black;

            //Set color stat
            for (int j = 0; j < Characters.GetFighters()[currentFighter].stats[i].value; j += 1)
                stats.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.yellow;
        }
    }

    
}
