using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Data data = new Data();
    public double cost;
    public List<GameObject> links;
    public UpgradeScript script;
    public GameObject obj;

    public void UpgradeClick()
    {
        if (data.wood >= cost)
        {
            script.UpgradeFunction();   
            foreach (var link in links)
            {
                link.SetActive(true);
            }
        }
    }

    void Start()
    {
        obj.GetComponent<Button>().onClick.AddListener(UpgradeClick);
    }
}
