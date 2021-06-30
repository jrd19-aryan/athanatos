using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enablecount : MonoBehaviour
{
    public GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Canvas.SetActive(true);
        }   else 
        {
            DisableCountUI();
        }

    }

    public void DisableCountUI()
    {
        Canvas.SetActive(false);
    }
}
