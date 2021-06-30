using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogorReg : MonoBehaviour
{
    public GameObject LoginPanel;
    public GameObject RegisterPanel;
    public GameObject SelectScreen;

    public TextMeshProUGUI loginmessage;
    public TextMeshProUGUI regismessage;

    public Canvas canv;

    private int updatekey;

    public void GotoLogin()
    {
        LoginPanel.SetActive(true);
        SelectScreen.SetActive(false);
    }

    public void GotoRegister()
    {
        RegisterPanel.SetActive(true);
        SelectScreen.SetActive(false);
    }

    public void BackFromReg()
    {
        SelectScreen.SetActive(true);
        RegisterPanel.SetActive(false);
    }

    public void BackFromLog()
    {
        SelectScreen.SetActive(true);
        LoginPanel.SetActive(false);
    }

    public void LoginMessage(int key)
    {
        Debug.Log("called");
        if(key == 0)
        {
            loginmessage.text = "Login Failed";
            updatekey = 0;
        }
        if (key == 1)
        {
            loginmessage.text = "Login Succesfull";
            updatekey = 0;
        }
    }

    public void RegMessage(int key)
    {
        Debug.Log("called");
        if (key == 0)
        {
            regismessage.text = "Registration Failed";
            updatekey = 0;
        }
        if (key == 1)
        {
            regismessage.text = "Registration Succesfull";
            updatekey = 0;
        }
    }

    public void Dispkey(int key)
    {
        updatekey = key;
    }

    private void Update()
    {
        if(updatekey == 1)
        {
            LoginMessage(0);
        }
        if (updatekey == 2)
        {
            LoginMessage(1);
        }
        if (updatekey == 3)
        {
            RegMessage(0);
        }
        if (updatekey == 4)
        {
            RegMessage(1);
        }
    }
}
