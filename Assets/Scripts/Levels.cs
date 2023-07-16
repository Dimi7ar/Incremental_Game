using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public GameController GameController;
    public double exp;
    public double level;
    public double levelRequierment;
    public float fillNumber;
    public Image fill;
    public TMP_Text text;
    public TMP_Text levelText;

    public Levels()
    {
        exp = 0;
        level = 0;
        levelRequierment = 10;
        fillNumber = 0;
    }

    public void LevelUp()
    {
        level++;
        exp = 0;
        levelRequierment += levelRequierment * 0.2;
    }
}
