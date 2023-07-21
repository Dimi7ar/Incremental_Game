using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public Image image;

    void Update()
    {
        
    }

    public void StartTransition()
    {
        var tempColor = image.color;
        for (float i = 0; i < 255; i+=(float)25.5)
        {
            tempColor.a = i;
            image.color = tempColor;
        }
    }

    public void EndTransition()
    {
        var tempColor = image.color;
        for (float i = 255; i > 0; i-=(float)25.5)
        {
            tempColor.a = i;
            image.color = tempColor;
        }
    }
}
