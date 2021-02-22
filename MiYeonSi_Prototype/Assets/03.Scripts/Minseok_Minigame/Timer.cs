using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timerText;
    private float time;
    private int currentTime;
    public static bool stop = false;

    // Use this for initialization
    void Start()
    {
        time = 30;
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
            time -= Time.deltaTime;
            currentTime = (int)time;
            timerText.text = currentTime.ToString();
    }
}
