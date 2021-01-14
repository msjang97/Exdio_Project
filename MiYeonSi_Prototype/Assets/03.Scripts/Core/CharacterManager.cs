using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for adding and maintaining characters in the scene.
/// </summary>
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    
    /// <summary>
    /// All characters must be attached to the character panel.
    /// </summary>
    public RectTransform characterPanel;

    /// <summary>
    /// A list of all characters surrently in our scene.
    /// </summary>
    public List<Character> characters = new List<Character>();

    /// <summary>
    /// Easy lookup for our characters.
    /// </summary>
    public Dictionary<string, int> characterDictionary = new Dictionary<string, int>();

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Try to get a character by the name provided from the character list.
    /// </summary>
    /// <param name = "characterName">Character name.</param>
    public Character GetCharacter(string characterName, bool createCharacterIfDoesNotExist = true)
    {
        //search our dictionary to find the character quickly if it is already in our scene.
        int index = -1;
        if(characterDictionary.TryGetValue (characterName, out index))
        {
            return characters[index];
        }
        else if (createCharacterIfDoesNotExist)
        {
            return CreateCharacter(characterName);
        }

        return null;
    }

    /// <summary>
    /// Creates the character.
    /// </summary>
    /// <param name="characterName"></param>
    public Character CreateCharacter (string characterName)
    {
        Character newCharacter = new Character(characterName);

        characterDictionary.Add(characterName, characters.Count);
        characters.Add(newCharacter);

        return newCharacter;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
