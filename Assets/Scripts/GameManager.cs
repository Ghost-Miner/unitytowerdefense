using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region References and variables
    public static string versionName = "Alpha 1";

    private bool gameEnded = false;
    private bool isPaused  = false;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject HudPanel;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelWonPanel;

    [SerializeField] private GameObject hideEdgesCanvas;

    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private TMP_Text fpsDisplay;
    [SerializeField] private TMP_Text versionText;
    [SerializeField] private TMP_Text sceneNameText;

    [SerializeField] private Toggle autoStartToggle;
    #endregion

    private void Start()
    {
        versionText.text   = versionName;
        sceneNameText.text = "Scene: " + SceneManager.GetActiveScene().name;

        hideEdgesCanvas.SetActive(true);
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
        Debug.Log("Autpstart: " + WaveManager.waveAutoStart);
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
}
