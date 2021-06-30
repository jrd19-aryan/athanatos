using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemymove1 : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent agent;

    public GameObject deadbody;
    public GameObject collectible;

    public float activedist;

    float distFromPlayer;

    public void kill()
    {
        Instantiate(deadbody, transform.position, transform.rotation);
        Instantiate(collectible, transform.position, transform.rotation);
        Destroy(gameObject);
        
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
