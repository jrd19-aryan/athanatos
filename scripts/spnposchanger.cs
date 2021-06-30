using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spnposchanger : MonoBehaviour
{
    public Vector3 newspawn;
    int done;

    private void Start()
    {
        done = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player") && done!=1)
        {
            other.GetComponent<playermovement>().spawnpos = newspawn;
            done = 1;
        }
    }
}
