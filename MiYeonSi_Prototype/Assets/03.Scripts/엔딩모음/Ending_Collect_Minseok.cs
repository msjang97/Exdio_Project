using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class Ending_Collect_Minseok : MonoBehaviour
{
    public Button Minseok_Button; //엔딩모음에서 민석 얼굴 버튼
    public Sprite Changing_Minseok; //바뀔 민석 엔딩 이미지

    void Start()
    {
        Minseok_Button = GetComponent<Button>();
    }

    void Update()
    {
        if (LovePoint.instance.Minseok_end == true) //민석 엔딩을 봤다면
        {
            Minseok_Button.image.sprite = Changing_Minseok;
        }
    }

    public void GotoEnding()
    {
        if (LovePoint.instance.Minseok_end == true)
        {
            SceneManager.LoadScene("Minseok_End");
        }
    }
}
