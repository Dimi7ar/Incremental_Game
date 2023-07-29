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
    public int count = 1;

    public void Start()
    {
        
    }

    public void Update()
    {
        count = GameController.data.creature_Count;
        if (fillNumber < 1)
        {
            fillNumber = (float)((GameController.data.level - 100) / (100 * count));
            fill.fillAmount = fillNumber;
        }

        if (GameController.data.card_Inventory.Count == 9)
        {
            button.SetActive(false);
        }
        else if ((GameController.data.level - 100) / (100 * count) >= 1)
        {
            button.SetActive(true);
        }
    }
    
    
}
