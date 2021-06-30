using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerdata : MonoBehaviour
{
    public string playername;
    public int number;

    public playerdata(string pname, int number)
    {
        this.playername = pname;
        this.number = number;
    }
}
