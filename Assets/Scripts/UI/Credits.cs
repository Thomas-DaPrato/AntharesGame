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
    private TextMeshProUGUI memberNameLeft;
    [SerializeField]
    private GameObject infosLeft;
    [SerializeField]
    private TextMeshProUGUI memberNameRight;
    [SerializeField]
    private GameObject infosRight;

    private void Awake() {
        currentMember = 0;
        FillInfos();
    }

    public void OnCharacterSwap(InputAction.CallbackContext context) {
        if (context.performed) {
            if (context.ReadValue<float>() > 0) {
                currentMember += 2;
                if (currentMember >= teamData.Count)
                    currentMember = 0;
            }
            if (context.ReadValue<float>() < 0) {
                currentMember -= 2;
                if (currentMember < 0)
                    currentMember = teamData.Count - 2;
            }
            Debug.Log("currentMember " + currentMember);
            FillInfos();
        }
    }

    public void FillInfos() {
        memberNameLeft.text = teamData[currentMember].memberName;
        if (currentMember + 1 < teamData.Count)
            memberNameRight.text = teamData[currentMember + 1].memberName;

        //Set infos left
        for (int i = 0; i < teamData[currentMember].stats.Length; i += 1) {
            infosLeft.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = teamData[currentMember].stats[i].nameStat;
        }
        if (currentMember + 1 < teamData.Count) {
            //Set infos right
            for (int i = 0; i < teamData[currentMember + 1].stats.Length; i += 1) {
                infosRight.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = teamData[currentMember + 1].stats[i].nameStat;
            }
        }
    }


}
