using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelController : MonoBehaviour
{
    public static NovelController instance;

    /// <summary> The lines of data loaded directly from a chapter file. /// </summary>
    List<string> data = new List<string>();
    /// <summary> The progress in the current data list. /// </summary>

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadChapterFile("Chapter0_start");
    }

    // Update is called once per frame
    void Update()
    {
        //testing
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Next();
            Debug.Log("넥스트가 true로 바뀜!!!!!!!!!!!!!!!!!!1");
        }
    }

    bool _next = false;
    public void Next()
    {
        _next = true;
    }

    public void LoadChapterFile(string fileName)
    {
        data = FileManager.LoadFile(FileManager.savPath + "Resources/Story/" + fileName);
        cachedLastSpeaker = "";
        
        if (handlingChapterFile != null)
            StopCoroutine(handlingChapterFile);
        handlingChapterFile = StartCoroutine(HandlingChapterFile());

        Next();
    }

    public bool isHandlingChapterFile { get { return handlingChapterFile != null; } }

    Coroutine handlingChapterFile = null;
    [HideInInspector] public int chapterProgress = 0;
    IEnumerator HandlingChapterFile()
    {
        //the progress through the lines in this chapter.
        chapterProgress = 0;

        while(chapterProgress < data.Count)
        {
            //we need a way of knowing when the player wants to advance. We nees d "next" trigger.Not just a keypress. But something that can be triggerd.
            //by a click or a keypress
            if(_next)
            {
                string line = data[chapterProgress];

                //this is a choice
                if(line.StartsWith("choice"))
                {
                    yield return HandlingChoiceLine(line);
                    chapterProgress++;
                }
                //this is a normal line of dialogue and actions.
                else
                {
                    HandleLine(data[chapterProgress]);
                    chapterProgress++;
                    while (isHandlingLine)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }
                
            }
            yield return new WaitForEndOfFrame();
        }
        handlingChapterFile = null;
    }


    IEnumerator HandlingChoiceLine(string line)
    {
        string title = line.Split('"')[1];
        List<string> choices = new List<string>();
        List<string> actions = new List<string>();

        bool gatheringChoices = true;
        while(gatheringChoices)
        {
            chapterProgress++;
            line = data[chapterProgress];

            if (line == "{")
                continue;

            line = line.Replace("    ", "");

            if(line != "}")
            {
                choices.Add(line.Split('"')[1]);
                actions.Add(data[chapterProgress + 1].Replace("    ", ""));
                chapterProgress++;
            }
            else
            {
                gatheringChoices = false;
            }
        }

        //display choices
        if(choices.Count > 0)
        {
            ChoiceScreen.Show(choices.ToArray()); yield return new WaitForEndOfFrame();
            while (ChoiceScreen.isWaitingForChoiceToBeMade)
                yield return new WaitForEndOfFrame();

            //choice is made. execute the paired action.
            string action = actions[ChoiceScreen.lastChoiceMade.index];
            HandleLine(action);

            while (isHandlingLine)
                yield return new WaitForEndOfFrame();
        }
        else
        {
            Debug.LogError("Invalid choice operation. No choices were found.");
        }

        chapterProgress++;
    }

    void HandleLine(string rawLine)
    {
        CLM.LINE line = CLM.Interpret(rawLine);

        //now we need to handle the line. This requires a loop full of waiting for input since the line consists of multiple segments that hve to be handled individually.
        StopHandlingLine();
        handlingLine = StartCoroutine(HandlingLine(line));

    }

    void StopHandlingLine()
    {
        if (isHandlingLine)
            StopCoroutine(handlingLine);
        handlingLine = null;
    }

    [HideInInspector]
    //Used as a fallback when no speaker is given.
    public string cachedLastSpeaker = "";

    public bool isHandlingLine { get { return handlingLine != null; } }
    Coroutine handlingLine = null;
    IEnumerator HandlingLine(CLM.LINE line)
    {
        _next = false;
        int lineProgress = 0; //progress through the segments of a line.

        while(lineProgress < line.segments.Count)
        {
            _next = false; //reset at the start of each loop.
            CLM.LINE.SEGMENT segment = line.segments[lineProgress];

            //always run the first segment automatically. But wait for the trigger on all proceding segments.
            if(lineProgress > 0)
            {
                if(segment.trigger == CLM.LINE.SEGMENT.TRIGGER.autoDelay)
                {
                    for(float timer = segment.autoDelay; timer >= 0; timer -= Time.deltaTime)
                    {
                        yield return new WaitForEndOfFrame();
                        if (_next)
                            break; //allow the termination of a delay when "next" is triggered. Prevents unskippable wait timers.
                    }
                }
                else
                {
                    while (!_next)
                        yield return new WaitForEndOfFrame(); //wait untill the player says move to the next segment.
                }
            }
            _next = false; // next could have been triggered during an event above.

            //the segment now needs to build and run.
            segment.Run();

            while(segment.isRunning)
            {
                yield return new WaitForEndOfFrame();
                //allow for auto completion of the current segment for skipping purposes.
                if(_next)
                {
                    //rapidly complete the text on first advance, force it to finish on the second.
                    if (!segment.architect.skip)
                        segment.architect.skip = true;
                    else
                        segment.ForceFinish();
                }
            }

            lineProgress++;

            yield return new WaitForEndOfFrame();
        }

        //Line is finished. Handle all the actions set at the end of the line.
        for(int i = 0; i<line.actions.Count; i++)
        {
            HandleAction(line.actions[i]);
        }

        handlingLine = null;       
    }

  
    //ACTIONS
    // /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void HandleAction(string action)
    {
        print("Handle action [" + action + "]");
        string[] data = action.Split('(', ')');

        switch(data[0])
        {
            case "enter":
                Command_Enter(data[1]);
                break;
            case "exit":
                Command_Exit(data[1]);
                break;
            case "setBackground":
                Command_SetLayerImage(data[1], BCFC.instance.background);
                break;
            case "setCinematic":
                Command_SetLayerImage(data[1], BCFC.instance.cinematic);
                break;
            case "setForeground":
                Command_SetLayerImage(data[1], BCFC.instance.foreground);
                break;
            case "playSound":
                Command_PlaySound(data[1]);
                break;
            case "playMusic":
                Command_PlayMusic(data[1]);
                break;
            case "move":
                Command_MoveCharacter(data[1]);
                break;
            case "setPosition":
                Command_SetPosition(data[1]);
                break;
            case "changeExpression":
                Command_ChangeExpression(data[1]);
                break;
            case "Load":
                Command_Load(data[1]);
                break;
        }
    }

    void Command_Load(string chapterName)
    {
        NovelController.instance.LoadChapterFile(chapterName);
    }

    void Command_SetLayerImage(string data, BCFC.LAYER layer)
    {
        string texName = data.Contains(",") ? data.Split(',')[0] : data;
        Texture2D tex = texName == "null" ? null : Resources.Load("Images/UI/Backdrops/" + texName) as Texture2D;
        float spd = 2f;
        bool smooth = false;

        if(data.Contains(","))
        {
            string[] parameters = data.Split(',');
            foreach(string p in parameters)
            {
                float fVal = 0;
                bool bVal = false;
                if (float.TryParse(p, out fVal))
                {
                    spd = fVal;  continue;
                }
                if (bool.TryParse(p, out bVal))
                {
                    smooth = bVal;  continue;
                }
            }
        }
        layer.TransitionToTexture(tex, spd, smooth);
    }

    void Command_PlaySound(string data)
    {
        AudioClip clip = Resources.Load("Audio/SFX/" + data) as AudioClip;

        if (clip != null)
            AudioManager.instance.PlaySFX(clip);
        else
            Debug.LogError("Clip does not exist - " + data);
    }

    void Command_PlayMusic(string data)
    {
        AudioClip clip = Resources.Load("Audio/Music/" + data) as AudioClip;

        if (clip != null)
            AudioManager.instance.PlaySong(clip);
        else
            Debug.LogError("Clip does not exist - " + data);
    }

    void Command_MoveCharacter(string data)
    {
        string[] parameters = data.Split(',');
        string character = parameters[0];
        float locationX = float.Parse(parameters[1]);
        float locationY = float.Parse(parameters[2]);
        float speed = parameters.Length >= 4 ? float.Parse(parameters[3]) : 1f;
        bool smooth = parameters.Length == 5 ? bool.Parse(parameters[4]) : true;

        Character c = CharacterManager.instance.GetCharacter(character);
        c.MoveTo(new Vector2(locationX, locationY), speed, smooth);
    }
    void Command_SetPosition(string data)
    {
        string[] parameters = data.Split(',');
        string character = parameters[0];
        float locationX = float.Parse(parameters[1]);
        float locationY = float.Parse(parameters[2]);

        Character c = CharacterManager.instance.GetCharacter(character);
        c.SetPosition(new Vector2(locationX, locationY));
    }
    void Command_ChangeExpression(string data)
    {
        string[] parameters = data.Split(',');
        string character = parameters[0];
        string region = parameters[1];
        string expression = parameters[2];
        float speed = parameters.Length == 4 ? float.Parse(parameters[3]) : 1f;

        Character c = CharacterManager.instance.GetCharacter(character);
        Sprite sprite = c.GetSprite(expression);

        if (region.ToLower() == "body")
            c.TransitionBody(sprite, speed, false);
        if (region.ToLower() == "face")
            c.TransitionBody(sprite, speed, false);
    }

    void Command_Enter(string data)
    {
        string[] parameters = data.Split(',');
        string[] characters = parameters[0].Split(';');
        float speed = 3;
        bool smooth = false;
        for(int i = 1; i < parameters.Length; i++)
        {
            float fVal = 0; bool bVal = false;
            if(float.TryParse(parameters[i], out fVal))
            { speed = fVal; continue; }
            if(bool.TryParse(parameters[i], out bVal))
            { smooth = bVal; continue; }

        }

        foreach(string s in characters)
        {
            Character c = CharacterManager.instance.GetCharacter(s, true, false);
            if(!c.enabled)
            {
                c.renderers.bodyRenderer.color = new Color(1, 1, 1, 0);
                //c.renderers.expressionRenderer.color = new Color(1, 1, 1, 0);
                c.enabled = true;

                c.TransitionBody(c.renderers.bodyRenderer.sprite, speed, smooth);
                //expression은 안함
            }
        }
    }

    void Command_Exit(string data)
    {
        string[] parameters = data.Split(',');
        string[] characters = parameters[0].Split(';');
        float speed = 3;
        bool smooth = false;
        for(int i = 1; i<parameters.Length; i++)
        {
            float fVal = 0; bool bVal = false;
            if(float.TryParse(parameters[i], out fVal))
            { speed = fVal; continue; }
            if(bool.TryParse(parameters[i], out bVal))
            { smooth = bVal; continue; }
        }

        foreach(string s in characters)
        {
            Character c = CharacterManager.instance.GetCharacter(s);
            c.FadeOut(speed, smooth);
        }
    }

}
