using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScr : MonoBehaviour
{
    public InputField MailInp;
    public InputField PassInp;

    public FirebaseAuth AuthScr;

    public void Login()
    {
        string mail = MailInp.text;
        string pass = PassInp.text;

        AuthScr.Login(mail, pass);
    }
}
