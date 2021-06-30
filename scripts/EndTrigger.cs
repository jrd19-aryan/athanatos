using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    public GameObject LevelCompleteUI;
    public GameObject scoredisp;
    public GameObject player;
    public GameObject cratecanvas;

    private ScoreManagement sm;

    private void Start()
    {
        sm = GameObject.Find("ScoreManagement").GetComponent<ScoreManagement>();
        LevelCompleteUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player"))
        {
            //Debug.Log("works");
            CompleteLevel();
        }
    }
    public void CompleteLevel()
    {
        int cc = player.GetComponent<playermovement>().collectcount;
        int td = player.GetComponent<playermovement>().timesdied;
        float score = 1000 * cc - 1000*(td - 5);

        sm.currCrates = cc;
        sm.timesplayerdied = td;
        sm.currscore = score;

        if (SceneManager.GetActiveScene().name == "test scene 3")
        {
            sm.GameComplete();
        }

        scoredisp.GetComponent<TextMeshProUGUI>().text = score.ToString();

        Debug.Log("Level Won !!!");
        LevelCompleteUI.SetActive(true);
        cratecanvas.SetActive(false);
        Time.timeScale = 0f;
    }

    public void BackToMenu()
    {
        Debug.Log("Returning to Menu !!!");
        Time.timeScale = 1f;
        SceneManager.LoadScene("main menu");
    }

    /*
    public Animator transition;
    public float TransitionTime = 1f;
    public void LoadScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(levelIndex);
    }
    */
}
