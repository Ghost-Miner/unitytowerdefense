using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator animator;

    private float animLenght = 1f;

    public void ChangeScene (string sceneName)
    {
        Time.timeScale = 1f;
        StartCoroutine(ChangeSceneCour(sceneName));
    }

    IEnumerator ChangeSceneCour (string sceneName)
    {
        animator.SetTrigger("ScEnd");

        yield return new WaitForSeconds(animLenght);
        
        if (sceneName == null)
        {
            Debug.LogError(gameObject.name + "| scneName is null!");
        }
        SceneManager.LoadScene(sceneName);
    }
}
