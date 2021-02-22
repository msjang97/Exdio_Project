using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public bool SetButton = false;  // 버튼 눌렸는지 아닌지 
    private GameObject SettingScreen; // 환경설정 화면

    private int Clicknum = 0;
  

    // Start is called before the first frame update
    void Start()
    {
        SettingScreen = GameObject.Find("SettingScreen");
        SettingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SetButton == true && Clicknum == 1)
        {
            SettingScreen.SetActive(true);
        }

        else if (SetButton == true && Clicknum > 1)
        {
            SettingScreen.SetActive(false);
            Clicknum = 0;
        }
    }

    public void Click()
    {
        SetButton = true;
        Clicknum++;
        Debug.Log(Clicknum);
    }
}
