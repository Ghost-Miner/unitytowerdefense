using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MainMenu : MonoBehaviour
{
    #region VARIABLES & REFERENCES
    public Animator anim;
    private string currentState;

    [SerializeField] private TMP_Text versionText;

    public SceneTransition sceneTransition;

    private bool levelFinished;
    private string dataFolder;
    private string saveFile;
    #endregion

    private void Start()
    {
        dataFolder = Application.dataPath + "/Game data" + "/";
        saveFile = "Tutorial.json";

        versionText.text = GameManager.versionName;

        ReadJson();
    }

    public void LoadScene (string sceneName)
    {
        sceneTransition.ChangeScene(sceneName);
    }

    public void PlayGame ()
    {
        string scene;

        if  (levelFinished)
        {
            scene = "Level select";
        }
        else
        {
            scene = "Tutorial 1";
        }

        sceneTransition.ChangeScene(scene);
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

    private void ReadJson()
    {
        if (!File.Exists(dataFolder + saveFile))
        {
            Debug.Log("File " + saveFile + " doesnt exist");

            levelFinished = false;
        }

        string json = File.ReadAllText(dataFolder + saveFile);

        LevelData levelData = JsonUtility.FromJson<LevelData>(json);
        Debug.Log(gameObject.name + ": " + json);

        levelFinished = levelData.finished;
    }

    // List of settings values
    public class LevelData
    {
        public bool unlocked;
        public bool finished;
    }
}
