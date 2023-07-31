using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data 
{
    public double exp = 0;
    public double level = 0;
    public double expPerTick = 1;
    public double levelRequirement = 2;
    public double maxLevel = 0;
    
    public double multiplier = 0;
    public double multiplier_Exp_Multiplier = 1;
    public double multiplier_Gain = 1;
    public double multiplier_Max_Level = 0;
    public double multiplier_Softcap = 1.5;
    public double multiplier_Hardcap = 10;

    public double rebirth = 0;
    public double rebirth_Multiplier_Multiplier = 1;
    public double rebirth_Gain = 1;
    public double rebirth_Max_Level = 0;
    public double rebirth_Softcap = 1.5;
    public double rebirth_Hardcap = 10;

    
    public double prestige = 0;
    public double prestige_Rebirth_Multiplier = 1;
    public double prestige_Exp_Multiplier = 1;
    public double prestige_Gain = 1;
    public double prestige_Max_Level = 0;
    public double prestige_Softcap = 1.5;
    public double prestige_Hardcap = 10;


    public List<int> card_Inventory = new List<int>();
    public int creature_Count = 1;
    public int cards_Available = 9;
    
    public double card_Exp_Multiplier = 1;
    public double card_Multiplier_Multiplier = 1;
    public double card_Rebirth_Multiplier = 1;
    public double card_Prestige_Multiplier = 1;
    public double card_Tickspeed_Multiplier = 1;
    public double card_Exp_Requirement_Decrease = 1;
    
    public int cosmosCount;
    public int cosmicPower;
    public List<int> cosmic_Bonuses_Obtained = new List<int>();
    public Data()
    {
        creature_Count = 1;
    }
}
