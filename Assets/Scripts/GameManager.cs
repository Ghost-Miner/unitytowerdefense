using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region References and variables
    [HideInInspector]
    public static string versionName = "Alpha 1";

    private bool  gameEnded = false;
    private bool  isPaused  = false;
    private float hideArrowTime = 3f;

    public float meshUpdateTime = 100f;

    [SerializeField] private NavMeshSurface surface;

    [Header("Save file")]
    public bool levelFinished;
    public bool levelUnlocked;
    private string saveFile;

    [Header ("Game UI")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject HudPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private TMP_Text   moneyText;

    [Header ("world canvse")]
    [SerializeField] private GameObject trackArrow;
    [SerializeField] private GameObject hideEdgesCanvas;

    [Header ("Debug info")]
    [SerializeField] private TMP_Text fpsDisplay;
    [SerializeField] private TMP_Text versionText;
    [SerializeField] private TMP_Text sceneNameText;

    [SerializeField] private Toggle autoStartToggle;

    [SerializeField] private IngameSettings ingameSettings;
    #endregion

    private void Start()
    {
        string fileName = SceneManager.GetActiveScene().name + ".json";
        Debug.Log("file " + fileName);

        versionText.text   = versionName;
        sceneNameText.text = "Scene: " + SceneManager.GetActiveScene().name;

        hideEdgesCanvas.SetActive(true);
        StartCoroutine(HideTrackArrow());

        //ingameSettings = FindObjectOfType<IngameSettings>();

        //InvokeRepeating("UpdateMesh", 5f, meshUpdateTime);
    }

    void UpdateMesh()
    {
        surface.BuildNavMesh();

        Debug.Log("Mesh updated");
    }

    void Update()
    {
        // Display FPS
        int fps = (int)(1f / Time.unscaledDeltaTime);
        fpsDisplay.text = "FPS: " + fps;

        moneyText.text = "Money: " + PlayerStats.money;

        if (gameEnded)
        {
            return;
        }
        if (PlayerStats.lives <= 0)
        {
            EndGame();
        }   

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
                Debug.Log("TIME: " + Time.timeScale);
            }
            else
            {
                Resume();
                Debug.Log("TIME: " + Time.timeScale);
            }
        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void RestartScene ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void HidePanel (GameObject panel)
    {
        panel.SetActive(false);
    }
    public void ConfirmPanel (GameObject panel)
    {
        panel.SetActive(true);
    }

    public void AuttoStartButton ()
    {
        WaveManager.waveAutoStart = !WaveManager.waveAutoStart;
        Debug.Log("Autostart: " + WaveManager.waveAutoStart);
    }

    public void OpenSettings ()
    {
        ingameSettings.SetOptions();

        settingPanel.SetActive(true);
    }

    // Pause game 
    #region pause game
    public void Pause ()
    {
        isPaused = true;
         
        pausePanel.SetActive(true);

        Time.timeScale = 0f; 
    }

    public void Resume ()
    {
        isPaused = false;

        pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }
    #endregion

    void EndGame()
    {
        gameEnded = true;

        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }
    
    private IEnumerator HideTrackArrow()
    {
        trackArrow.SetActive(true);

        yield return new WaitForSeconds(hideArrowTime);

        trackArrow.SetActive(false);
    }


    // Save settings function
    #region Save and load settings function
    public void SaveGame()
    {
        string fileName = SceneManager.GetActiveScene().name + ".json";
        Debug.Log("file " + fileName);

        LevelData levelData = new LevelData
        {
            unlocked = levelUnlocked,
            finished = levelFinished,
        };

        string json = JsonUtility.ToJson(levelData);

        File.WriteAllText(Application.dataPath + "/Game data" + "/" + fileName + ".json", json);

        Debug.Log(json);
    }

    // Load settings function
    public void LoadGame()
    {
        string json = File.ReadAllText(Application.dataPath + "/Game data" + "/Level_.json");

        LevelData loadLevelData = JsonUtility.FromJson<LevelData>(json);
        
    }

    // List of settings values
    public class LevelData
    {
        public bool unlocked;
        public bool finished;
    }
    #endregion
}
