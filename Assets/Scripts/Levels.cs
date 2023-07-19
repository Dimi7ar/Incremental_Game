using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public GameController GameController;
    public double levelRequierment = 2;
    public float fillNumber = 0;
    public Image fill;
    public TMP_Text text;
    public TMP_Text levelText;

    public Levels()
    {
        
    }

    public void LevelUp()
    {
        if (GameController.data.level < 25)
        {
            levelRequierment += levelRequierment * 0.2;
        }
        else
        {
            levelRequierment += levelRequierment * 0.25;
        }
        GameController.data.level++;
        GameController.data.exp = 0;
    }
}
