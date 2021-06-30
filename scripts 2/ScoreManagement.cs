using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagement : MonoBehaviour
{
    public float currscore;
    public float localhighscore;

    public int lvl1StartCrates;
    public int currCrates;

    public int timesplayerdied;

    public static ScoreManagement instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        localhighscore = PlayerPrefs.GetFloat("highscore",0);
        lvl1StartCrates = 5;
        timesplayerdied = 0;
    }

    public void GameComplete()
    {
        if (currscore>localhighscore)
        {
            localhighscore = currscore;
            PlayerPrefs.SetFloat("highscore", localhighscore);
        }
    }

}
