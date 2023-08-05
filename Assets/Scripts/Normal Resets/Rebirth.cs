using System;
using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Rebirth : MonoBehaviour
{
    public GameController GameController;
    public BigDouble rebirth;
    public BigDouble rebirthGain = 1;
    public BigDouble multiplierMultiplier = 1;
    public Button button;
    public TMP_Text rebirthDescription;
    public TMP_Text rebirthButtonText;
    public BigDouble maxLevel;
    
    private BigDouble softcap = 1.5;
    private BigDouble cap = 10;
    private BigDouble hardcap = 10;

    public Rebirth()
    {
        
    }

    public void Start()
    {
        rebirth = GameController.data.rebirth;
        rebirthGain = GameController.data.rebirth_Gain;
        multiplierMultiplier = GameController.data.rebirth_Multiplier_Multiplier;
        softcap = GameController.data.rebirth_Softcap;
        hardcap = GameController.data.rebirth_Hardcap;
        maxLevel = GameController.data.rebirth_Max_Level;
        rebirthDescription.SetText($"Your {GameController.Notation(rebirth, "F2")} rebirth points increase multiplier gain by x{GameController.Notation(multiplierMultiplier, "F2")}");
    }

    public void Update()
    {
            if (GameController.data.level >= 50)
            {
                button.interactable = true;
                if (GameController.data.level > maxLevel)
                {
                    if (GameController.data.level >= 1000 + 50)
                        softcap = 1.1;
                    else
                    {
                        cap = math.pow(10, (GameController.data.level - 50).ToString().Length);
                        hardcap += cap;
                        softcap -= softcap / hardcap;
                        GameController.data.multiplier_Softcap = softcap;
                        GameController.data.multiplier_Hardcap = hardcap;
                    }
                    rebirthGain *= softcap;
                    maxLevel = GameController.data.level;
                }
                rebirthButtonText.SetText(
                    $"You will receive {GameController.Notation(rebirthGain * GameController.prestige.rebirthMultiplier * GameController.rebirthMultiplier, "F0")} rebirth on reset");
                GameController.data.rebirth_Gain = rebirthGain;
            }
            else
            {
                button.interactable = false;
                rebirthButtonText.SetText("Reach level 50 to unlock");
            }
    }

    public void BuyRebirth()
    {
        Reset();
        
        multiplierMultiplier = 1;
        rebirth += rebirthGain * GameController.prestige.rebirthMultiplier * GameController.rebirthMultiplier;
        
        multiplierMultiplier = rebirth.Pow(0.66);
        GameController.data.rebirth_Multiplier_Multiplier = multiplierMultiplier;
        
        rebirthGain = 1;
        GameController.data.rebirth_Gain = 1;
        
        GameController.multiplier.multiplierDescription.SetText($"Your {GameController.Notation(GameController.multiplier.multiplier, "F2")} multiplier points increase exp gain by x{GameController.Notation(GameController.multiplier.expMultiplier, "F2")}");
        rebirthDescription.SetText($"Your {GameController.Notation(rebirth, "F2")} rebirth points increase multiplier gain by x{GameController.Notation(multiplierMultiplier, "F2")}");
    }

    public void ResetRebirth()
    {
        GameController.data.level = 0;
        GameController.data.exp = 0;
        GameController.levels.levelRequierment = 2;
        softcap = 1.5;
        maxLevel = 0;
        cap = 10;
        hardcap = 10;
    }

    public void Reset()
    {
        ResetRebirth();
        GameController.multiplier.ResetMultiplier();
        GameController.multiplier.multiplier = 0;
        GameController.multiplier.expMultiplier = 1;
        GameController.multiplier.multiplierGain = 1;
        GameController.data.multiplier = 0;
        GameController.data.multiplier_Exp_Multiplier = 1;
        GameController.data.multiplier_Gain = 1;
        
        GameController.prestige.ResetPrestige();
        GameController.prestige.prestigeGain = 1;
        GameController.data.prestige_Gain = 1;
    }
}
