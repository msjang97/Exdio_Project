using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Button))]
public class Ending_Collect_Solo : MonoBehaviour
{
    public Button Solo_Button; //엔딩모음에서 솔로 얼굴 버튼
    public Sprite Changing_Soio; //바뀔 솔로 엔딩 이미지

    void Start()
    {
        Solo_Button = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Update()
    {
        if (LovePoint.instance.Solo_end == true) //솔로 엔딩을 봤다면
        {
            Solo_Button.image.sprite = Changing_Soio;
        }
    }

    public void GotoEnding()
    {
        if (LovePoint.instance.Solo_end == true)
        {
            SceneManager.LoadScene("Solo_End");
        }
    }
}
