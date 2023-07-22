using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card
{
    public int id;
    public string description;

    public Card(int id, string description)
    {
        this.id = id;
        this.description = description;
    }

}
