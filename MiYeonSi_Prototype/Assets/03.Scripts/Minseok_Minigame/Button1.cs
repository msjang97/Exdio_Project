using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Button1 : MonoBehaviour
{
    public GameObject[] Buttons = new GameObject[4];
    
    private float time = 0;


    public int ButtonX ; //-170~185
    public int Button1Y; // 400 ~ 700
    public int Button2Y; // 100 ~ 400
    public int Button3Y; // -200 ~ 100
    public int Button4Y; // -500 ~ -200


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {      
        if (time < 0.5f)
        {    
        }

        else if (time > 0.5f)
        {
            getRandomInt(4, 0, 4);          
            Invoke("ResetTime", 1.0f);                
        }
        time += Time.deltaTime;
    }
    

    public void getRandomInt(int length, int min, int max)
    {
        int[] randArray = new int[length];
        bool isSame;

        for (int i = 0; i < length; ++i)
        {
            while (true)
            {
                randArray[i] = Random.Range(min, max); 
                isSame = false;
                for (int j = 0; j < i; ++j) 
                {                  
                    if (randArray[j] == randArray[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
        }

        for (int i = 0; i < length; i++)
        {
            movePos(randArray[i], Buttons[i]); 
        }
       
    }


    private void ResetTime()
    {
        //this.gameObject.SetActive(true);
        time = 0;
    }

    public void Click()
    {
        this.gameObject.SetActive(false);
    }


    public void movePos(int ranNum, GameObject gameObject)
    {
        if (ranNum == 0)
        {
            //gameObject.transform.localPosition = new Vector3(0.0f, 370.0f, 0.0f);
            ButtonX = Random.Range(-170, 186);
            Button1Y= Random.Range(500, 751); // 500 ~ 750 250
            gameObject.transform.localPosition = new Vector3(ButtonX, Button1Y, 0.0f);
        }

        else if (ranNum == 1)
        {
            //gameObject.transform.localPosition = new Vector3(0.0f, 150.0f, 0.0f);
            ButtonX = Random.Range(-170, 186);
            Button2Y = Random.Range(150, 401);// 150 ~ 400 250
            gameObject.transform.localPosition = new Vector3(ButtonX, Button2Y, 0.0f);
        }
        else if (ranNum == 2)
        {
            //gameObject.transform.localPosition = new Vector3(0.0f, -70.0f, 0.0f);
            ButtonX = Random.Range(-170, 186);
            Button3Y = Random.Range(-200, 51); // -200 ~ -50 250
            gameObject.transform.localPosition = new Vector3(ButtonX, Button3Y, 0.0f);
        }

        else if (ranNum == 3)
        {
            //gameObject.transform.localPosition = new Vector3(0.0f, -290.0f, 0.0f);
            ButtonX = Random.Range(-170, 186);
            Button4Y = Random.Range(-550, -301); // -550 ~ -300 250
            gameObject.transform.localPosition = new Vector3(ButtonX, Button4Y, 0.0f);
        }
    }
}
