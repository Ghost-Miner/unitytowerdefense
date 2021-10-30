using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class ErrorReportWindow : MonoBehaviour
{
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private TMP_Text   eroroDescription;

    [SerializeField] private TMP_Text sceneText;
    [SerializeField] private TMP_Text waveIndexText;
    [SerializeField] private TMP_Text enemiesAliveText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text livesText;

    public void SetMessage (string errorDesc, int waveIndex)
    {
        Time.timeScale = 0f;
        Debug.Log(gameObject.name + "Time scale: " + Time.timeScale);

        errorPanel.SetActive(true);
        eroroDescription.text = errorDesc;

        sceneText.text          = "Scene: " + SceneManager.GetActiveScene().name;
        waveIndexText.text      = "waveIndex: " + waveIndex;
        enemiesAliveText.text   = "enemiesAlive: " + WaveManager.enemiesAlive;
        moneyText.text          = "Money: " + PlayerStats.money;
        livesText.text          = "Lives: " + PlayerStats.lives;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void Continue ()
    {
        errorPanel.SetActive(false);

        Time.timeScale = 1f;
        Debug.Log(gameObject.name + "Time scale: " + Time.timeScale);
    }

    public void BackToMenu ()
    {
        SceneManager.LoadScene("Main menu");
    }

    public void CloseGame ()
    {
        Debug.Log("game closed");
        Application.Quit();
    }
}
