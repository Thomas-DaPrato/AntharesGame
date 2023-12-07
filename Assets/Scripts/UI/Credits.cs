using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField]
    private List<TeamData> teamData = new List<TeamData>();
    private int currentMember;

    [SerializeField]
    private Image support;
    [SerializeField]
    private GameObject infos;
    [SerializeField]
    private TextMeshProUGUI name;

    private void Awake() {
        currentMember = 0;
        support.sprite = teamData[currentMember].sprite;
        FillInfos();
    }

    public void OnCharacterSwap(InputAction.CallbackContext context) {
        if (context.performed) {
            if (context.ReadValue<float>() > 0) {
                currentMember += 1;
                if (currentMember >= teamData.Count)
                    currentMember = 0;
            }
            if (context.ReadValue<float>() < 0) {
                currentMember -= 1;
                if (currentMember < 0)
                    currentMember = teamData.Count - 1;
            }
            Debug.Log("currentFighter " + currentMember);
            support.sprite = teamData[currentMember].sprite;
            FillInfos();
        }
    }

    public void FillInfos() {
        name.text = teamData[currentMember].name;
        for (int i = 0; i < teamData[currentMember].stats.Length; i += 1) {
            infos.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = teamData[currentMember].stats[i].nameStat;

            //Reset color stat
            for (int j = 0; j < 3; j += 1)
                infos.transform.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.black;

            //Set color stat
            for (int j = 0; j < teamData[currentMember].stats[i].value; j += 1)
                infos.transform.GetChild(i).GetChild(1).GetChild(j).GetComponent<Image>().color = Color.yellow;
        }
    }


}
