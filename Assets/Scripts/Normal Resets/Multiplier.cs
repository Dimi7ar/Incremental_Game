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

    private double multiplierSoftcap = 1.5;
    private double hardcap = 25;
    private double softcap = 0;

    public Multiplier()
    {
        expMultiplier = 1;
    }
    public void Update()
    {
        if (GameController.data.level >= 5)
        {
            button.interactable = true;
            if (GameController.data.level > maxLevel)
            {
                if (GameController.data.level <= 15)
                {
                    softcap += hardcap;
                    multiplierSoftcap -= multiplierSoftcap / softcap;
                }
                else if (GameController.data.level <= 10000)
                {
                    hardcap = 40;
                    softcap += hardcap;
                    multiplierSoftcap -= multiplierSoftcap / softcap;
                }
                else
                {   
                    hardcap = 100;
                    softcap += hardcap;
                    multiplierSoftcap -= multiplierSoftcap / softcap;
                }

                multiplierGain *= multiplierSoftcap;
                maxLevel = GameController.data.level;
            }
            multiplierButtonText.SetText($"You will receive {GameController.ConvertNumber(multiplierGain * GameController.rebirth.multiplierMultiplier * GameController.multiplierMultiplier, 0)} multiplier on reset");
            GameController.data.multiplierGain = multiplierGain;
        }
        else
        {
            button.interactable = false;
            multiplierButtonText.SetText("Reach level 5 to unlock");
        }
        
        
    }

    public void BuyMultiplier()
    {
        ResetMultiplier();
        GameController.rebirth.ResetRebirth();
        GameController.rebirth.rebirthGain = 1;
        GameController.prestige.ResetPrestige();
        GameController.prestige.prestigeGain = 1;
        
        expMultiplier = 1;
        multiplier += multiplierGain * GameController.rebirth.multiplierMultiplier * GameController.multiplierMultiplier;
        
        expMultiplier = math.pow(multiplier, 0.66);
        GameController.data.multiplier = multiplier;
        GameController.data.expMultiplier = expMultiplier;
        multiplierGain = 1;
        
        multiplierDescription.SetText($"Your {GameController.ConvertNumber(multiplier, 0)} multiplier points increase exp gain by x{GameController.ConvertNumber(expMultiplier, 2)}");
    }

    public void ResetMultiplier()
    {
        GameController.data.level = 0;
        GameController.data.exp = 0;
        GameController.levels.levelRequierment = 2;
        multiplierSoftcap = 1.5;
        maxLevel = 0;
        hardcap = 25;
        softcap = 0;
    }
}
