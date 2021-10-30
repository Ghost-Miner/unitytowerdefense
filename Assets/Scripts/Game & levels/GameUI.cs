using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject autoStartDesc;
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
    public void RestartScene ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void NextScene ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AutoStartDescShow ()
    {
        autoStartDesc.SetActive(true);
    }

    public void AutoStartDescHide()
    {
        autoStartDesc.SetActive(false);
    }
}
