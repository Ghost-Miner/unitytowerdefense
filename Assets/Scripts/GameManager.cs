using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static string versionName = "Alpha 1";

    private bool gameEnded = false;
    private bool isPaused  = false;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject HudPanel;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelWonPanel;

    [SerializeField] private GameObject hideEdgesCanvas;

    [SerializeField] private TMP_Text fpsDisplay;
    [SerializeField] private TMP_Text versionText;

    [SerializeField] private Toggle autoStartToggle;

    private void Start()
    {
        versionText.text = versionName;
        hideEdgesCanvas.SetActive(true);
    }

    void Update()
    {
        // Display FPS
        int fps = (int)(1f / Time.unscaledDeltaTime);
        fpsDisplay.text = "FPS: " + fps;

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
        //HudPanel.SetActive(false);

        Time.timeScale = 0f;
    }

    public void Resume ()
    {
        isPaused = false;

        pausePanel.SetActive(false);
        //HudPanel.SetActive(true);

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
