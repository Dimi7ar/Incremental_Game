using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Multiplier : MonoBehaviour
{
    public GameController GameController;
    public double multiplier;
    public double multiplierGain = 1;
    public double maxLevel;
    public double exp;
    public Button button;
    public TMP_Text multiplierDescription;
    public TMP_Text multiplierButtonText;

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
                if (maxLevel > 25)
                {
                    multiplierGain *= 1.01;
                }
                else
                {
                    multiplierGain *= 1.1;
                }
            }
            maxLevel = GameController.data.level;
            multiplierButtonText.SetText($"You will receive {GameController.ConvertNumber(multiplierGain)} multiplier on reset");
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
        GameController.levels.levelRequierment = 10;
        exp = 1;
        multiplier += multiplierGain;
        exp = math.pow(multiplier, multiplier / (multiplier * 5));
        multiplierDescription.SetText($"Your {GameController.ConvertNumber(multiplier)} multiplier points increase exp gain by x{GameController.ConvertNumber(exp)}");
    }
}
