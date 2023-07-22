using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;
using Unity.UI;
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
    private int chosenCard = 0;
    private bool hideCard = false;

    public CardObject card1Object;
    public CardObject card2Object;
    public CardObject card3Object;

    public Button card1Button;
    public Button card2Button;
    public Button card3Button;

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

        if (hideCard)
        {
            if (chosenCard == 1)
            {
                card2Object.GetComponent<RectTransform>().localScale = new Vector2(0, 0);
                card3Object.GetComponent<RectTransform>().localScale = new Vector2(0, 0);
                hideCard = false;
            }
            else if (chosenCard == 2)
            {
                card1Object.GetComponent<RectTransform>().localScale = new Vector2(0, 0);
                card3Object.GetComponent<RectTransform>().localScale = new Vector2(0, 0);
                hideCard = false;
            }
            else if (chosenCard == 3)
            {
                card1Object.GetComponent<RectTransform>().localScale = new Vector2(0, 0);
                card2Object.GetComponent<RectTransform>().localScale = new Vector2(0, 0);
                hideCard = false;
            }
            GameController.CreatureReset();
        }
        
        
    }
    public void Align()
    {
        //FIX THIS SHIT
        //
        //
        //
        //
        //
        //
        //
        //PLEASE
        bool flag = true;
        bool card1Done = false;
        bool card2Done = false;
        bool card3Done = false;
        while (flag)
        {
            int cardId = (int)Random.Range(1, 9);
            if (GameController.cardInventory.Count == totalCard)
            {
                flag = false;
            }
            if (GameController.cards.Exists(x => x.id == cardId) && cardId >= 1 && cardId <= 9)
            {
                if (card1Done != true && GameController.cards.Count >= 3)
                {
                    card1 = GameController.cards.Find(x => x.id == cardId);
                    GameController.cards.RemoveAt(GameController.cards.FindIndex(x => x.id == cardId));
                    card1Object.card = card1;
                    card1Object.descriptionText.SetText(card1Object.card.description);
                    card1Done = true;
                }
                else if (card2Done != true && GameController.cards.Count >= 2)
                {
                    card2 = GameController.cards.Find(x => x.id == cardId);
                    GameController.cards.RemoveAt(GameController.cards.FindIndex(x => x.id == cardId));
                    card2Object.card = card2;
                    card2Object.descriptionText.SetText(card2Object.card.description);
                    card2Done = true;
                }
                else if (card3Done != true && GameController.cards.Count >= 1)
                {
                    card3 = GameController.cards.Find(x => x.id == cardId);
                    GameController.cards.RemoveAt(GameController.cards.FindIndex(x => x.id == cardId));
                    card3Object.card = card3;
                    card3Object.descriptionText.SetText(card3Object.card.description);
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

    public void Choose1()
    {
        GameController.cardInventory.Add(card1Object.card);
        chosenCard = 1;
        card1Button.interactable = false;
        CardEffect(card1Object.card.id);
        GameController.cards.Add(card2Object.card);
        GameController.cards.Add(card3Object.card);
        hideCard = true;
    }
    public void Choose2()
    {
        GameController.cardInventory.Add(card2Object.card);
        chosenCard = 2;
        card2Button.interactable = false;
        CardEffect(card2Object.card.id);
        GameController.cards.Add(card1Object.card);
        GameController.cards.Add(card3Object.card);
        hideCard = true;
    }
    public void Choose3()
    {
        GameController.cardInventory.Add(card3Object.card);
        chosenCard = 3;
        card3Button.interactable = false;
        CardEffect(card3Object.card.id);
        GameController.cards.Add(card1Object.card);
        GameController.cards.Add(card2Object.card);
        hideCard = true;
    }

    public void CardEffect(int id)
    {
        switch (id)
        {
            case 1:
                GameController.multiplierMultiplier *= 5;
                break;
            case 2:
                GameController.rebirthMultiplier *= 4;
                break;
            case 3:
                GameController.prestigeMultiplier *= 3;
                break;
            case 4:
                GameController.expMultiplier *= 5;
                break;
            case 5:
                GameController.expMultiplier *= 2;
                GameController.multiplierMultiplier *= 2;
                break;
            case 6:
                GameController.expMultiplier *= 2;
                GameController.rebirthMultiplier *= 2;
                break;
            case 7:
                GameController.expMultiplier *= 2;
                GameController.prestigeMultiplier *= 2;
                break;
            case 8:
                GameController.tickspeedMultiplier *= 2;
                break;
            case 9:
                GameController.expRequirementDecrease -= 0.25;
                break;
        }
    }

    public void ResetCards()
    {
        card1Object.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        card2Object.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        card3Object.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        card1Button.interactable = true;
        card2Button.interactable = true;
        card3Button.interactable = true;
        
        card1Object.GetComponent<RectTransform>().anchoredPosition = new Vector2(-540, anchor);
        card2Object.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, anchor);
        card3Object.GetComponent<RectTransform>().anchoredPosition = new Vector2(540, anchor);
    }
}
