using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dice : MonoBehaviour
{
    public InputField nameinput;
    public InputField numberinput;
    public InputField IDinput;

    public GameObject[] displays;

    string[] dataarray;

    string playername;
    int number;
    string userID;

    public savemanager sm;

    float t1;
    bool callagain;

    int displayed;

    private UserData UData;

    ScoreManagement scoreman;

    public void Submit()
    {
        if(UData.SignedIn == false)
        {
            Debug.Log("Logged Out, Cant Upload Data");
            return;
        }
        if(scoreman.localhighscore > 0)
        {
            playername = UData.Uname;
            userID = UData.UID;
            number = (int)(scoreman.localhighscore);

            sm.playerScores(userID);
        }
    }

    public void ReturnHigh(string high)
    {
        int hScore = int.Parse(high);
        if(hScore<scoreman.localhighscore)
        {
            playername = UData.Uname;
            userID = UData.UID;
            number = (int)(scoreman.localhighscore);

            sm.UpdateUserData(userID, playername, number);
        }
        if(hScore > scoreman.localhighscore)
        {
            scoreman.localhighscore = hScore;
        }
    }

    public void CallUpdate()
    {
        Debug.Log("downloading");
        sm.DownloadScores();
    }

    public void UpdateScore(int rank, string nam, string num)
    {
        //Debug.Log(rank + " : " + nam + " : " + num);
        dataarray[2 * rank] = nam;
        dataarray[2 * rank + 1] = num;
        displayed = 0;
    }

    public void DownloadFailed()
    {
        t1 = Time.time;
        callagain = true;
    }

    private void DisplayScores()
    {
        for (int i=0;i<5;i++)
        {
            //Debug.Log(dataarray[2 * i] + " : " + dataarray[2 * i + 1]);
            displays[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = dataarray[i*2];
            displays[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = dataarray[i * 2 + 1];
        }
    }

    private void Start()
    {
        displayed = 1;
        dataarray = new string[10];
        UData = GameObject.Find("FirebaseUserData").GetComponent<UserData>();
        scoreman = GameObject.Find("ScoreManagement").GetComponent<ScoreManagement>();
        //Submit();
    }

    private void Update()
    {
        if(displayed == 0)
        {
            DisplayScores();
            displayed = 1;
        }

    }
}
