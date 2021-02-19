using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class architectTesting : MonoBehaviour
{
    public TextMeshProUGUI tmprotext;
    TextArchitect architect;

    [TextArea(5, 10)]
    public string say;
    public int charactersPerFrame = 1;
    public float speed = 1f;

    //Use this for initialization
    void Start()
    {
        architect = new TextArchitect(tmprotext, say, "", charactersPerFrame, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            architect = new TextArchitect(tmprotext, say, "", charactersPerFrame, speed);
        }
    }
}
