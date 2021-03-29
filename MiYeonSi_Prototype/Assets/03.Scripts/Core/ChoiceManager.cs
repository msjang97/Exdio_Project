using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceManager : MonoBehaviour
{
    private string selectedNum = null;
    public string P_selectedNum { get { return selectedNum; } set { selectedNum = value; } }

    private int chapterNum = 1;
    public int P_chapterNum { get { return chapterNum; } set { chapterNum = value; } }

    private bool isMainSceneLoaded = false;
    public bool P_isMainSceneLoaded { get { return isMainSceneLoaded; } set { isMainSceneLoaded = value; } }

    [HideInInspector] public int savedChapterProgress;

    public List<string> choices = new List<string>();
    public List<string> actions = new List<string>();

    //private GameObject ChoiceManagerObject; 
    private static ChoiceManager instance = null;
    public static ChoiceManager P_instance 
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedNum != null && isMainSceneLoaded == false)
        {
            StartCoroutine("LoadMainScene");
            isMainSceneLoaded = true;  
        }         
    }

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(2.0f); //애니메이션 재생시간만큼
        SceneManager.LoadScene("Chapter0");
    }
}
