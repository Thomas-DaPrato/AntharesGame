using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerIcone : MonoBehaviour
{
    public Sprite P1Icone;
    public Sprite P2Icone;

    public Image support;
    

    public void Init(string playerName) {
        if (playerName.Equals("P1"))
            support.sprite = P1Icone;
        else
            support.sprite = P2Icone;
    }
}
