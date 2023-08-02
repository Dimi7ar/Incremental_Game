using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cosmos_Reset;
using UnityEngine;
using UnityEngine.UI;

public class Cosmos : MonoBehaviour
{
    public GameController GameController;
    public List<CosmicBonus> bonuses = new List<CosmicBonus>()
    {
        new CosmicBonus(1,1, "Gain 1% of Multiplier per tick, that you would get on reset.", false),
        new CosmicBonus(2,2, "Cosmic Power gain is increase x2 for each Cosmos after 1.", false),
        new CosmicBonus(3,3, "Add 3 new cards for the void to offer.", false),
        new CosmicBonus(4,4, "Gain 1% of Rebirth per tick, that you would get on reset.", false),
        new CosmicBonus(5,5, "", false),
        new CosmicBonus(6,6, "", false),
        new CosmicBonus(7,7, "Gain 1% of Prestige per tick, that you would get on reset.", false),
        new CosmicBonus(8,8, "", false),
        new CosmicBonus(9,9, "", false),
        new CosmicBonus(10,10, "Multiply every gain by 10, except Cosmic Power.", false)
    };

    public List<CosmicBonusObject> bonusObjects;
    public int cosmosCount;
    public int cosmicPower;

    public void Start()
    {
        foreach (var bonusObject in bonusObjects)
        {
            bonusObject.description.SetText(bonuses[bonusObject.id - 1].description);
        }
    }

    public void Update()
    {
        foreach (var bonus in bonuses.Where(x => x.obtained == false))
        {
            if ((bonus.requirement <= cosmosCount))
            {
                bonus.obtained = true;
                Color newColor = new Color(0, 157, 161);
                bonusObjects[bonus.id - 1].background.color = newColor;
                if (!GameController.data.cosmic_Bonuses_Obtained.Contains(bonus.id))
                     GameController.data.cosmic_Bonuses_Obtained.Add(bonus.id);
            }
        }
        
    }
}

