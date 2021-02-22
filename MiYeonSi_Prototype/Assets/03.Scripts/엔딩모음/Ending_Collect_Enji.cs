using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Button))]
public class Ending_Collect_Enji : MonoBehaviour
{
    // Start is called before the first frame update
    public Button Enji_Button; //엔딩모음에서 은지 얼굴 버튼
    public Sprite Changing_Enji; //바뀔 은지 엔딩 이미지
  
    void Start()
    {
        Enji_Button = GetComponent<Button>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (LovePoint.instance.Enji_end == true) //은지 엔딩을 봤다면
        {
            Enji_Button.image.sprite = Changing_Enji;
        }

   
    }

    public void GotoEnding()
    {
        if (LovePoint.instance.Enji_end == true)
        {
            SceneManager.LoadScene("Enji_End");
        }
    }
}
