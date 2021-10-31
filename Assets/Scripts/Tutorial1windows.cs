using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial1windows : MonoBehaviour
{
    #region vars
    public static bool waveStart;

    [SerializeField] private GameObject background;
    [SerializeField] private Button     startWaveButton;
    [SerializeField] private Button     waveSpeedButton;

    [SerializeField] private GameObject[] tutMessage;
    [SerializeField] private GameObject[] enemyTypeMessage;

    private int tutNumber = 0;
    private int enemyTypeNumber = 0;
    #endregion

    private void Start()
    {
        waveStart = false;

        startWaveButton.interactable = false;

        tutMessage[0].SetActive(true);
        background.SetActive(true);

        StartCoroutine(EnemyInfo());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            waveStart = true;
        }
    }

    #region Intro
    public void NextButton ()
    {
        Time.timeScale = 0f;

        tutNumber++;
        SwitchPanels();
    }

    public void BackButton ()
    {
        Time.timeScale = 0f;

        if (tutNumber <= 0)
        {
            Debug.LogWarning("tutorial message number is 0 or lower");
            return;
        }

        tutNumber--;
        SwitchPanels();
    }

    void SwitchPanels ()
    {
        for (int i = 0; i < tutMessage.Length; i++)
        {
            tutMessage[i].SetActive(false);
        }

        if (tutNumber == tutMessage.Length)
        {
            background.SetActive(false);
            startWaveButton.interactable = true;
            waveSpeedButton.interactable = true;

            return;
        }

        tutMessage[tutNumber].SetActive(true);

        Time.timeScale = 1f;
    }

    public void SkipTutorial ()
    {
        for (int i = 0; i < tutMessage.Length; i++)
        {
            tutMessage[i].SetActive(false);
        }

        background.SetActive(false);

        startWaveButton.interactable = true;
        waveSpeedButton.interactable = true;

        Time.timeScale = 1f;
    }
    #endregion

    #region Enemy types
    public void Continue (int EnemywindowsIndex)
    {
        background.SetActive(false);
        waveStart = false;
        
        for (int i = 0; i < enemyTypeMessage.Length; i++)
        {
            enemyTypeMessage[i].SetActive(false);
        }

        StartCoroutine(EnemyInfo());

        startWaveButton.interactable = true;
        waveSpeedButton.interactable = true;
        Time.timeScale = 1f;
    }

    void DisplayEnemyType ()
    {
        if (enemyTypeNumber >= enemyTypeMessage.Length)
        {
            Debug.Log("full enemy");
            return;
        }

        background.SetActive(true);

        Time.timeScale = 0f;
        
        for (int i = 0; i < enemyTypeMessage.Length; i++)
        {
            enemyTypeMessage[i].SetActive(false);
        }

        startWaveButton.interactable = false;
        waveSpeedButton.interactable = false;
        enemyTypeMessage[enemyTypeNumber].SetActive(true);

        enemyTypeNumber++;
    }

    IEnumerator EnemyInfo ()
    {
        Debug.Log("START");
        yield return new WaitUntil(() => waveStart);

        DisplayEnemyType();
        Debug.Log("END");
    }
    #endregion
}
