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
    public double expMultiplier;
    public Button button;
    public TMP_Text multiplierDescription;
    public TMP_Text multiplierButtonText;

    private double multiplierSoftcap = 1.25;
    private int maxLength = 0;
    private double softcap = 1;

    public Multiplier()
    {
        
    }
    public void Update()
    {
        if (GameController.data.level >= 5)
        {
            button.interactable = true;
            if (GameController.data.level > maxLevel)
            {
                int length = ((int)multiplier).ToString().Length;
                if (GameController.data.level > 25)
                {
                    if (maxLength < length)
                    {
                        multiplierSoftcap = 1 + (softcap / 10);
                        maxLength = length;
                    }
                    multiplierGain *= multiplierSoftcap;
                }
                else if (GameController.data.level > 100)
                {
                    if (maxLength < length)
                    {   
                        multiplierSoftcap = 1 + (softcap / 100);
                        maxLength = length;
                    }
                    multiplierGain *= multiplierSoftcap;
                }
                else
                {
                    multiplierGain *= multiplierSoftcap;
                }
                maxLevel = GameController.data.level;

            }
            multiplierButtonText.SetText($"You will receive {GameController.ConvertNumber(multiplierGain, 0)} multiplier on reset");
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
        GameController.data.level = 0;
        GameController.data.exp = 0;
        GameController.levels.levelRequierment = 2;
        expMultiplier = 1;
        multiplier += multiplierGain;
        expMultiplier = math.pow(multiplier, multiplier / (multiplier * 2));
        multiplierGain = 1;
        maxLevel = 0;
        GameController.data.multiplier = multiplier;
        GameController.data.expMultiplier = expMultiplier;
        multiplierDescription.SetText($"Your {GameController.ConvertNumber(multiplier, 0)} multiplier points increase exp gain by x{GameController.ConvertNumber(expMultiplier, 2)}");
    }
}
