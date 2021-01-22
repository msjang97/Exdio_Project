using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public string characterName;
    /// <summary>
    /// The root is the container for all images related to the character in the scene. The root object.
    /// </summary>
    [HideInInspector] public RectTransform root;

    public bool isMultiLayerCharacter { get { return renderers.renderer == null; } }

    public bool enabled { get { return root.gameObject.activeInHierarchy; } set { root.gameObject.SetActive(value); } }

    /// <summary>
    /// The space between the anchors of this character. Defines how much space a character takes up on the canvas.
    /// </summary>
    /// <value>The anchor padding.</value>
    public Vector2 anchorPadding { get { return root.anchorMax - root.anchorMin; } }

    DialogueSystem dialogue;

    /// <summary>
    /// Make this character say something
    /// </summary>
    /// <param name="speech"></param>
    public void Say(string speech, bool add = false)
    {
        if (!enabled)
            enabled = true;

        if (!add)
            dialogue.Say(speech, characterName);
        else dialogue.SayAdd(speech, characterName);
    }

    Vector2 targetPosition;
    Coroutine moving;
    bool isMoving { get { return moving != null; } }
    /// <summary>
    /// Move to a specific point relative to the canvas spave. (1,1) = far top right, (0, 0) = far bottom left, (0.5, 0.5) = Middle.
    /// </summary>
    /// <param name="Target"></param>
    /// <param name="speed"></param>
    /// <param name="smooth"></param>
    public void MoveTo(Vector2 Target, float speed, bool smooth = true)
    {
        //if we are moving, stop moving
        StopMoving();
        //start moving coroutine.
        moving = CharacterManager.instance.StartCoroutine(Moving(Target, speed, smooth));
    }


    // Begin Trasnitioning Images
    public Sprite GetSprite(int index = 0)
    {
        //스프라이트를 안쓰고 이미지를 불러다 바꾸고 싶을때
        //Sprite sprite = Resources.Load<Sprite>("Images/Character/GiChanWu/GiChanWu_Panic");
        //return sprite;

        // 스프라이트로 불러 올 때.
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/Character/GiChanWu/" + characterName);
        Debug.Log(sprites.Length);
        return sprites[index];
    }

    public void SetBody(int index)
    {
        renderers.bodyRenderer.sprite = GetSprite(index);
    }

    public void SetBody(Sprite sprite)
    {
        renderers.bodyRenderer.sprite = sprite;
    }

    public void SetExpression(int index)
    {
        renderers.expressionRenderer.sprite = GetSprite(index);
    }

    public void SetExpression(Sprite sprite)
    {
        renderers.expressionRenderer.sprite = sprite;
    }

    Coroutine transitioningBody = null;
    bool isTransitioningBody { get { return transitioningBody != null; } }


    public void TransitionBody(Sprite sprite, float speed, bool smooth)
    {
        if (renderers.bodyRenderer.sprite == sprite)
            return;

        StopTranstioningBody();
        transitioningBody = CharacterManager.instance.StartCoroutine(TransitioningBody(sprite, speed, smooth));
    }

    void StopTranstioningBody()
    {
        if (isTransitioningBody)
            CharacterManager.instance.StopCoroutine(transitioningBody);
        transitioningBody = null;
    }

    public IEnumerator TransitioningBody(Sprite sprite, float speed, bool smooth)
    {
        for (int i = 0; i < renderers.allBodyRenderers.Count; i++)
        {
            Image image = renderers.allBodyRenderers[i];
            if (image.sprite == sprite)
            {
                renderers.bodyRenderer = image;
                break;
            }
        }

        if (renderers.bodyRenderer.sprite != sprite)
        {
            Image image = GameObject.Instantiate(renderers.bodyRenderer.gameObject, renderers.bodyRenderer.transform.parent).GetComponent<Image>();
            renderers.allBodyRenderers.Add(image);
            renderers.bodyRenderer = image;
            image.color = GlobalF.SetAlpha(image.color, 0f);
            image.sprite = sprite;
        }

        while (GlobalF.TransitionImages(ref renderers.bodyRenderer, ref renderers.allBodyRenderers, speed, smooth))
            yield return new WaitForEndOfFrame();

        StopTranstioningBody();
    }


    // End Transition Images




    /// <summary>
    /// Stops the character in its tracks, either setting it immediately at the target position or not.
    /// </summary>
    /// <param name="arriveAtTargetPositionImmediately">If set to <c>true</c> arrive at target position immediately.</param>
    public void StopMoving(bool arriveAtTargetPositionImmediately = false)
    {
        if (isMoving)
        {
            CharacterManager.instance.StopCoroutine(moving);
            if (arriveAtTargetPositionImmediately)
                SetPosition(targetPosition);
        }
        moving = null;
    }

    /// <summary>
    /// Imediately set the position of this character to the intended target.
    /// </summary>
    /// <param name="target"></param>
    public void SetPosition(Vector2 target)
    {
        Vector2 padding = anchorPadding;
        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;
        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);

        root.anchorMin = minAnchorTarget;
        root.anchorMax = root.anchorMin + padding;
    }

    /// <summary>
    /// The coroutine that runs to gradually move the character towards a position.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="speed"></param>
    /// <param name="smooth"></param>
    /// <returns></returns>
    IEnumerator Moving(Vector2 target, float speed, bool smooth)
    {
        targetPosition = target;

        //now we want to get the padding between the anchors of this character so we know whar their min and max positions are.
        Vector2 padding = anchorPadding;

        //now get the limitations for 0 to 100% movement. The farthest a character can move to the right before reaching 100% should be the 1 value - the padding.
        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        //now get the actual position target for the minimum anchors (left / bottom bounds) of the character. becuase maxX and maxY is just a percent reference.
        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);
        speed *= Time.deltaTime;

        //move untill we reach the target position.
        while (root.anchorMin != minAnchorTarget)
        {
            root.anchorMin = (!smooth) ? Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed) : Vector2.Lerp(root.anchorMin, minAnchorTarget, speed);
            root.anchorMax = root.anchorMin + padding;
            yield return new WaitForEndOfFrame();
        }
        StopMoving();
    }

    /// <summary>
    ///  Create a new character.
    /// </summary>
    /// <param name=""></param>
    public Character(string _name, bool enableOnStart = true)
    {
        CharacterManager cm = CharacterManager.instance;
        //locate the character prefab.
        GameObject prefab = Resources.Load("Characters/" + _name) as GameObject;
        if (prefab == null)
        {
            Debug.Log("프리팹이 null값입니다!!");
        }
        //spawn an instance of the prefab directly on the character panel.
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);

        root = ob.GetComponent<RectTransform>();
        characterName = _name;

        //get the renderer(s)
        renderers.renderer = ob.GetComponent<RawImage>();
        if (isMultiLayerCharacter)
        {
            renderers.bodyRenderer = ob.transform.Find("BodyLayer").GetComponentInChildren<Image>();
            renderers.expressionRenderer = ob.transform.Find("ExpressionLayer").GetComponentInChildren<Image>();
            renderers.allBodyRenderers.Add(renderers.bodyRenderer);
            renderers.allExpressionRenderers.Add(renderers.expressionRenderer);
        }

        dialogue = DialogueSystem.instance;

        enabled = enableOnStart;
    }

    [System.Serializable]
    public class Renderers
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


        public List<Image> allBodyRenderers = new List<Image>();
        public List<Image> allExpressionRenderers = new List<Image>();

    }

    public Renderers renderers = new Renderers();
}
