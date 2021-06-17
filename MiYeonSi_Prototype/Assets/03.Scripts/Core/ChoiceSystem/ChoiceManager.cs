using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceManager : MonoBehaviour
{
    [HideInInspector] public int selectedNum = 0;
    [HideInInspector] public int chapterNum = 0;
    [HideInInspector] public bool isMainSceneLoaded = false;
    [HideInInspector] public int savedChapterProgress;

    public List<string> choices = new List<string>();
    public List<string> actions = new List<string>();

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

    void Update()
    {
        if (selectedNum != 0 && isMainSceneLoaded == false) //선택된 num이 있고, 아직 MainScene이 로드되지 않았을때 if문 실행.
        {
            StartCoroutine("LoadMainScene");
            isMainSceneLoaded = true;  
        }         
    }

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(2.0f); //애니메이션 재생시간만큼
        SceneManager.LoadScene("MainSystem");
    }
}
