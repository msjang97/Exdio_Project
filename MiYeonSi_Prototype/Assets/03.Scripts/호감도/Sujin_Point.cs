using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sujin_Point : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Point_Plus_Five()
    {
        LovePoint.instance.Sujin_LovePoint += 5;
        SceneManager.LoadScene("Loading_Scene");
    }

    public void Point_Plus_Three()
    {
        LovePoint.instance.Sujin_LovePoint += 3;
        SceneManager.LoadScene("Loading_Scene");
    }

    public void Point_Zero()
    {
        LovePoint.instance.Sujin_LovePoint += 0;
        SceneManager.LoadScene("Loading_Scene");
    }

    public void Point_Minus_Five()
    {
        LovePoint.instance.Sujin_LovePoint -= 5;
        SceneManager.LoadScene("Loading_Scene");
    }
}
