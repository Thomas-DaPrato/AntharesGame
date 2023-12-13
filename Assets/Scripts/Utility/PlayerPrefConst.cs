using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefConst
{
    public string playerPrefFighterP1 = "FighterP1";
    public string playerPrefFighterP2 = "FighterP2";

    private static PlayerPrefConst instance;
    
    public static PlayerPrefConst GetInstance() {
        if (instance == null)
            instance = new PlayerPrefConst();
        return instance;
    }
}
