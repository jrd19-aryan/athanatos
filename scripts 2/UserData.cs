using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public string email;
    public string Uname;
    public string UID;
    public bool SignedIn;

    public static UserData instance;

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

    

    public void UpdateData(string newMail, string NewName, string NewUID, bool Si)
    {
        email = newMail;
        Uname = NewName;
        UID = NewUID;
        SignedIn = Si;
    }
}
