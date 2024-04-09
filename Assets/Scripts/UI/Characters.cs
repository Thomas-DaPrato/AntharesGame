using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public List<FighterData> fightersData = new List<FighterData>();

    private Dictionary<FighterData, List<ColorFighter>> colorFighterManager;
    

    public static Characters instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("create character instance");
            instance = this;
            colorFighterManager = new Dictionary<FighterData, List<ColorFighter>>();
            foreach (FighterData fighter in fightersData)
                colorFighterManager.Add(fighter, new List<ColorFighter>() { new ColorFighter(ColorFighter.ColorType.Original), new ColorFighter(ColorFighter.ColorType.Mirror) });

            
        }
    }

    public ColorFighter.ColorType GetAvailableColor(FighterData fighter)
    {
        foreach (ColorFighter colorFighter in colorFighterManager[fighter])
            if (!colorFighter.isPicked)
                return colorFighter.colorType;
        return ColorFighter.ColorType.None;
    }

    public void SetColorFighterIsPickedTrue(FighterData fighter, ColorFighter.ColorType color)
    {
        foreach (ColorFighter colorFighter in colorFighterManager[fighter])
            if (colorFighter.colorType == color)
            {
                colorFighter.isPicked = true;
                //DisplayColorManager();
                return;
            }
    }
    public void SetColorFighterIsPickedFalse(FighterData fighter, ColorFighter.ColorType color)
    {
        foreach (ColorFighter colorFighter in colorFighterManager[fighter])
            if (colorFighter.colorType == color)
            {
                colorFighter.isPicked = false;
                //DisplayColorManager();  
                return;
            }
    }

    public void DisplayColorManager(){
        foreach(KeyValuePair<FighterData, List<ColorFighter>> entry in colorFighterManager){
            Debug.Log(entry.Key.nickName);
            foreach(ColorFighter color in entry.Value)
                Debug.Log("    " + color.colorType + " " + color.isPicked);
        }
    }

    public Sprite GetSpriteNotSelected(FighterData fighter)
    {
        if (GetAvailableColor(fighter) == ColorFighter.ColorType.Original)
            return fighter.spriteOriginalNotSelected;
        else
            return fighter.spriteMirrorNotSelected;
    }

    public Sprite GetSpriteSelected(FighterData fighter)
    {
        if (GetAvailableColor(fighter) == ColorFighter.ColorType.Original)
            return fighter.spriteOriginalSelected;
        else
            return fighter.spriteMirrorSelected;
    }
}

public class ColorFighter
{
    public bool isPicked;
    public ColorType colorType;

    public enum ColorType
    {
        Original,
        Mirror,
        None
    }

    public ColorFighter(ColorType colorType)
    {
        this.colorType = colorType;
        isPicked = false;
    }
}
