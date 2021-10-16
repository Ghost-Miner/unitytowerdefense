using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    #region References and variables
    public WaveBlueprint[] waves;

    [Header("References and variables")]
    public static int  enemiesAlive = 0;
    public static bool waveAutoStart;

    public TMP_Text   waveNumberText;
    public GameObject startButton;
    public GameObject speedButton;

    //private float spawnTimer = 0f;
    private int   waveIndex = 0;
    private bool  waveStarted = false;
    private float gameSpeed = 1f;

    private GameObject unitPlaceholder;
    [SerializeField] private Transform  spawnPoint;

    [SerializeField] private GameObject levelWonPanel;

    [Header("Wave control button")]
    [SerializeField] private Button     waveStartButton;
    [SerializeField] private Button     waveSpeedButton;
    [SerializeField] private GameObject speedImage;

    [SerializeField] private TMP_Text waveStartBtnText;
    [SerializeField] private TMP_Text waveSpeedText;

    [Header ("Debug info")]
    [SerializeField] private TMP_Text waveIndexText;
    [SerializeField] private TMP_Text wavestartedtext;
    [SerializeField] private TMP_Text enemalivetext;
    #endregion

    void Update()
    {
        waveNumberText.text = (waveIndex + 1).ToString();   // GAME STATS, Wave number ,but starts from 1.

        wavestartedtext.text = "waveStarted: " + waveStarted;   // DEBUG ONLY, displays waveStarted value.
        waveIndexText.text   = "waveIndex: "   + waveIndex;     // DEBUG ONLY, displays waveIndex number.
        enemalivetext.text   = "eneiesAlive: " + enemiesAlive;  // DEBUG ONLY, displays number of living enemies

        if (enemiesAlive > 0 || GameObject.FindWithTag("Enemy") != null)
        {
            DisableButton();
            return;
        }

        //EnableButton();
    }

    void EnableButton()
    {
        startButton.SetActive(true);
        speedButton.SetActive(false);
    }
    void DisableButton()
    {
        startButton.SetActive(false);
        speedButton.SetActive(true);
    }

    public void StartWaveButton()
    {
        if (enemiesAlive > 0 || GameObject.FindWithTag("Enemy") != null)
        {
            return;
        }

        StartWave();
    }
    
    void StartWave()
    {
        //spawnTimer = 0;

        waveStarted = true;

        startButton.SetActive(false);
        speedButton.SetActive(true);

        waveSpeedText.text = "Speed: " + gameSpeed.ToString("0") + "x";

        StartCoroutine(WaveSpawn());
    }

    // Controls speed of the game
    public void WaveSpeed()
    {
        if (Time.timeScale <= 1f)
        {
            Time.timeScale = 2f;
            gameSpeed = Time.timeScale;
            waveSpeedText.text = "Speed: " + gameSpeed.ToString("0") + "x";
            speedImage.SetActive(true);
        }
        else if (Time.timeScale >= 2f)
        {
            Time.timeScale = 1f;
            gameSpeed = Time.timeScale;
            waveSpeedText.text = "Speed: " + gameSpeed.ToString("0") + "x";
            speedImage.SetActive(false);
        }
    }

    void EndGame()
    {
        StopCoroutine(WaveSpawn());
        Debug.Log("Game won");

        levelWonPanel.SetActive(true);

        waveStartButton.interactable = false;
        startButton.SetActive(false);
        speedButton.SetActive(false);

        this.enabled = false;
    }

    void SpawnEnemy(GameObject unit)
    {
        Instantiate(unit, spawnPoint.position, spawnPoint.rotation);

        //Debug.Log("Unit method called");
    }

    IEnumerator WaveSpawn()
    {
        float typesDelay = 3f;
        float wavesDelay = 5f;

        WaveBlueprint wave = waves[waveIndex];

        if (wave.u2_prefab == null)
        {
            wave.u2_prefab = unitPlaceholder;
            wave.u2_Count = 0;
        }
        if (wave.u3_prefab == null)
        {
            wave.u3_prefab = unitPlaceholder;
            wave.u3_Count = 0;
        }
        enemiesAlive = wave.u1_Count + wave.u2_Count + wave.u3_Count; // Set total number of enemies to spawn

        for (int i = 0; i < wave.u1_Count; i++)
        {
            Instantiate(wave.u1_prefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1f / wave.u1_rate);
        }

        yield return new WaitForSeconds(typesDelay);

        for (int i = 0; i < wave.u2_Count; i++)
        {
            Instantiate(wave.u2_prefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1f / wave.u2_rate);
        }
        yield return new WaitForSeconds(typesDelay);

        if (wave.u3_prefab != null)
        {
            for (int i = 0; i < wave.u3_Count; i++)
            {
                Instantiate(wave.u3_prefab, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(1f / wave.u3_rate);
            }
        }

        //yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => GameObject.FindWithTag("Enemy") == null);
        waveIndex++;
        EnableButton();

        if (waveIndex == waves.Length)
        {
            EndGame();
        }

        if (waveAutoStart)
        {
            yield return new WaitForSeconds(wavesDelay);

            Debug.Log("WAVE AUTOSTART. " + waveIndex);
            StartWave();
        }
    }
}