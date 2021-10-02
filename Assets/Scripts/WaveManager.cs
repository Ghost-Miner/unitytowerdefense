using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    #region References and variables
    public WaveBlueprint[] waves;

    //public Transform startPoint;
    public Transform spawnPoint;

    //public TMP_Text waveCountdownText;
    public TMP_Text waveNumberText;

    [SerializeField] private Button waveStartButton;
    [SerializeField] private TMP_Text waveStartBtnText;

    public static int enemiesAlive = 0;

    //public float timeBeterrmWaves = 5.5f;
    private float spawnTimer = 0f;

    private int waveIndex = 0;
    public bool waveAutoStart;
    private bool waveStarted = false;

    public GameObject startButton;
    public GameObject speedButton;

    private bool unit2spawned = false;
    private bool unit3spawned = false;

    public TMP_Text timer;
    public TMP_Text wavestartedtext;
    public TMP_Text waveSpeedText;
    public TMP_Text enemalivetext;

    private float gameSpeed;
    #endregion

    void Start()
    {
        gameSpeed = Time.timeScale;
    }

    void Update()
    {
        wavestartedtext.text = waveStarted.ToString();      // DEBUG ONLY, displays waveStarted value.
        waveNumberText.text = (waveIndex + 1).ToString();   // GAME STATS, Wave number ,but starts from 1.
        timer.text = spawnTimer.ToString();                 // DEBUG ONLY, displays the spawn timer.
        enemalivetext.text = enemiesAlive.ToString();       // DEBUG ONLY, displays number of living enemies

        // Spawn units of type 2 and higher
        if (waveStarted)
        {
            Debug.Log(waveIndex);
            spawnTimer += Time.deltaTime;           // Spawns timer
            WaveBlueprint wave = waves[waveIndex];  // Wave blueprint array

            if (wave.u2_prefab == null)
            {
                waveStarted = false;
            }

            if (spawnTimer >= wave.u2_spawnDelay && !unit2spawned)
            {
                unit2spawned = true;
                StartCoroutine(SpawnEnemyCour_2());
                Debug.Log("unit 2 spawned");

                if (wave.u3_prefab == null)
                {
                    waveStarted = false;
                }
            }

            if (spawnTimer >= wave.u3_spawnDelay && !unit3spawned)
            {
                unit3spawned = true;
                StartCoroutine(SpawnEnemyCour_3());
                Debug.Log("unit 3 spawned");

                waveStarted = false;
            }
        }

        // Change the START WAVE button if there ar eno enemies. UNFINISHED
        if (enemiesAlive > 0 || GameObject.FindWithTag("Enemy") != null)
        {
            startButton.SetActive(false);
            speedButton.SetActive(true);

            if (waveIndex == waves.Length)
            {
                EndGame();
                Debug.Log("Game won");
            }
            else
            {
                return;
            }
        }

        EnableButton();
    }

    void EnableButton()
    {
        startButton.SetActive(true);
        speedButton.SetActive(false);
    }

    public void StartWaveButton()
    {
        if (enemiesAlive > 0 || GameObject.FindWithTag("Enemy") != null)
        {
            return;
        }

        Debug.Log("wave: : " + waveIndex);

        StartWave();

        Debug.Log("Starting wave " + (waveIndex));
    }
    
    // Controls speed of the game
    public void WaveSpeed()
    {
        if (Time.timeScale <= 1f)
        {
            Time.timeScale = 2f;
            gameSpeed = Time.timeScale;

            Debug.Log(gameSpeed);
        }
        else if (Time.timeScale >= 2f)
        {
            Time.timeScale = 1f;
            gameSpeed = Time.timeScale;

            Debug.Log(gameSpeed);
        }
    }

    void StartWave()
    {
        spawnTimer = 0;

        waveStarted = true;
        unit2spawned = false;
        unit3spawned = false;

        startButton.SetActive(false);
        speedButton.SetActive(true);

        StartCoroutine(SpawnEnemyCour_1());
    }

    void EndGame()
    {
        Debug.Log("Game won");
        this.enabled = false;
    }

    IEnumerator SpawnEnemyCour_1 ()
    {
        WaveBlueprint wave = waves[waveIndex];
        enemiesAlive = wave.u1_Count + wave.u2_Count + wave.u3_Count; // Set total number of enemies to spawn

        for (int i = 0; i < wave.u1_Count; i++)
        {
            SpawnEnemy(wave.u1_prefab);
            yield return new WaitForSeconds(1f / wave.u1_rate);
        }

        if (wave.u2_prefab == null)
        {
            waveIndex++;
            Debug.Log("u1 Wave: " + waveIndex);

            if (waveIndex == waves.Length)
            {
                Invoke("EndGame", 5f);
                Debug.Log("Game won");
            }
        }
    }
    IEnumerator SpawnEnemyCour_2 ()
    {
        WaveBlueprint wave = waves[waveIndex];

        for (int i = 0; i < wave.u2_Count; i++)
        {
                SpawnEnemy(wave.u2_prefab);
                yield return new WaitForSeconds(1f / wave.u2_rate);
        }

        if (wave.u3_prefab == null)
        {
            waveIndex++;
            Debug.Log("u2 Wave: " + waveIndex);

            if (waveIndex == waves.Length)
            {
                Invoke("EndGame", 5f);
                Debug.Log("Game won");
            }
        }
    }
    IEnumerator SpawnEnemyCour_3 ()
    {
        WaveBlueprint wave = waves[waveIndex];

        if (wave.u3_prefab != null)
        {
            for (int i = 0; i < wave.u3_Count; i++)
            {
                SpawnEnemy(wave.u3_prefab);
                yield return new WaitForSeconds(1f / wave.u3_rate);
            }
        }

        if (wave.u3_prefab != null)
        {
            waveIndex++;
            Debug.Log("u3 Wave: " + waveIndex);

            if (waveIndex == waves.Length)
            {
                Invoke("EndGame", 5f);
                Debug.Log("Game won");
            }
        }
    }

    void SpawnEnemy(GameObject unit)
    {
        Instantiate(unit, spawnPoint.position, spawnPoint.rotation);

        //Debug.Log("Unit method called");
    }
}