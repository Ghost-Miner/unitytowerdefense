using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    #region VARIABLES & REFERENCES
    public Animator anim;
    private string currentState;

    [SerializeField] private TMP_Text versionText;

    public SceneTransition sceneTransition;
    #endregion

    private void Start()
    {
        versionText.text = GameManager.versionName;

        SoundManager.PlaySound(SoundManager.Sound.m_Breaktime);
    }

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
