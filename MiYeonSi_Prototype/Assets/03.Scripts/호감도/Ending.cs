using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public int Enji_Ending_point; // 엔딩시 은지 호감도
    public int Sujin_Ending_point; // 엔딩시 수진 호감도 
    public int Minseok_Ending_point; // 엔딩시 민석 호감도
    public int Hagyoung_Ending_point; // 엔딩시 하경 호감도

    public int Com_Point = 0; //세사람 중 누가 가장 적을지 비교하기 위해서.


    // Start is called before the first frame update
    void Start()
    {
        Enji_Ending_point = LovePoint.instance.Enji_LovePoint;
        Sujin_Ending_point = LovePoint.instance.Sujin_LovePoint;
        Minseok_Ending_point = LovePoint.instance.Minseok_LovePoint;
        Hagyoung_Ending_point = LovePoint.instance.Hagyoung_LovePoint;
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void End()
    {
        //호감도 최고점 찾기
        Com_Point = Minseok_Ending_point < Sujin_Ending_point ? (Sujin_Ending_point < Hagyoung_Ending_point ? Hagyoung_Ending_point : Sujin_Ending_point) : (Minseok_Ending_point < Hagyoung_Ending_point ? Hagyoung_Ending_point : Minseok_Ending_point); 
        
        if (Enji_Ending_point >= 85) //무조건 은지의 호감도가 85점 이상이면 은지엔딩.
            SceneManager.LoadScene("Enji_End");

        else
        {
            //3개가 다 똑같으면 솔로엔딩
            if (Minseok_Ending_point == Sujin_Ending_point && Sujin_Ending_point == Hagyoung_Ending_point && Hagyoung_Ending_point == Minseok_Ending_point) 
            {
                SceneManager.LoadScene("Solo_End");
            }

            else
            {
                if (Com_Point == Sujin_Ending_point) //수진이 최고점수 일 때
                {
                    if (Sujin_Ending_point == Hagyoung_Ending_point || Sujin_Ending_point == Minseok_Ending_point) //뭐랑 같아도 솔로엔딩
                    {
                        SceneManager.LoadScene("Solo_End");
                    }
                    else
                    {
                        SceneManager.LoadScene("Sujin_End"); //수진이 점수만 최고점이면 수진 엔딩
                    }
                }

                else if (Com_Point == Hagyoung_Ending_point) //하경이 최고점수 일 때
                {
                    if (Hagyoung_Ending_point == Sujin_Ending_point || Hagyoung_Ending_point == Minseok_Ending_point ) //뭐랑 같아도 솔로엔딩
                    {
                        SceneManager.LoadScene("Solo_End");
                    }
                    else
                    {
                        SceneManager.LoadScene("Hagyoung_End"); //하경이 점수만 최고점이면 하경 엔딩
                    }
                }

                if (Com_Point == Minseok_Ending_point) //민석이 점수가 최고점일때
                {
                    if (Minseok_Ending_point == Sujin_Ending_point || Minseok_Ending_point == Hagyoung_Ending_point) //뭐랑같아도 솔로엔딩
                    {
                        SceneManager.LoadScene("Solo_End"); 
                    }
                    else
                    {
                        SceneManager.LoadScene("Minseok_End"); // 민석아저씨 점수만 최고점일때 민석엔딩
                    }
                }
            }
        }
    }
}

