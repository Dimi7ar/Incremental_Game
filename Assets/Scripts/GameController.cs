using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BreakInfinity;
using TMPro;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;

public class GameController : MonoBehaviour
{
    public Data data = new Data();
    public Levels levels;
    public Multiplier multiplier;
    public Rebirth rebirth;
    public Prestige prestige;
    public Creature creature;

    public GameObject mainGame;
    public GameObject creatureScreen;

    public List<Card> cards = new List<Card>()
    {
        new Card(1, "Increase your multiplier gain by x5"),
        new Card(2, "Increase your rebirth gain by x4"),
        new Card(3, "Increase your prestige gain by x3"),
        new Card(4, "Increase your exp gain by x5"),
        new Card(5, "Increase your exp gain by x2 and multiplier gain by x2"),
        new Card(6, "Increase your exp gain by x2 and rebirth gain by x2"),
        new Card(7, "Increase your exp gain by x2 and prestige gain by x2"),
        new Card(8, "Divide tickspeed by 2"),
        new Card(9, "Decrease level requirement by 25%")
    };

    public List<int> cardInventory = new List<int>();
    public ChooseCard choice;
    public TMP_Text cardEffect;

    private BigDouble expPerTick = 1;
    public BigDouble expMultiplier = 1;
    public BigDouble multiplierMultiplier = 1;
    public BigDouble rebirthMultiplier = 1;
    public BigDouble prestigeMultiplier = 1;
    public BigDouble tickspeedMultiplier = 1;
    public BigDouble expRequirementDecrease = 1;

    public CanvasGroup mainGameCG;
    public CanvasGroup creatureScreenCG;
    private bool fadeIntoCreatureScreen = false;
    private bool fadeIntoMainGame = false;
    
    
    private float period = 0.0f;
    private float SaveTime = 0.0f;
    public TMP_Text lastSave;

    public GameController()
    {
        
    }

    public void Start()
    {
        data = DataSave.SaveExists() ? DataSave.LoadData<Data>() : new Data();
        LoadController();
    }
    public void Update()
    {
        Fadings();

        if (period > (0.1 / tickspeedMultiplier))
        {
            ExpPerSecond();
            period = 0;
        }
        GameLoop();
        period += Time.deltaTime;

        SaveTime += Time.deltaTime;
        lastSave.SetText($"Last save: {SaveTime:F0}s ago");
        if (SaveTime >= 15)
        {
            SaveTime = 0;
            DataSave.SaveData(data);
        }
    }

    private void LevelUpAction()
    {
        levels.text.SetText($"{Notation(data.exp, "F0")}/{Notation(levels.levelRequierment, "F0")}");
        levels.fillNumber = (float)(data.exp / levels.levelRequierment).ToDouble();
        levels.fill.fillAmount = levels.fillNumber;
        if (data.exp >= levels.levelRequierment)
        {
            levels.LevelUp();
        }
        data.levelRequirement = levels.levelRequierment;
        levels.levelText.SetText($"Level: <color=green>{Notation(data.level, "F0")}</color>");
    }
    private void ExpPerSecond()
    {
        data.expPerTick = expPerTick * multiplier.expMultiplier * prestige.expMultiplier * expMultiplier;
        data.exp += data.expPerTick;
    }
    public void BuyMultiplier()
    {
        multiplier.BuyMultiplier();
        data.multiplier = multiplier.multiplier;
    }
    
    public void BuyRebirth()
    {
        rebirth.BuyRebirth();
        data.rebirth = rebirth.rebirth;
    }
    
    public void BuyPrestige()
    {
        prestige.BuyPrestige();
        data.prestige = prestige.prestige;
    }

    public void CreatureReset()
    {
        if (choice.cardsAvailable > 0)
        {
            if (creatureScreenCG.alpha == 0)
            {
                ResetContent();
                fadeIntoCreatureScreen = true;
                choice.Align();
                data.maxLevel = 0;
            }
            else
            {
                fadeIntoMainGame = true;
                cardEffect.SetText(CardText());
                creature.fillNumber = 0;
                creature.button.SetActive(false);
                creature.gameObject.SetActive(false);
                creature.count++;
                data.creature_Count++;
            }
        }
        else if (choice.cardsAvailable == 0)
        {
            fadeIntoMainGame = true;
            cardEffect.SetText(CardText());
            creature.fillNumber = 0;
            creature.button.SetActive(false);
            creature.gameObject.SetActive(false);
            creature.count++;
            data.creature_Count++;
        }
    }
    private void GameLoop()
    {
        LevelUpAction();
        CheckAvailability();
    }

    private void CheckAvailability()
    {
        if (data.maxLevel >= 40 && rebirth.gameObject.activeSelf == false)
        {
            rebirth.gameObject.SetActive(true);
        }
        else if (data.maxLevel <= 40 && rebirth.gameObject.activeSelf)
        {
            rebirth.gameObject.SetActive(false);
        }
        if (data.maxLevel >= 90 && prestige.gameObject.activeSelf == false)
        {
            prestige.gameObject.SetActive(true);
        }
        else if (data.maxLevel <= 90 && prestige.gameObject.activeSelf)
        {
            prestige.gameObject.SetActive(false);
        }
        if (data.maxLevel >= 100 && creature.gameObject.activeSelf == false)
        {
            creature.gameObject.SetActive(true);
        }
        else if (data.maxLevel <= 100 && creature.gameObject.activeSelf)
        {
            creature.gameObject.SetActive(false);
        }
    }

