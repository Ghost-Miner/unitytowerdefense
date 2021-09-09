using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    string versionName = "some verision";

    bool gameEnded = false;
    bool isPaused = false;

    public GameObject pausePanel;
    public GameObject shopPaael;
    public GameObject pauseButton;

    public Animator shopPanelAnimator;

    [SerializeField] private GameObject hideEdgesCanvas;

    public TMP_Text fpsDisplay;                     //FPS meter
    public TMP_Text versionText;

    ///ObjectPooler objectPooler; 

    private void Start()
    {
        versionText.text = versionName;
        hideEdgesCanvas.SetActive(true);
        ///objectPooler = ObjectPooler.Instance;
    }

    void Update()
    {
        // Display FPS
        int fps = (int)(1f / Time.unscaledDeltaTime);
        fpsDisplay.text = "FPS: " + fps;

        //objectPooler.SpawnFromPool("Yellow", transform.position, Quaternion.identity);

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
        pauseButton.SetActive(false);
        //shopPaael.SetActive(false);
        shopPanelAnimator.SetTrigger("hide");

        Time.timeScale = 0f;
    }

    public void Resume ()
    {
        isPaused = false;

        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        //shopPaael.SetActive(true);
        shopPanelAnimator.SetTrigger("show");

        Time.timeScale = 1f;
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("game ended");
    }
}
