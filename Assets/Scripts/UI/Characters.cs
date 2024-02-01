using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    [SerializeField]
    private List<FighterData> fightersData = new List<FighterData>();
    private static List<FighterData> fighters = new List<FighterData>();
    private static List<ColorType> initialAvailableColor = new List<ColorType>();
    public static Dictionary<int, List<ColorType>> availableColorForFighter;

    private static bool isInit = false;


    public enum ColorType{
        Original,
        Mirror,
        None
    }



    private void Awake() {
        Debug.Log("init");
        if (!isInit) {
            isInit = true;
            fighters = fightersData;
            initialAvailableColor.Add(ColorType.Original);
            initialAvailableColor.Add(ColorType.Mirror);
        }


        InitDicoAvailableColor();
    }

    public static List<FighterData> GetFighters() {
        return fighters;
    }

    public static void InitDicoAvailableColor() {
        availableColorForFighter = new Dictionary<int, List<ColorType>>();
        foreach (FighterData fighter in fighters)
            availableColorForFighter.Add(fighters.IndexOf(fighter), new List<ColorType>(initialAvailableColor));
    }


    public static void ResetAvailableColor(int index) {
        availableColorForFighter[index] = initialAvailableColor;
    }
}
