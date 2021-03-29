using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TomainMenu : MonoBehaviour
{
    public void ChangeSceneBtn()
    {

        switch (this.gameObject.name)
        {
            case "ToMenu":
                SceneManager.LoadScene("toMainMenu");
                break;
        }
    }
}
