using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer_SuJin : MonoBehaviour
{
    public Bar bar;
    public TextMeshProUGUI TimeCount;
    public float TimeCost;

    void Update()
    {
        if (bar.P_isStoped == false)
            CountTime();        
    }

    private void CountTime()
    {
        TimeCost -= Time.deltaTime;
        TimeCount.text = "" + (int)TimeCost;
    }
}
