using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceTxt : MonoBehaviour
{
    private TextMeshProUGUI tmpro;   
    public string text { get { return tmpro.text; } set { tmpro.text = value; } }

    // Start is called before the first frame update
    void Start()
    {
        tmpro = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
