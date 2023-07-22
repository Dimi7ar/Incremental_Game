using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardObject : MonoBehaviour, IPointerClickHandler
{
    public ChooseCard choice;
    public Card card;
    public TMP_Text descriptionText;


    public void OnPointerClick(PointerEventData eventData)
    {
        choice.Choose(gameObject, this.card.id, this.card.description);
    }
}
