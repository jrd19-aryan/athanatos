using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Unity.Editor;

public class firebase : MonoBehaviour
{

    private void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
                InitializeFirebase();
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });

        //InitializeFirebase();
    }


    void InitializeFirebase()
    {
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLogin);

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://athanatos.firebaseio.com/");
    }
}
