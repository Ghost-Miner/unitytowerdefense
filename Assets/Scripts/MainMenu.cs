using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    public void PlayGame ()
    {

    }

    public void LoadScene (string sceneName)
    {
        if (sceneName == null)
        {
            Debug.LogError("Scene name not set!");
        }
        SceneManager.LoadScene(sceneName);
    }

    public void OpenLink (string link)
    {
        if  (link == null)
        {
            Debug.LogError("MainMenu.cs | No link entered.");
            return;
        }
        Application.OpenURL(link);
    }
}
