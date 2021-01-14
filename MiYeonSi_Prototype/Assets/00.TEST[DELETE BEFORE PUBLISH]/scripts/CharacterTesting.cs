using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTesting : MonoBehaviour
{
    public Character ChanWu; 

    // Use this for initialization
    void Start()
    {
        ChanWu = CharacterManager.instance.GetCharacter("ChanWu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
