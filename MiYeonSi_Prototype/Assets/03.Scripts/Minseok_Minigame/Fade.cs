using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public GameObject currentGameObj;
    public float alpha = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        currentGameObj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        changeAlpha(currentGameObj.GetComponent<Renderer>().material, alpha);
    }

    void changeAlpha(Material mat, float alphanum)
    {
        Color oldcolor = mat.color;
        Color newcolor = new Color(oldcolor.r, oldcolor.g, oldcolor.b, alphanum);
        mat.SetColor("_color", newcolor);
    }
}
