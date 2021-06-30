using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;

public class LogOut : MonoBehaviour
{
    private UserData UData;
    private FirebaseAuth FBAuth;

    private void Start()
    {
        UData = GameObject.Find("FirebaseUserData").GetComponent<UserData>();
        FBAuth = GameObject.Find("FirebaseAuth").GetComponent<FirebaseAuth>();
    }

    public void SignOutFunc()
    {
        FBAuth.SignOutFunc();
    }
}
