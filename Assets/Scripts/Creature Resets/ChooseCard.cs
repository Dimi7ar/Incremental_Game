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
    public GameObject frame1;
    public GameObject frame2;
    public GameObject background;
    private int totalCard = 0;
    public Card card1;
    public Card card2;
    public Card card3;

    private float period = 0.0f;
    private float periodAnimation = 0.0f;
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
                if (card1Object.gameObject.activeSelf)
                    card1Object.GetComponent<RectTransform>().anchoredPosition = new Vector2(-540, anchor);
                if (card2Object.gameObject.activeSelf)
                    card2Object.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, anchor);
                if (card3Object.gameObject.activeSelf)
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

        if (periodAnimation > 0.5)
        {
            if (frame1.activeSelf)
            {
                frame1.SetActive(false);
                frame2.SetActive(true);
            }
            else
            {
                frame1.SetActive(true);
                frame2.SetActive(false);
            }
            periodAnimation = 0;
        }
        periodAnimation += Time.deltaTime;

        if (background.GetComponent<RectTransform>().anchoredPosition.x != 480)
        {
            background.GetComponent<RectTransform>().anchoredPosition = new Vector2(background.GetComponent<RectTransform>().anchoredPosition.x + 0.2f, 0);
        }
        else
        {
            background.GetComponent<RectTransform>().anchoredPosition = new Vector2(-480, 0);
        }
    }
    public void Align()
    {
        if (totalCard == 0)
        {
            totalCard = GameController.cards.Count;
        }
        
        bool flag = true;
        bool card1Done = false;
        bool card2Done = false;
        bool card3Done = false;
        while (flag)
        {
            if (GameController.cards.Count == 0)
            {
                flag = false;
                GameController.CreatureReset();
            }
            int cardIndex = (int)Random.Range(0, GameController.cards.Count);
            int cardId = GameController.cards[cardIndex].id;
            
            if (GameController.cardInventory.Count <= totalCard - 3)
            {
                if (GameController.cards.Exists(x => x.id == cardId))
                {
                    if (card1Done != true)
                    {
                        card1 = GameController.cards.Find(x => x.id == cardId);
                        card1Object.card = card1;
                        card1Object.descriptionText.SetText(card1Object.card.description);
                        card1Done = true;
                    }
                    else if (card2Done != true && cardId != card1.id)
                    {
                        card2 = GameController.cards.Find(x => x.id == cardId);
                        card2Object.card = card2;
                        card2Object.descriptionText.SetText(card2Object.card.description);
                        card2Done = true;
                    }
                    else if (card3Done != true && cardId != card1.id && cardId != card2.id)
                    {
                        card3 = GameController.cards.Find(x => x.id == cardId);
                        card3Object.card = card3;
                        card3Object.descriptionText.SetText(card3Object.card.description);
                        card3Done = true;
                    }
                    else if (card1Done == true && card2Done == true && card3Done == true)
                    {
                        flag = false;
                    }
                }
            }
            else if (GameController.cardInventory.Count <= totalCard - 2)
            {
                if (GameController.cards.Exists(x => x.id == cardId))
                {
                    if (card1Done != true)
                    {
                        card1 = GameController.cards.Find(x => x.id == cardId);
                        card1Object.card = card1;
                        card1Object.descriptionText.SetText(card1Object.card.description);
                        card1Done = true;
                    }
                    else if (card2Done != true && cardId != card1.id)
                    {
                        card2 = GameController.cards.Find(x => x.id == cardId);
                        card2Object.card = card2;
                        card2Object.descriptionText.SetText(card2Object.card.description);
                        card2Done = true;
                    }
                    else if (card1Done == true && card2Done == true)
                    {
                        flag = false;
                    }

                    card3Object.gameObject.SetActive(false);
                }
            }
            else if (GameController.cardInventory.Count <= totalCard - 1)
            {
                if (GameController.cards.Exists(x => x.id == cardId))
                {
                    if (card1Done != true)
                    {
                        card1 = GameController.cards.Find(x => x.id == cardId);
                        card1Object.card = card1;
                        card1Object.descriptionText.SetText(card1Object.card.description);
                        card1Done = true;
                    }
                    else
                    {
                        flag = false;
                    }

                    card2Object.gameObject.SetActive(false);
                    card3Object.gameObject.SetActive(false);
                }
            }
            else
            {
                flag = false;
            }
        }
        moveCard = true;
    }

    public void Choose1()
    {
        GameController.cards.RemoveAt(GameController.cards.FindIndex(x => x.id == card1.id));
        GameController.cardInventory.Add(card1);
        chosenCard = 1;
        card1Button.interactable = false;
        CardEffect(card1Object.card.id);
        hideCard = true;
        GameController.data.cards = GameController.cards;
        GameController.data.card_Inventory = GameController.cardInventory;
    }
    public void Choose2()
    {
        GameController.cards.RemoveAt(GameController.cards.FindIndex(x => x.id == card2.id));
        GameController.cardInventory.Add(card2);
        chosenCard = 2;
        card2Button.interactable = false;
        CardEffect(card2Object.card.id);
        hideCard = true;
        GameController.data.cards = GameController.cards;
        GameController.data.card_Inventory = GameController.cardInventory;
    }
    public void Choose3()
    {
        GameController.cards.RemoveAt(GameController.cards.FindIndex(x => x.id == card3.id));
        GameController.cardInventory.Add(card3);
        chosenCard = 3;
        card3Button.interactable = false;
        CardEffect(card3Object.card.id);
        hideCard = true;
        GameController.data.cards = GameController.cards;
        GameController.data.card_Inventory = GameController.cardInventory;
    }

    public void CardEffect(int id)
    {
        switch (id)
        {
            case 1:
                GameController.multiplierMultiplier *= 5;
                GameController.data.card_Multiplier_Multiplier = GameController.multiplierMultiplier;
                break;
            case 2:
                GameController.rebirthMultiplier *= 4;
                GameController.data.card_Rebirth_Multiplier = GameController.rebirthMultiplier;
                break;
            case 3:
                GameController.prestigeMultiplier *= 3;
                GameController.data.card_Prestige_Multiplier = GameController.prestigeMultiplier;
                break;
            case 4:
                GameController.expMultiplier *= 5;
                GameController.data.card_Exp_Multiplier = GameController.expMultiplier;
                break;
            case 5:
                GameController.expMultiplier *= 2;
                GameController.multiplierMultiplier *= 2;
                GameController.data.card_Exp_Multiplier = GameController.expMultiplier;
                GameController.data.card_Multiplier_Multiplier = GameController.multiplierMultiplier;
                break;
            case 6:
                GameController.expMultiplier *= 2;
                GameController.rebirthMultiplier *= 2;
                GameController.data.card_Exp_Multiplier = GameController.expMultiplier;
                GameController.data.card_Rebirth_Multiplier = GameController.rebirthMultiplier;
                break;
            case 7:
                GameController.expMultiplier *= 2;
                GameController.prestigeMultiplier *= 2;
                GameController.data.card_Exp_Multiplier = GameController.expMultiplier;
                GameController.data.card_Prestige_Multiplier = GameController.prestigeMultiplier;
                break;
            case 8:
                GameController.tickspeedMultiplier *= 2;
                GameController.data.card_Tickspeed_Multiplier = GameController.tickspeedMultiplier;
                break;
            case 9:
                GameController.expRequirementDecrease -= 0.25;
                GameController.data.card_Exp_Requirement_Decrease = GameController.expRequirementDecrease;
                break;
        }
    }

    public void ResetCards()
    {
        card1Object.gameObject.SetActive(true);
        card2Object.gameObject.SetActive(true);
        card3Object.gameObject.SetActive(true);
        
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
