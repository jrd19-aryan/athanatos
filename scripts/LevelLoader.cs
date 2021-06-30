using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float TransitionTime = 0f;
    public void LoadScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Time.timeScale = 1f;
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);
        
        SceneManager.LoadScene(levelIndex);
    }
}
