using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private string versionName = "some verision";

    private bool gameEnded = false;
    private bool isPaused = false;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject HudPanel;

    public Animator shopPanelAnimator;

    [SerializeField] private GameObject hideEdgesCanvas;

    [SerializeField] private TMP_Text fpsDisplay;
    [SerializeField] private TMP_Text versionText;

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

    public void Pause ()
    {
        isPaused = true;

        pausePanel.SetActive(true);
        HudPanel.SetActive(false);

        shopPanelAnimator.SetTrigger("hide");

        Time.timeScale = 0f;
    }

    public void Resume ()
    {
        isPaused = false;

        pausePanel.SetActive(false);
        HudPanel.SetActive(true);

        shopPanelAnimator.SetTrigger("show");

        Time.timeScale = 1f;
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("game ended");
    }
}
