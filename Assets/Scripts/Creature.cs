using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class Creature : MonoBehaviour
{
    public GameController GameController;
    public float fillNumber = 0;
    public Image fill;
    public GameObject button;
    private int count = 1;
    private List<Card> cards;

    public void Update()
    {
        if (fillNumber < 1)
        {
            fillNumber = (float)((GameController.data.level - (100 * count)) / 100);
            fill.fillAmount = fillNumber;
        }
        if (fillNumber >= 1)
        {
            button.SetActive(true);
        }
    }
    
    
}
