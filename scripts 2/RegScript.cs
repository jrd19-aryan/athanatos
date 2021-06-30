using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegScript : MonoBehaviour
{
    public InputField MailInp;
    public InputField NameInp;
    public InputField PassInp;

    public FirebaseAuth AuthScr;

    public void Register()
    {
        string mail = MailInp.text;
        string name = NameInp.text;
        string pass = PassInp.text;

        //Debug.Log("here");

        AuthScr.Register(mail, pass, name);
    }
}
