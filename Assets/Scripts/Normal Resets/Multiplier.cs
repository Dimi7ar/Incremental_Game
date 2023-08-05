using System;
using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Multiplier : MonoBehaviour
{
    public GameController GameController;
    public BigDouble multiplier;
    public BigDouble multiplierGain = 1;
    public BigDouble maxLevel;
    public BigDouble expMultiplier = 1;
    public Button button;
    public TMP_Text multiplierDescription;
    public TMP_Text multiplierButtonText;

    public BigDouble softcap = 1.5;
    public BigDouble cap = 10;
    public BigDouble hardcap = 10;

    public Multiplier()
    {
        
    }

    public void Start()
    {
        multiplier = GameController.data.multiplier;
        multiplierGain = GameController.data.multiplier_Gain;
        expMultiplier = GameController.data.multiplier_Exp_Multiplier;
        softcap = GameController.data.multiplier_Softcap;
        hardcap = GameController.data.multiplier_Hardcap;
        maxLevel = GameController.data.multiplier_Max_Level;
        multiplierDescription.SetText($"Your {GameController.Notation(multiplier, "F2")} multiplier points increase exp gain by x{GameController.Notation(expMultiplier, "F2")}");
    }

    public void Update()
    {
        if (GameController.data.level >= 5)
        {
            button.interactable = true;
            if (GameController.data.level > maxLevel)
            {
                if (GameController.data.level >= 1000)
                    softcap = 1.1;
                else
                {
                    cap = math.pow(10, GameController.data.level.ToString().Length);
                hardcap += cap;
                softcap -= softcap / hardcap;
                GameController.data.multiplier_Softcap = softcap;
                GameController.data.multiplier_Hardcap = hardcap;
                }
                
                multiplierGain *= softcap;
                GameController.data.multiplier_Gain = multiplierGain;
                
                maxLevel = GameController.data.level;
                GameController.data.multiplier_Max_Level = maxLevel;
            }
            multiplierButtonText.SetText($"You will receive {GameController.Notation(multiplierGain * GameController.rebirth.multiplierMultiplier * GameController.multiplierMultiplier, "F0")} multiplier on reset");
        }
        else
        {
            button.interactable = false;
            multiplierButtonText.SetText("Reach level 5 to unlock");
        }
        
        
    }

    public void BuyMultiplier()
    {
        Reset();
        
        expMultiplier = 1;
        GameController.data.multiplier_Exp_Multiplier = 1;
        
        multiplier += multiplierGain * GameController.rebirth.multiplierMultiplier * GameController.multiplierMultiplier;
        expMultiplier = multiplier.Pow(0.66);
        
        GameController.data.multiplier = multiplier;
        GameController.data.multiplier_Exp_Multiplier = expMultiplier;
        
        multiplierGain = 1;
        GameController.data.multiplier_Gain = multiplierGain;
        
        multiplierDescription.SetText($"Your {GameController.Notation(multiplier, "F2")} multiplier points increase exp gain by x{GameController.Notation(expMultiplier, "F2")}");
    }

    public void ResetMultiplier()
    {
        GameController.data.level = 0;
        GameController.data.exp = 0;
        GameController.levels.levelRequierment = 2;
        softcap = 1.5;
        maxLevel = 0;
        cap = 10;
        hardcap = 10;
        GameController.data.multiplier_Softcap = softcap;
        GameController.data.multiplier_Max_Level = maxLevel;
        GameController.data.multiplier_Hardcap = hardcap;
        GameController.data.levelRequirement = 2;
    }

    public void Reset()
    {
        ResetMultiplier();
        GameController.rebirth.ResetRebirth();
        GameController.rebirth.rebirthGain = 1;
        GameController.data.rebirth_Gain = 1;
        GameController.prestige.ResetPrestige();
        GameController.prestige.prestigeGain = 1;
        GameController.data.prestige_Gain = 1;
    }
}
