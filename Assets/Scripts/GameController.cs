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
    
    private float period = 0.0f;

    public GameController()
    {
        
    }

    public void Update()
    {
        if (period > 0)
        {
            ExpPerSecond();
            period = 0;
        }
        GameLoop();
        period += Time.deltaTime;
    }

    private void LevelUpAction()
    {
        levels.text.SetText($"{ConvertNumber(data.exp)}/{ConvertNumber(levels.levelRequierment)}");
        levels.exp = data.exp;
        levels.level = data.level;
        levels.fillNumber = (float)(data.exp / levels.levelRequierment);
        levels.fill.fillAmount = levels.fillNumber;
        if (levels.exp >= levels.levelRequierment)
        {
            levels.LevelUp();
            data.exp = 0;
            data.level = levels.level;
            levels.levelText.SetText($"Level: <color=green>{ConvertNumber(data.level)}</color>");
        }
    }
    private void ExpPerSecond()
    {
        data.exp += data.expPerSecond;
    }
    public void BuyMultiplier()
    {
        multiplier.BuyMultiplier();
        data.expPerSecond = multiplier.exp;
        data.multiplier = multiplier.multiplier;
    }
    private void GameLoop()
    {
        LevelUpAction();
    }

    public string ConvertNumber(double number)
    {
        if (number >= 1000)
        {
            return string.Format("{0:#.##E+0}", number);
        }

        return $"{number:F0}";
    }
}
