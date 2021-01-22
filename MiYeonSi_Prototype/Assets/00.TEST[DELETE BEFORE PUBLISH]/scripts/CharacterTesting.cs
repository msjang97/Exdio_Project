using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTesting : MonoBehaviour
{
    public Character ChanWu;
    public Character LoveCell;

    // Use this for initializations
    void Start()
    {
        ChanWu = CharacterManager.instance.GetCharacter("ChanWu", enableCreatedCharacterOnStart: false);
        ChanWu.GetSprite(2);
        //LoveCell = CharacterManager.instance.GetCharacter("LoveCell", enableCreatedCharacterOnStart: false);
    }

    public string[] speech;
    int i = 0;

    public Vector2 moveTarget;
    public float moveSpeed;
    public bool smooth;

    public int bodyIndex, expressionIndex = 0;
    public float speed = 5f;
    public bool smoothranstitions = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (i < speech.Length)
                ChanWu.Say(speech[i]);
            else
                DialogueSystem.instance.Close();
            i++;
        }

        if (Input.GetKey(KeyCode.M))
        {
            ChanWu.MoveTo(moveTarget, moveSpeed, smooth);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChanWu.StopMoving(true);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Input.GetKey(KeyCode.T))
            {
                Debug.Log("눌렸어요");
                ChanWu.TransitionBody(ChanWu.GetSprite(CharacterManager.characterExpressions.happy), speed, smoothranstitions);
            }
            else
                ChanWu.SetBody(bodyIndex);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ChanWu.SetExpression(bodyIndex);
        }

    }
}
