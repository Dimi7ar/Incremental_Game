using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Prestige : MonoBehaviour
{
    public GameController GameController;
    public double prestige;
    public double prestigeGain = 1;
    public double rebirthMultiplier = 1;
    public double expMultiplier = 1;
    public Button button;
    public TMP_Text prestigeDescription;
    public TMP_Text prestigeButtonText;
    public double maxLevel;
    
    private double prestigeSoftcap = 1.5;
    private double hardcap = 25;
    private double softcap = 0;

    public Prestige()
    {
        expMultiplier = 1;
    }
    public void Update()
    {
            if (GameController.data.level >= 100)
            {
                button.interactable = true;
                if (GameController.data.level > maxLevel)
                {
                    if (GameController.data.level <= 105)
                    {
                        softcap += hardcap;
                        prestigeSoftcap -= prestigeSoftcap / softcap;
                    }
                    else if (GameController.data.level <= 100000)
                    {
                        hardcap = 40;
                        softcap += hardcap;
                        prestigeSoftcap -= prestigeSoftcap / softcap;
                    }
                    else
                    {   
                        hardcap = 100;
                        softcap += hardcap;
                        prestigeSoftcap -= prestigeSoftcap / softcap;
                    }
                    prestigeGain *= prestigeSoftcap;
                    maxLevel = GameController.data.level;
                }
                prestigeButtonText.SetText(
                    $"You will receive {GameController.ConvertNumber(prestigeGain, 0)} prestige on reset");
                GameController.data.prestigeGain = prestigeGain;
            }
            else
            {
                button.interactable = false;
                prestigeButtonText.SetText("Reach level 100 to unlock");
            }
    }

    public void BuyPrestige()
    {
        ResetPrestige();
        
        GameController.rebirth.ResetRebirth();
        GameController.rebirth.rebirth = 0;
        GameController.rebirth.multiplierMultiplier = 1;
        GameController.rebirth.rebirthGain = 1;

        GameController.multiplier.ResetMultiplier();
        GameController.multiplier.multiplier = 0;
        GameController.multiplier.expMultiplier = 1;
        GameController.multiplier.multiplierGain = 1;
        
        rebirthMultiplier = 1;
        expMultiplier = 1;

        prestige += prestigeGain;
        
        expMultiplier = math.pow(prestige, 0.66);
        rebirthMultiplier = math.pow(prestige, 0.44);
        
        GameController.rebirth.rebirthGain = rebirthMultiplier;
        
        prestigeGain = 1;
        
        GameController.multiplier.multiplierDescription.SetText($"Your {GameController.ConvertNumber(GameController.multiplier.multiplier, 0)} multiplier points increase exp gain by x{GameController.ConvertNumber(GameController.multiplier.expMultiplier, 2)}");
        GameController.rebirth.rebirthDescription.SetText($"Your {GameController.ConvertNumber(GameController.rebirth.rebirth, 0)} rebirth points increase multiplier gain by x{GameController.ConvertNumber(GameController.rebirth.multiplierMultiplier, 2)}");
        prestigeDescription.SetText($"Your {GameController.ConvertNumber(prestige, 0)} prestige points increase rebirth gain by x{GameController.ConvertNumber(rebirthMultiplier, 2)} and exp gain by x{GameController.ConvertNumber(expMultiplier, 2)}");
    }

    public void ResetPrestige()
    {
        GameController.data.level = 0;
        GameController.data.exp = 0;
        GameController.levels.levelRequierment = 2;
        prestigeSoftcap = 1.5;
        maxLevel = 0;
        hardcap = 25;
        softcap = 0;
    }
}
