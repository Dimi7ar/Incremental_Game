using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Data data = new Data();

    public TMP_Text woodText;

    public void Update()
    {
        woodText.text = $"Wood: <color=#854121>{data.wood}</color>";
    }
}
