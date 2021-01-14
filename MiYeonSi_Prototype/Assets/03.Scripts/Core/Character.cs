using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //테스트!!!(이수진)

[System.Serializable]
public class Character
{
    public string characterName; //캐릭터 이름 (깃헙 테스트 끝나면 삭제바람!)
    /// <summary>
    /// The root is the container for all images related to the character in the scene. The root object.
    /// </summary>
    [HideInInspector]public RectTransform root;

    public bool isMultiLayerCharacter { get { return renderers.renderer == null; } }
    
    /// <summary>
    ///  Create a new character.
    /// </summary>
    /// <param name=""></param>
    public Character (string _name)
    {
        CharacterManager cm = CharacterManager.instance;
        //locate the character prefab.
        GameObject prefab = Resources.Load("Characters/"+_name) as GameObject;
        if(prefab == null)
        {
            Debug.Log("프리팹이 null값입니다!!");
        }
        //spawn an instance of the prefab directly on the character panel.
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);

        root = ob.GetComponent<RectTransform>();
        characterName = _name;

        //get the renderer(s)
        renderers.renderer = ob.GetComponent<RawImage>();
        if(isMultiLayerCharacter)
        {
            renderers.bodyRenderer = ob.transform.Find("bodyLayer").GetComponent<Image>();
            renderers.expressionRenderer = ob.transform.Find("expressionLayer").GetComponent<Image>();
        }
        
    }

    class Renderers
    {
        /// <summary>
        /// used as the only umage for a single layer character    /// </summary>
        public RawImage renderer;

        //sprites use images.
        /// <summary>
        /// The body renderer for a multi layer character.
        /// </summary>
        public Image bodyRenderer;
        /// <summary>
        /// The expression renderer for a multi layer character.
        /// </summary>
        public Image expressionRenderer;
    }

    Renderers renderers = new Renderers();
}
