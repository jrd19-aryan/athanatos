using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!gameObject.GetComponent<AudioManager>().isSoundPlaying("mainmusic"))
        {
            gameObject.GetComponent<AudioManager>().Play("mainmusic");
        }
        //gameObject.GetComponent<AudioManager>().Play("mainmusic");
    }

    
}
