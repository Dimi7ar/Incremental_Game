using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Data data = new Data();
    public Levels levels;
    public Multiplier multiplier;
    public Rebirth rebirth;
    
    
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
        // if (dataSavePeriod > 30 )
        // {
        //     DataSave.Save(data);
        //     dataSavePeriod = 0;
        // }
        period += Time.deltaTime;
        // dataSavePeriod += Time.deltaTime;
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
        data.exp += data.expPerTick;
    }
    public void BuyMultiplier()
    {
        multiplier.BuyMultiplier();
        data.expPerTick = multiplier.expMultiplier;
        data.multiplier = multiplier.multiplier;
    }
    public void BuyRebirth()
    {
        rebirth.BuyRebirth();
        data.expPerTick = 1;
        data.rebirth = rebirth.rebirth;
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
    }
}
