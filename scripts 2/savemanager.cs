using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class savemanager : MonoBehaviour
{
    public playerdata pdata;
    public dice dice;

    DatabaseReference reference;

    //private UserData UData;

    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://athanatos.firebaseio.com/");

        reference = FirebaseDatabase.DefaultInstance.RootReference;
        dice.Submit();
        DownloadScores();

        //UData = GameObject.Find("FirebaseUserData").GetComponent<UserData>();
    }

    public void UpdateUserData(string userID, string uname, int num)
    {
        playerdata player = new playerdata(uname, num);

        string jsondata = JsonUtility.ToJson(player);

        reference.Child("users").Child(userID).SetRawJsonValueAsync(jsondata);
        Debug.Log("datasent");
    }

    public void DownloadScores()
    {
        reference.Child("users").OrderByChild("number").LimitToLast(5).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("cant download data");
                dice.DownloadFailed();
                return;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                int counter = 4;

                foreach (DataSnapshot h in snapshot.Children)
                {
                    //Debug.Log(h.GetRawJsonValue());

                    string nam = h.Child("playername").GetValue(false).ToString();
                    string num = h.Child("number").GetValue(false).ToString();

                    //Debug.Log(nam + " : " + num);

                    dice.UpdateScore(counter, nam, num);
                    counter--;
                }   
            }
        });
    }

    public void playerScores(string UserID)
    {
       
        reference.Child("users").Child(UserID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("cant download data");
                return;
                
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists == true)
                {
                    string playerhigh = snapshot.Child("number").GetValue(false).ToString();
                    dice.ReturnHigh(playerhigh);
                }
                else
                {
                    dice.ReturnHigh("0");
                }
            }
        });

    }
}
     

