using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstToMainMenu : MonoBehaviour
{

    public void ChangeSceneBtn()
    {

        switch (this.gameObject.name)
        {
            case "ButtonToMenu":
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
   
}
