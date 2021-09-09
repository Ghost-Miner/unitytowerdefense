using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    #region References and variables
    public Wave[] waves;

    //public Transform startPoint;
    public Transform spawnPoint;

    public TMP_Text waveCountdownText;
    public TMP_Text waveNumberText;

    [SerializeField] private Button   waveStartButton;
    [SerializeField] private TMP_Text waveStartBtnText;

    public static int enemiesAlive = 0;

    public float  timeBeterrmWaves = 5.5f;
    private float countdown = 2f;
    
    private int   waveIndex = 0;
    public bool waveAutoStart;
    #endregion 

    private void Start()
    {
        waveStartBtnText.text = "START WAVE";
    }

    private void Update()
    {
        if (enemiesAlive > 0 || GameObject.FindWithTag("Enemy") != null)
        {
            waveStartButton.interactable = false;
            //Debug.Log("WaveSpawner | enemies " + enemiesAlive);
            return;
        }

        if (waveAutoStart)
        {
            //StartCoroutine(SpawnWave());
        }

        EnableButton();

        #region disabled countdown
        /*if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBeterrmWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);*/

        //waveCountdownText.text = "Next in " + string.Format("{0:00.0}", countdown);
        #endregion
        waveNumberText.text = (waveIndex + 1).ToString();
    }

    void EnableButton ()
    {
        waveStartButton.interactable = true;
    }

    public void StartWaveButton ()
    {
        if (enemiesAlive > 0)
        {
            return;
        }

        //StartCoroutine(SpawnWave());
        waveStartButton.interactable = false;

        //StartCoroutine(SpawnWave());
        Debug.Log("Starting wave " + (waveIndex + 1));
    }
        
    /*IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];
        enemiesAlive = wave.enemyCount;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy1, wave.enemy2, wave.enemy3);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
        Debug.Log("wave ended");

        if (waveIndex == waves.Length && enemiesAlive <= 0)
        {
            Invoke("EndGame", 5f);
            waveStartButton.interactable = false;
        }
    }

    void SpawnEnemy(GameObject enemy1, GameObject enemy2, GameObject enemy3)
    {
        Instantiate(enemy1, spawnPoint.position, spawnPoint.rotation);

        //enemiesAlive++;
        //Debug.Log("Enemies alive: " + enemiesAlive);

        if (enemy2 != null)
        {
            Instantiate(enemy2, spawnPoint.position, spawnPoint.rotation);
            //enemiesAlive++;
            Debug.Log("Enemies alive: " + enemiesAlive);
        }
        
        if (enemy3 != null)
        {
            Instantiate(enemy3, spawnPoint.position, spawnPoint.rotation);
            //enemiesAlive++;
            Debug.Log("Enemies alive: " + enemiesAlive);
        }
    }*/

    void EndGame ()
    {
        Debug.Log("Game won");
        this.enabled = false;
    }
}
