using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        agent.destination = playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = playerTransform.position;
    }
    
    
}
