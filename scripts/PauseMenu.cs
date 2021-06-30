
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false, IsOn = false;
    public GameObject PauseMenuUI, CountValueUI, MapZoomUI;
    public string menuscene, gamescene;

    void Start()
    {
        PauseMenuUI.SetActive(false);
        CountValueUI.SetActive(false);
        MapZoomUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
                Debug.Log("Resumed!!");
            }   else
            {
                Pause();
                Debug.Log("Pause!!");
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        CountValueUI.SetActive(true);
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        CountValueUI.SetActive(false);
    }

    public void Restart()
    {
        Debug.Log("Restarting Game !!!");
        SceneManager.LoadScene(gamescene);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game !!!");
        SceneManager.LoadScene(menuscene);
        Time.timeScale = 1f;
    }

    public void MiniMapZoom()
    {
        if (IsOn)
        {
            Debug.Log("Map Zoom deactivated !!!");
            MapZoomUI.SetActive(false);
            IsOn = false;
            CountValueUI.SetActive(true);
            Time.timeScale = 1f;
        }   else
        {
            Debug.Log("Map Zoom Activated !!!");
            MapZoomUI.SetActive(true);
            IsOn = true;
            CountValueUI.SetActive(false);
            Time.timeScale = 0.25f;
        }
    }
}
