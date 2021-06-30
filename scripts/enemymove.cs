using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemymove : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;

    public GameObject deadbody;

    public float activedist;

    float distFromPlayer;

    public void kill()
    {
        Instantiate(deadbody, transform.position, transform.rotation);
        Destroy(gameObject);
        
    }

    private void Start()
    {
        SettingsScr settingSet = GameObject.Find("Settings").GetComponent<SettingsScr>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed += 3 * settingSet.GameSpeed;
    }

    void Update()
    {
        distFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distFromPlayer < activedist)
        {
            agent.SetDestination(player.transform.position);
        }
        else if(distFromPlayer >= activedist)
        {
            agent.SetDestination(transform.position);
        }
    }
}
