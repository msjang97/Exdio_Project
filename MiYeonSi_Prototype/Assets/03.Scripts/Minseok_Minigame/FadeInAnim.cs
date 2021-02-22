using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAnim : MonoBehaviour
{
    public GameObject[] button = new GameObject[4];  // 선택지
    public GameObject[] buttonText = new GameObject[4]; //선택지에 있는 텍스트

    public void Start()
    {
        FadeOut(0.5f); //0.5초간 사라짐 
        Invoke("TestFI", 1.5f);//1.5초뒤 나타나는 코드 실행
    }

    public void Awake()
    {

    }

    void Update()
    {
       
    }

    void Continue_()
    {

    }

    void TestFI() {
        FadeIn(0.5f);//1초간 나타남
    }

    public void FadeIn(float fadeOutTime, System.Action nextEvent = null)
    {
        StartCoroutine(CoFadeIn(fadeOutTime, nextEvent));
    }

    public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
    {
        StartCoroutine(CoFadeOut(fadeOutTime, nextEvent));
    }


    // 투명 -> 불투명
    IEnumerator CoFadeIn(float fadeOutTime, System.Action nextEvent = null)
    {
        Image[] buttonImg = new Image[4];
        

        for (int i = 0; i < 4; i++)
        {
            buttonImg[i] = button[i].GetComponent<Image>(); //버튼의 이미지 속성을 가져와 넣는다.
        }

        Color tempColor = buttonImg[0].color; // tempcolor = 첫번째 버튼의 컬러값을 가진다.
        Color tempTextColor = buttonText[0].GetComponent<Text>().color; //tempTextColor는 첫번째 텍스트의 컬러값을 가진다.
        
        while (tempColor.a < 1f) //버튼의 투명도가 최대치가 될때까지
        {
            //투명도 증가
            tempColor.a += Time.deltaTime / fadeOutTime;
            tempTextColor.a = tempColor.a;

            //한번 투명도를 증가시키고 투명도를 적용함
            for (int i = 0; i < 4; i++)
            {
                buttonImg[i].color = tempColor; //버튼[i]번째 이미지의 컬러값에 tempcolor값을 넣는다.
                buttonText[i].GetComponent<Text>().color = tempTextColor; //텍스트[i]번째 컬러값에 temptextcolor값을 넣는다.
            }

            if (tempColor.a >= 1f){
                tempColor.a = 1f; //버튼의 투명도가 1을 넘지 않았나
                tempTextColor.a = 1f;
            }
                

            yield return null; //투명도가 증가했다면 null반환
        }

        for (int i = 0; i < 4; i++) //한번더 투명도 적용
        {
            buttonImg[i].color = tempColor;
            buttonText[i].GetComponent<Text>().color = tempTextColor;
        }

        if (nextEvent != null) nextEvent(); //null이 반환되었다면 다시 실행

        
        FadeOut(1.0f);
    }

    // 불투명 -> 투명
    IEnumerator CoFadeOut(float fadeOutTime, System.Action nextEvent = null)
    {
        Image[] buttonImg = new Image[4];

        for (int i = 0; i < 4; i++)
        {
            buttonImg[i] = button[i].GetComponent<Image>(); //버튼의 이미지 속성을 가져와 넣는다.
        }

        Color tempColor = buttonImg[0].color; // tempcolor = 첫번째 버튼의 컬러값을 가진다.
        Color tempTextColor = buttonText[0].GetComponent<Text>().color; //tempTextColor는 첫번째 텍스트의 컬러값을 가진다.


        while (tempColor.a > 0f)
        {
            tempColor.a -= Time.deltaTime / fadeOutTime;
            tempTextColor.a = tempColor.a;

            for (int i = 0; i < 4; i++)
            {
                buttonImg[i].color = tempColor; //버튼[i]번째 이미지의 컬러값에 tempcolor값을 넣는다.
                buttonText[i].GetComponent<Text>().color = tempTextColor; //텍스트[i]번째 컬러값에 temptextcolor값을 넣는다.
            }

            if (tempColor.a <= 0f){
                tempColor.a = 0f;// 버튼의 투명도가 0 이하로 갔는가
                tempTextColor.a = 0f; //텍스트의 투명도가 0이하로 갔는가
            }
            
            yield return null;
        }

        for (int i = 0; i < 4; i++) //한번더 투명도 적용
        {
            buttonImg[i].color = tempColor;
            buttonText[i].GetComponent<Text>().color = tempTextColor;
        }

        if (nextEvent != null) nextEvent();

        Invoke("TestFI", 1.0f);
    }
}

