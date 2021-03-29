using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public int CLICK = 0;  // 0에서 시작

    public int CLICKK = 1;  // 한 번 터치 할 때마다 1씩 증가

    public void OnClick()
    {
        CLICK = CLICK + CLICKK;
    }
}
