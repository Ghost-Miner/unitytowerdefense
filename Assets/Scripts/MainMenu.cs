using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region VARIABLES & REFERENCES
    // Animations
    public Animator anim;
    private string currentState;

    public SceneTransition sceneTransition;
    #endregion

    public void LoadScene (string sceneName)
    {
        sceneTransition.ChangeScene(sceneName);
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

    public void PlayAnimation (string animName)
    {
        ChangeAnimationState(animName);
        anim.speed = 1.5f;

        Debug.Log("mouse engtered"+ animName);
    }

    //Change animation
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }

        anim.Play(newState);

        currentState = newState;
    }
}
