using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastsceneaudio : MonoBehaviour
{
    public float timeThreshold;
    float timer;
    bool tostart;
    GameObject AudioMan;
    // Start is called before the first frame update
    void Start()
    {
        AudioMan = GameObject.Find("AudioManager");
        tostart = false;
        AudioMan.GetComponent<AudioManager>().PauseSound("mainmusic");
    }

    
    void Update()
    {
        if(tostart)
        {
            timer += Time.deltaTime;
        }
        
        if(timer > timeThreshold && tostart)
        {
            AudioMan.GetComponent<AudioManager>().Play("explosion");
            tostart = false;
        }
    }

    public void starttimer()
    {
        tostart = true;
    }
    public void StartMainTheme()
    {
        AudioMan.GetComponent<AudioManager>().Play("mainmusic");
    }
}
