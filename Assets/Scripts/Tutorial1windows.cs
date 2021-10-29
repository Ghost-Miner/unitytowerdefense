using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1windows : MonoBehaviour
{
    #region vars
    [SerializeField] private GameObject background;

    [SerializeField] private GameObject[] tutMessage;
    [SerializeField] private GameObject[] enemyTypeMessage;

    private int tutNumber = 0;
    #endregion

    private void Start()
    {
        tutMessage[0].SetActive(true);
        background.SetActive(true);
    }

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

    public void Continue (int EnemywindowsIndex)
    {
        enemyTypeMessage[EnemywindowsIndex].SetActive(false);
        background.SetActive(false);

        Time.timeScale = 1f;
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
            return;
        }

        tutMessage[tutNumber].SetActive(true);

        Time.timeScale = 1f;
    }
}
