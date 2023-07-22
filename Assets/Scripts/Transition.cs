using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public Image image;
    public bool startTransition = false;
    public bool endTransition = false;
    private float period = 0.0f;
    void Update()
    {
        if (startTransition)
        {
            var tempColor = image.color;
            while (tempColor.a < 255f)
            {
                if (period > 1 && tempColor.a < 255f)
                {
                    tempColor.a += 25.5f;
                    image.color = tempColor;
                    period = 0;
                }

                period += Time.deltaTime;
            }

            startTransition = false;
        }

        if (endTransition)
        {
            var tempColor = image.color;
            while (tempColor.a > 0)
            {
                if (period > 1 && tempColor.a > 0f)
                {
                    tempColor.a -= 25.5f;
                    image.color = tempColor;
                    period = 0;
                }

                period += Time.deltaTime;
            }

            endTransition = false;
        }
    }
}
