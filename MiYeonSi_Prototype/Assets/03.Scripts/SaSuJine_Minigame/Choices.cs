using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Choices : MonoBehaviour
{
    public TextMeshProUGUI[] tmpro;

    void Start()
    {
        InputText(); //mainScene에서 넘어온 텍스트를 미니게임 선택지에 적용.
    }

    private void InputText()
    {
        for(int i = 0; i < 4; i++)
        {
            tmpro[i].text = ChoiceManager.P_instance.choices[i];
        }
    }
}
