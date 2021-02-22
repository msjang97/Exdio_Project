using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LovePoint : MonoBehaviour
{
    public static LovePoint instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    public int Enji_LovePoint = 0; // 최은지 호감도
    public int Minseok_LovePoint = 0; // 마민석 호감도
    public int Sujin_LovePoint = 0; // 사수진 호감도
    public int Hagyoung_LovePoint = 0; // 안하경 호감도

  

    public bool Enji_end = true;
    public bool Minseok_end = false;
    public bool Sujin_end = false;
    public bool Hagyong_end = false;
    public bool Solo_end = false;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {      

    }
}
