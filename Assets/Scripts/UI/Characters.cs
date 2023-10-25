using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [SerializeField]
    private List<FighterData> fightersData = new List<FighterData>();
    private static List<FighterData> fighters = new List<FighterData>();
    private void Awake() {
        fighters = fightersData;
    }

    public static List<FighterData> GetFighters() {
        return fighters;
    }
}
