using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChooseCard : MonoBehaviour
{
    public GameController GameController;
    private int totalCard = 9;
    
    public Card card1;
    public Card card2;
    public Card card3;

    private float period = 0.0f;
    private bool moveCard = false;
    private int anchor = -700;

    public CardObject card1Object;
    public CardObject card2Object;
    public CardObject card3Object;

    public void Update()
    {
        if (moveCard)
        {
            if (period > 0.01 && anchor != -275)
            {
                anchor += 5;
                card1Object.GetComponent<RectTransform>().anchoredPosition = new Vector2(-540, anchor);
                card2Object.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, anchor);
                card3Object.GetComponent<RectTransform>().anchoredPosition = new Vector2(540, anchor);
                period = 0;
            }
            if (anchor == -275)
            {
                moveCard = false;
                anchor = -700;
            }
            period += Time.deltaTime;
        }
        
        
    }
    public void Align()
    {
        bool flag = true;
        bool card1Done = false;
        bool card2Done = false;
        bool card3Done = false;
        while (flag)
        {
            int cardId = Random.Range(1, 9);
            if (GameController.cardInventory.Count == totalCard)
            {
                flag = false;
            }
            if (GameController.cards.Exists(x => x.id == cardId))
            {
                if (card1Done != true)
                {
                    card1 = GameController.cards[cardId];
                    card1Object.card = card1;
                    card1Done = true;
                }
                else if (card2Done != true)
                {
                    card2 = GameController.cards[cardId];
                    card2Object.card = card2;
                    card2Done = true;
                }
                else if (card3Done != true)
                {
                    card3 = GameController.cards[cardId];
                    card3Object.card = card3;
                    card3Done = true;
                }
                else
                {
                    flag = false;
                }
            }
        }

        moveCard = true;




    }

    public void Choose(GameObject card, int id, string description)
    {
        GameController.cardInventory.Add(new Card(id, description));
        GameController.cards.RemoveAll(x => x.id == id);
    }
}
