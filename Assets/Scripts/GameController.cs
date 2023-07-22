using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        new Card(9, "Decrease exp requirement by 50%")
    };

    public List<Card> cardInventory = new List<Card>();
    public ChooseCard choice;


    private const double expPerTick = 1;

    public CanvasGroup mainGameCG;
    public CanvasGroup creatureScreenCG;
    private bool fadeInMainGame = false;
    private bool fadeOutMainGame = false;
    private bool fadeInCreatureScreen = false;
    private bool fadeOutCreatureScreen = false;
    
    
    private float period = 0.0f;
    // private float dataSavePeriod = 0.0f;

    public GameController()
    {
        
    }

    public void Start()
    {
        // DataSave.Load(data);
    }
    public void Update()
    {
        if (period > 0.2)
        {
            ExpPerSecond();
            period = 0;
        }
        GameLoop();
        period += Time.deltaTime;
        Fadings();

    }

    private void LevelUpAction()
    {
        levels.text.SetText($"{ConvertNumber(data.exp, 0)}/{ConvertNumber(levels.levelRequierment, 0)}");
        levels.fillNumber = (float)(data.exp / levels.levelRequierment);
        levels.fill.fillAmount = levels.fillNumber;
        if (data.exp >= levels.levelRequierment)
        {
            levels.LevelUp();
            levels.levelText.SetText($"Level: <color=green>{ConvertNumber(data.level, 0)}</color>");
        }
    }
    private void ExpPerSecond()
    {
        data.expPerTick = expPerTick * multiplier.expMultiplier * prestige.expMultiplier;
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
        ResetContent();
        fadeOutMainGame = true;
        mainGame.SetActive(false);
        creatureScreen.SetActive(true);
        fadeInCreatureScreen = true;
        choice.Align();
        

    }
    private void GameLoop()
    {
        LevelUpAction();
        CheckAvailability();
    }

    public string ConvertNumber(double number, int _float)
    {
        if (number >= 1000)
        {
            return string.Format("{0:#.##e0}", number);
        }

        if (_float == 2)
        {
            return $"{number:F2}";
        }

        return $"{number:F0}";
    }

    private void CheckAvailability()
    {
        if (data.level >= 40 && rebirth.gameObject.activeSelf == false)
        {
            rebirth.gameObject.SetActive(true);
        }
        if (data.level >= 90 && prestige.gameObject.activeSelf == false)
        {
            prestige.gameObject.SetActive(true);
        }
        if (data.level >= 100 && creature.gameObject.activeSelf == false)
        {
            creature.gameObject.SetActive(true);
        }
    }

    private void ResetContent()
    {
        prestige.ResetPrestige();
        prestige.prestige = 0;
        prestige.expMultiplier = 1;
        prestige.rebirthMultiplier = 1;
        prestige.prestigeGain = 1;
        
        rebirth.ResetRebirth();
        rebirth.rebirth = 0;
        rebirth.multiplierMultiplier = 1;
        rebirth.rebirthGain = 1;

        multiplier.ResetMultiplier();
        multiplier.multiplier = 0;
        multiplier.expMultiplier = 1;
        multiplier.multiplierGain = 1;
    }

    private void Fadings()
    {
        if (fadeInMainGame && mainGameCG.alpha < 1f)
        {
            mainGameCG.alpha += Time.deltaTime;
            if (mainGameCG.alpha >= 1)
            {
                fadeInMainGame = false;
            }
        }
        if (fadeOutMainGame && mainGameCG.alpha > 0f)
        {
            mainGameCG.alpha -= Time.deltaTime;
            if (mainGameCG.alpha <= 0)
            {
                fadeOutMainGame = false;
            }
        }
        if (fadeInCreatureScreen && creatureScreenCG.alpha < 1f)
        {
            creatureScreenCG.alpha += Time.deltaTime;
            if (mainGameCG.alpha >= 1)
            {
                fadeInCreatureScreen = false;
            }
        }
        if (fadeOutCreatureScreen && creatureScreenCG.alpha > 0f)
        {
            creatureScreenCG.alpha += Time.deltaTime;
            if (creatureScreenCG.alpha <= 0)
            {
                fadeOutCreatureScreen = false;
            }
        }
    }
}
