using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Multiplier : MonoBehaviour
{
    public GameController GameController;
    public double multiplier;
    public double multiplierGain = 1;
    public double maxLevel;
    public double expMultiplier = 1;
    public Button button;
    public TMP_Text multiplierDescription;
    public TMP_Text multiplierButtonText;

    public double softcap = 1.5;
    public double cap = 10;
    public double hardcap = 10;

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
        multiplierDescription.SetText($"Your {GameController.ConvertNumber(multiplier, 0)} multiplier points increase exp gain by x{GameController.ConvertNumber(expMultiplier, 2)}");
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
            multiplierButtonText.SetText($"You will receive {GameController.ConvertNumber(multiplierGain * GameController.rebirth.multiplierMultiplier * GameController.multiplierMultiplier, 0)} multiplier on reset");
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
        expMultiplier = math.pow(multiplier, 0.66);
        
        GameController.data.multiplier = multiplier;
        GameController.data.multiplier_Exp_Multiplier = expMultiplier;
        
        multiplierGain = 1;
        GameController.data.multiplier_Gain = multiplierGain;
        
        multiplierDescription.SetText($"Your {GameController.ConvertNumber(multiplier, 0)} multiplier points increase exp gain by x{GameController.ConvertNumber(expMultiplier, 2)}");
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
