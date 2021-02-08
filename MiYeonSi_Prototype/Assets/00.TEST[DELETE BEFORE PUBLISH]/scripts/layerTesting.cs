using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layerTesting : MonoBehaviour
{
    BCFC controller;

    public Texture tex;

    public float speed;
    public bool smooth;


    // Start is called before the first frame update
    void Start()
    {
        controller = BCFC.instance; //원래는 BCFC.instance 였음.
    }

    // Update is called once per frame
    void Update()
    {
        BCFC.LAYER layer = null;

        if (Input.GetKey(KeyCode.Q))
            layer = controller.background;
        if (Input.GetKey(KeyCode.W))
            layer = controller.cinematic;
        if (Input.GetKey(KeyCode.E))
            layer = controller.foreground;


        if (Input.GetKey(KeyCode.T))
        {
            if (Input.GetKeyDown(KeyCode.A))
                layer.TransitionToTexture(tex, speed, smooth);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            layer.SetTexture(tex);
        }
    }
}
