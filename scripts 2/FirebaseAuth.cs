using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;

public class FirebaseAuth : MonoBehaviour
{
    private UserData UData;
    private LogorReg UIscr;

    private string nameShare;

    static FirebaseAuth instance;

    private void Start()
    {
        UData = GameObject.Find("FirebaseUserData").GetComponent<UserData>();
        UIscr = GameObject.Find("UI Handler").GetComponent<LogorReg>();
        DontDestroyOnLoad(gameObject);
        InitializeFirebase();
    }
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



    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
                UData.SignedIn = false;
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                UData.UID = user.UserId;
                UData.Uname = user.DisplayName;
                /*if(user.DisplayName == "")
                {
                    Debug.Log("yesss");
                    UData.Uname = nameShare;
                }*/
                Debug.Log("Signed in name " + user.DisplayName);
                UData.SignedIn = true;
                ChangeScene CS = GameObject.Find("SceneChanger").GetComponent<ChangeScene>();
                CS.LoadNxt();
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }





    public void Register(string email, string pass, string name)
    {
        nameShare = name;
        
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        Firebase.Auth.FirebaseUser newUser;

        auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                UIscr.Dispkey(3);
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                UIscr.Dispkey(3);
                return;
            }
            
            // Firebase user has been created.
            newUser = task.Result;
            
            //UIscr.Dispkey(4);
            
            Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
            if (newUser != null)
            {
                Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
                {
                    DisplayName = name
                };
                newUser.UpdateUserProfileAsync(profile).ContinueWith(task2 => {
                    if (task2.IsCanceled)
                    {
                        Debug.LogError("UpdateUserProfileAsync was canceled.");
                        return;
                    }
                    if (task2.IsFaulted)
                    {
                        Debug.LogError("UpdateUserProfileAsync encountered an error: " + task2.Exception);
                        return;
                    }

                    Debug.Log("User profile updated successfully.");

                    Debug.Log(name);
                    UData.Uname = name;
                    Debug.Log(UData.Uname);
                    UData.UID = newUser.UserId;
                    UData.SignedIn = true;
                });
            }
        });

        /*newUser = auth.CurrentUser;
        if (newUser != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = name
            };
            newUser.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");

                UData.Uname = newUser.DisplayName;
                UData.UID = newUser.UserId;
                UData.SignedIn = true;
            });
        }*/
    }

    public void Login(string mail, string pass)
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        Firebase.Auth.FirebaseUser newUser;

        auth.SignInWithEmailAndPasswordAsync(mail, pass).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                UIscr.Dispkey(1);
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                UIscr.Dispkey(1);
                return;
            }

            newUser = task.Result;

            UData.Uname = newUser.DisplayName;
            UData.UID = newUser.UserId;
            UData.SignedIn = true;
            //UIscr.Dispkey(2);
            Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
        });
    }

    public void SignOutFunc()
    {
        auth.SignOut();
        UData.SignedIn = false;
        UData.Uname = "Dummy";
        UData.UID = "0";
    }
}
