using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroywall : MonoBehaviour
{
    public GameObject destwall;

    public void Dest()
    {
        Instantiate(destwall, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
