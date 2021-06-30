using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LdrbrdUI : MonoBehaviour
{
    //public Button SIn;
    public Button LogOut;

    UserData UData;


    private void Start()
    {
        LogOut.interactable = false;
        //SIn.interactable = false;
        UData = GameObject.Find("FirebaseUserData").GetComponent<UserData>();

        if(UData.SignedIn == true)
        {
            LogOut.interactable = true;
            //SIn.interactable = false;
        }
        if (UData.SignedIn == false)
        {
            //SIn.interactable = true;
            LogOut.interactable = false;
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("main menu");
    }
    public void SignIn()
    {
        SceneManager.LoadScene("login and reg");
    }

    private void Update()
    {
        if (UData.SignedIn == true)
        {
            LogOut.interactable = true;
            //SIn.interactable = false;
        }
        if (UData.SignedIn == false)
        {
            //SIn.interactable = true;
            LogOut.interactable = false;
        }
    }
}
