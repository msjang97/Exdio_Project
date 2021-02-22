using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class Ending_Collect_Hagyoung : MonoBehaviour
{
    public Button Hagyoung_Button; //엔딩모음에서 하경 얼굴 버튼
    public Sprite Changing_Hagyuong; //바뀔 하경 엔딩 이미지

    void Start()
    {
        Hagyoung_Button = GetComponent<Button>();
    }

    void Update()
    {
        if (LovePoint.instance.Hagyong_end == true) //하경 엔딩을 봤다면
        {
            Hagyoung_Button.image.sprite = Changing_Hagyuong;
        }
    }

    public void GotoEnding()
    {
        if (LovePoint.instance.Hagyong_end == true)
        {
            SceneManager.LoadScene("Hagyoung_End");
        }
    }
}