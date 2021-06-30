using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascr : MonoBehaviour
{
    public GameObject player;
    Vector3 target;
    public Vector3 offset;
    public float smoothT;
    Vector3 vel = Vector3.zero;

    void LateUpdate()
    {
        target = player.transform.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, target, ref vel, smoothT);
    }
}
