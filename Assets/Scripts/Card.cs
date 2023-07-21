using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private int id;
    private string name;
    private string description;

    public Card(int id, string name, string description)
    {
        this.id = id;
        this.name = name;
        this.description = description;
    }
}