    private void ResetContent()
    {
        multiplier.ResetMultiplier();
        multiplier.multiplier = 0;
        multiplier.expMultiplier = 1;
        multiplier.multiplierGain = 1;
        data.multiplier = 0;
        data.multiplier_Exp_Multiplier = 1;
        data.multiplier_Gain = 1;
        multiplier.multiplierDescription.SetText($"Your {Notation(multiplier.multiplier, "F2")} multiplier points increase exp gain by x{Notation(multiplier.expMultiplier, "F2")}");
        
        rebirth.ResetRebirth();
        rebirth.rebirth = 0;
        rebirth.multiplierMultiplier = 1;
        rebirth.rebirthGain = 1;
        data.rebirth = 0;
        data.rebirth_Multiplier_Multiplier = 1;
        data.rebirth_Gain = 1;
        rebirth.rebirthDescription.SetText($"Your {Notation(rebirth.rebirth, "F2")} rebirth points increase multiplier gain by x{Notation(rebirth.multiplierMultiplier, "F2")}");
        
        prestige.ResetPrestige();
        prestige.prestige = 0;
        prestige.expMultiplier = 1; 
        prestige.rebirthMultiplier = 1;
        prestige.prestigeGain = 1;
        data.prestige = 0;
        data.prestige_Exp_Multiplier = 1;
        data.prestige_Rebirth_Multiplier = 1;
        data.prestige_Gain = 1;
        prestige.prestigeDescription.SetText($"Your {Notation(prestige.prestige, "F2")} prestige points increase rebirth gain by x{Notation(prestige.rebirthMultiplier, "F2")} and exp gain by x{Notation(prestige.expMultiplier, "F2")}");
    }

    private void Fadings()
    {
        if (fadeIntoMainGame)
        {
            if (creatureScreenCG.alpha > 0)
            {
                creatureScreenCG.alpha -= Time.deltaTime;
            }
            else
            {
                creatureScreenCG.alpha = 0;
                mainGameCG.gameObject.SetActive(true);
                creatureScreenCG.gameObject.SetActive(false);
                mainGameCG.alpha += Time.deltaTime;
            }

            if (mainGameCG.alpha >= 1)
            {
                choice.ResetCards();
                fadeIntoMainGame = false;
            }
        }
        
        if (fadeIntoCreatureScreen)
        {
            if (mainGameCG.alpha > 0)
            {
                mainGameCG.alpha -= Time.deltaTime;
            }
            else
            {
                mainGameCG.alpha = 0;
                creatureScreenCG.gameObject.SetActive(true);
                mainGameCG.gameObject.SetActive(false);
                creatureScreenCG.alpha += Time.deltaTime;
            }

            if (creatureScreenCG.alpha >= 1)
            {
                creatureScreenCG.alpha = 1;
                fadeIntoCreatureScreen = false;
            }
        }
    }

    private string CardText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Card effects: ");
        if (expMultiplier > 1)
        {
            sb.Append($"{expMultiplier}x exp, ");
        }
        if (multiplierMultiplier > 1)
        {
            sb.Append($"{multiplierMultiplier}x multiplier, ");
        }
        if (rebirthMultiplier > 1)
        {
            sb.Append($"{rebirthMultiplier}x rebirth, ");
        }
        if (prestigeMultiplier > 1)
        {
            sb.Append($"{prestigeMultiplier}x prestige, ");
        }
        if (tickspeedMultiplier > 1)
        {
            sb.Append($"TPS / {tickspeedMultiplier}, ");
        }
        if (expRequirementDecrease < 1)
        {
            sb.Append($"LevelReq * {expRequirementDecrease}, ");
        }
        sb.Remove(sb.Length - 2, 2);
        return sb.ToString().TrimEnd();
    }

    private void LoadController()
    {
        levels.levelRequierment = data.levelRequirement;
        cardInventory = data.card_Inventory;
        expMultiplier = data.card_Exp_Multiplier;
        multiplierMultiplier = data.card_Multiplier_Multiplier;
        rebirthMultiplier = data.card_Rebirth_Multiplier;
        prestigeMultiplier = data.card_Prestige_Multiplier;
        tickspeedMultiplier = data.card_Tickspeed_Multiplier;
        expRequirementDecrease = data.card_Exp_Requirement_Decrease;
        if (data.card_Inventory.Count > 0)
        {
            cardEffect.SetText(CardText());
        }
    }

    public void TestButtonClick()
    {
        data.level += 100;
    }

    public string Notation(BigDouble x, string y)
    {
        if (x > 1000)
        {
            BigDouble cap = 10;
            BigDouble exponent = x.AbsLog10();
            exponent = exponent.Floor();
            BigDouble mantissa = x / cap.Pow(exponent);
            return mantissa.ToString(format: "F2") + "e" + exponent;
        }
        return x.ToString(y);
    }
   
}
