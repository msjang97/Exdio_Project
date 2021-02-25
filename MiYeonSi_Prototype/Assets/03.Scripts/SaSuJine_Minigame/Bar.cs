using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    private float upLimit = 3;
    private float downLimit = -3;
    private float moveSpeed = 15;
    private int direction = 1;

    private bool isStoped = false;
    public bool P_isStoped { get { return isStoped; } }

    private Vector3 BarPosition;
    public Vector3 P_BarPosition { get { return BarPosition; } }

    // Update is called once per frame
    void Update()
    {
        if(isStoped == false)
            MoveBar();       
    }

    private void MoveBar()
    {
        if (transform.position.y < downLimit)
            direction = 1;
        else if (transform.position.y > upLimit)
            direction = -1;

        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime * direction);
    }

    public void StopBar()
    {
        isStoped = true;
        BarPosition = this.transform.position;
    }  
}
