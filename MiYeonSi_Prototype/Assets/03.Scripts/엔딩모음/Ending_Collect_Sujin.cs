using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class Ending_Collect_Sujin : MonoBehaviour
{
    public Button Sujin_Button; //엔딩모음에서 수진 얼굴 버튼
    public Sprite Changing_Sujin; //바뀔 수진 엔딩 이미지

    void Start()
    {
        Sujin_Button = GetComponent<Button>();
    }

    void Update()
    {
        if (LovePoint.instance.Sujin_end == true) //수진 엔딩을 봤다면
        {
            Sujin_Button.image.sprite = Changing_Sujin;
        }
    }

    public void GotoEnding()
    {
        if (LovePoint.instance.Sujin_end == true)
        {
            SceneManager.LoadScene("Sujin_End");
        }
    }
}
