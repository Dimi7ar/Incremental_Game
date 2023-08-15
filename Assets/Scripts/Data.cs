using System;
using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;

[Serializable]
public class Data 
{
    public BigDouble exp = 0;
    public BigDouble level = 0;
    public BigDouble expPerTick = 1;
    public BigDouble levelRequirement = 2;
    public BigDouble maxLevel = 0;
    
    public BigDouble multiplier = 0;
    public BigDouble multiplier_Exp_Multiplier = 1;
    public BigDouble multiplier_Gain = 1;
    public BigDouble multiplier_Max_Level = 0;
    public BigDouble multiplier_Softcap = 1.5;
    public BigDouble multiplier_Hardcap = 10;

    public BigDouble rebirth = 0;
    public BigDouble rebirth_Multiplier_Multiplier = 1;
    public BigDouble rebirth_Gain = 1;
    public BigDouble rebirth_Max_Level = 0;
    public BigDouble rebirth_Softcap = 1.5;
    public BigDouble rebirth_Hardcap = 10;

    
    public BigDouble prestige = 0;
    public BigDouble prestige_Rebirth_Multiplier = 1;
    public BigDouble prestige_Exp_Multiplier = 1;
    public BigDouble prestige_Gain = 1;
    public BigDouble prestige_Max_Level = 0;
    public BigDouble prestige_Softcap = 1.5;
    public BigDouble prestige_Hardcap = 10;


    public List<int> card_Inventory = new List<int>();
    public int creature_Count = 1;
    public int cards_Available = 9;
    
    public BigDouble card_Exp_Multiplier = 1;
    public BigDouble card_Multiplier_Multiplier = 1;
    public BigDouble card_Rebirth_Multiplier = 1;
    public BigDouble card_Prestige_Multiplier = 1;
    public BigDouble card_Tickspeed_Multiplier = 1;
    public BigDouble card_Exp_Requirement_Decrease = 1;
    
    public int cosmosCount;
    public BigDouble cosmicPower;
    public List<int> cosmic_Bonuses_Obtained = new List<int>();
    public int cosmos_Multiplier = 1;
    public Data()
    {
        creature_Count = 1;
        cards_Available = 9;
    }
}
