using System;
using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Prestige : MonoBehaviour
{
    public GameController GameController;
    public BigDouble prestige;
    public BigDouble prestigeGain = 1;
    public BigDouble rebirthMultiplier = 1;
    public BigDouble expMultiplier = 1;
    public Button button;
    public TMP_Text prestigeDescription;
    public TMP_Text prestigeButtonText;
    public BigDouble maxLevel;
    
    private BigDouble softcap = 1.5;
    private BigDouble cap = 10;
    private BigDouble hardcap = 10;

    public Prestige()
    {
        expMultiplier = 1;
    }

    public void Start()
    {
        prestige = GameController.data.prestige;
        prestigeGain = GameController.data.prestige_Gain;
        expMultiplier = GameController.data.prestige_Exp_Multiplier;
        rebirthMultiplier = GameController.data.prestige_Rebirth_Multiplier;
        softcap = GameController.data.prestige_Softcap;
        hardcap = GameController.data.prestige_Hardcap;
        maxLevel = GameController.data.prestige_Max_Level;
        prestigeDescription.SetText($"Your {GameController.Notation(prestige, "F2")} prestige points increase rebirth gain by x{GameController.Notation(rebirthMultiplier, "F2")} and exp gain by x{GameController.Notation(expMultiplier, "F2")}");
    }

    public void Update()
    {
            if (GameController.data.level >= 100)
            {
                button.interactable = true;
                if (GameController.data.level > maxLevel)
                {
                    if (GameController.data.level >= 1000 + 100)
                        softcap = 1.1;
                    else
                    {
                        cap = math.pow(10, (GameController.data.level - 100).ToString().Length);
                        hardcap += cap;
                        softcap -= softcap / hardcap;
                        GameController.data.multiplier_Softcap = softcap;
                        GameController.data.multiplier_Hardcap = hardcap;
                    }
                    prestigeGain *= softcap;
                    GameController.data.prestige_Gain = prestigeGain;
                    
                    maxLevel = GameController.data.level;
                }
                prestigeButtonText.SetText(
                    $"You will receive {GameController.Notation(prestigeGain * GameController.prestigeMultiplier, "F0")} prestige on reset");
                GameController.data.prestige_Gain = prestigeGain;
            }
            else
            {
                button.interactable = false;
                prestigeButtonText.SetText("Reach level 100 to unlock");
            }
    }

    public void BuyPrestige()
    {
        Reset();
        
        rebirthMultiplier = 1;
        expMultiplier = 1;

        prestige += prestigeGain * GameController.prestigeMultiplier;
        
        expMultiplier = prestige.Pow(0.66);
        rebirthMultiplier = prestige.Pow(0.44);

        GameController.data.prestige_Exp_Multiplier = expMultiplier;
        GameController.data.prestige_Rebirth_Multiplier = rebirthMultiplier;
        
        prestigeGain = 1;
        GameController.data.prestige_Gain = 1;
        
        GameController.multiplier.multiplierDescription.SetText($"Your {GameController.Notation(GameController.multiplier.multiplier, "F2")} multiplier points increase exp gain by x{GameController.Notation(GameController.multiplier.expMultiplier, "F2")}");
        GameController.rebirth.rebirthDescription.SetText($"Your {GameController.Notation(GameController.rebirth.rebirth, "F2")} rebirth points increase multiplier gain by x{GameController.Notation(GameController.rebirth.multiplierMultiplier, "F2")}");
        prestigeDescription.SetText($"Your {GameController.Notation(prestige, "F2")} prestige points increase rebirth gain by x{GameController.Notation(rebirthMultiplier, "F2")} and exp gain by x{GameController.Notation(expMultiplier, "F2")}");
    }

    public void ResetPrestige()
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
        ResetPrestige();
        GameController.rebirth.ResetRebirth();
        GameController.rebirth.rebirth = 0;
        GameController.rebirth.multiplierMultiplier = 1;
        GameController.rebirth.rebirthGain = 1;
        GameController.data.rebirth = 0;
        GameController.data.rebirth_Multiplier_Multiplier = 1;
        GameController.data.rebirth_Gain = 1;

        GameController.multiplier.ResetMultiplier();
        GameController.multiplier.multiplier = 0;
        GameController.multiplier.expMultiplier = 1;
        GameController.multiplier.multiplierGain = 1;
        GameController.data.multiplier = 0;
        GameController.data.multiplier_Exp_Multiplier = 1;
        GameController.data.multiplier_Gain = 1;
    }
}
