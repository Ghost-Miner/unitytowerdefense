using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{

    //----------------------------------
    // THIS WHOLE SYSTEM IS ONE BIG MESS WHICH DOESNT EVEN WORK PROPERLY
    // I have no idea what I'm doing
    // Please send help
    //-------------------------------


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

    bool unit2spawned;
    bool unit3spawned;

    public TMP_Text timer;
    public TMP_Text wavestartedtext;
    public TMP_Text waveSpeedText;

    private float gameSpeed;
    #endregion

    void Start()
    {
        waveStarted = false;
        unit2spawned = false;
        unit3spawned = false;

        gameSpeed = Time.timeScale;
    }

    void Update()
    {
        wavestartedtext.text = waveStarted.ToString(); // debug text
        waveNumberText.text = (waveIndex + 1).ToString(); // wave numer 
        timer.text = spawnTimer.ToString();

        if (waveStarted) 
        {
            spawnTimer += Time.deltaTime;
            WaveBlueprint wave = waves[waveIndex];
            
            if (wave.u2_prefab == null)
            {
                waveStarted = false;
                return;
            }

            if (spawnTimer >= wave.u2_spawnDelay && !unit2spawned)
            {
                unit2spawned = true;
                StartCoroutine(SpawnEnemyCour_2());
                Debug.Log("unit 2 spawned");

                if (wave.u3_prefab == null)
                {
                    waveStarted = false;
                    Debug.Log("unit3spawned 3 null, wave stopped");
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

        if (enemiesAlive > 0 || GameObject.FindWithTag("Enemy") != null)
        {
            startButton.SetActive(false);
            speedButton.SetActive(true);

            //waveStartButton.interactable = false;

            if (waveIndex == waves.Length)
            {
                Invoke("EndGame", 5f);
                Debug.Log("Game won");
            }
            else
            {
                //Debug.Log("WaveManager | enemies " + enemiesAlive);
                return;
            }
        }

        EnableButton();
    }

    void EnableButton()
    {
        startButton.SetActive(true);
        speedButton.SetActive(false);

        //waveStartButton.interactable = true;
    }

    public void StartWaveButton()
    {
        if (enemiesAlive > 0)
        {
            return;
        }

        StartWave();

        startButton.SetActive(false);
        speedButton.SetActive(true);

        Debug.Log("Starting wave " + (waveIndex));
    }

    public void WaveSpeed ()
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

    void StartWave ()
    {
        waveStarted = true;
        unit2spawned = false;
        unit3spawned = false;

        StartCoroutine(SpawnEnemyCour_1());
    }

    IEnumerator SpawnEnemyCour_1()
    {
        WaveBlueprint wave = waves[waveIndex];
        //enemiesAlive = wave.unityCount;
        enemiesAlive = wave.u1_Count + wave.u2_Count + wave.u3_Count;

        for (int i = 0; i < wave.u1_Count; i++)
        {
            SpawnEnemy(wave.u1_prefab);
            yield return new WaitForSeconds(1f / wave.u1_rate);
        }

        if (wave.u2_prefab == null && wave.u3_prefab == null)
        {
            waveIndex++;
            Debug.Log("Wave: " + waveIndex);

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

        if (wave.u2_prefab != null)
        {
            for (int i = 0; i < wave.u2_Count; i++)
            {
                SpawnEnemy(wave.u2_prefab);
                yield return new WaitForSeconds(1f / wave.u2_rate);
            }
        }

        if (wave.u2_prefab != null && wave.u3_prefab == null)
        {
            waveIndex++;
            Debug.Log("Wave: " + waveIndex);

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

        if (wave.u2_prefab == null && wave.u3_prefab != null)
        {
            //waveIndex++;
            Debug.Log("Wave: " + waveIndex);

            if (waveIndex == waves.Length)
            {
                Invoke("EndGame", 5f);
                Debug.Log("Game won");
            }
        }
    }

    void SpawnEnemy (GameObject unit)
    {
        Instantiate(unit, spawnPoint.position, spawnPoint.rotation);

        //Debug.Log("Unit method called");
    }

    void EndGame()
    {
        Debug.Log("Game won");
        this.enabled = false;
    }
}
