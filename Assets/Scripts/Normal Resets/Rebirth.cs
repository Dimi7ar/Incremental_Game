using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Rebirth : MonoBehaviour
{
    public GameController GameController;
    public double rebirth;
    public double rebirthGain = 1;
    public double multiplierMultiplier = 1;
    public Button button;
    public TMP_Text rebirthDescription;
    public TMP_Text rebirthButtonText;
    public double maxLevel;
    
    private double rebirthSoftcap = 1.5;
    private double hardcap = 25;
    private double softcap = 0;

    public Rebirth()
    {
        
    }
    public void Update()
    {
            if (GameController.data.level >= 50)
            {
                button.interactable = true;
                if (GameController.data.level > maxLevel)
                {
                    if (GameController.data.level <= 55)
                    {
                        softcap += hardcap;
                        rebirthSoftcap -= rebirthSoftcap / softcap;
                    }
                    else if (GameController.data.level <= 60000)
                    {
                        hardcap = 40;
                        softcap += hardcap;
                        rebirthSoftcap -= rebirthSoftcap / softcap;
                    }
                    else
                    {   
                        hardcap = 100;
                        softcap += hardcap;
                        rebirthSoftcap -= rebirthSoftcap / softcap;
                    }
                    rebirthGain *= rebirthSoftcap;
                    maxLevel = GameController.data.level;
                }
                rebirthButtonText.SetText(
                    $"You will receive {GameController.ConvertNumber(rebirthGain, 0)} rebirth on reset");
                GameController.data.rebirthGain = rebirthGain;
            }
            else
            {
                button.interactable = false;
                rebirthButtonText.SetText("Reach level 50 to unlock");
            }
    }

    public void BuyRebirth()
    {
        ResetRebirth();
        GameController.multiplier.ResetMultiplier();
        GameController.multiplier.multiplier = 0;
        GameController.multiplier.expMultiplier = 1;
        GameController.prestige.ResetPrestige();
        GameController.prestige.prestigeGain = 1;
        
        multiplierMultiplier = 1;
        rebirth += rebirthGain;
        
        multiplierMultiplier = math.pow(rebirth, 0.66);
        GameController.multiplier.multiplierGain = multiplierMultiplier;
        
        if (GameController.prestige.prestige >= 1)
            rebirthGain = 1 * GameController.prestige.rebirthMultiplier * GameController.rebirthMultiplier;
        else
            rebirthGain = 1;
        
        GameController.multiplier.multiplierDescription.SetText($"Your {GameController.ConvertNumber(GameController.multiplier.multiplier, 0)} multiplier points increase exp gain by x{GameController.ConvertNumber(GameController.multiplier.expMultiplier, 2)}");
        rebirthDescription.SetText($"Your {GameController.ConvertNumber(rebirth, 0)} rebirth points increase multiplier gain by x{GameController.ConvertNumber(multiplierMultiplier, 2)}");
    }

    public void ResetRebirth()
    {
        GameController.data.level = 0;
        GameController.data.exp = 0;
        GameController.levels.levelRequierment = 2;
        rebirthSoftcap = 1.5;
        maxLevel = 0;
        hardcap = 25;
        softcap = 0;
    }
}
