using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveTo : MonoBehaviour {

    public Transform goal;
    NavMeshAgent agent;
       
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        
    }

    void FixedUpdate()
    {
        agent.destination = goal.position; 
    }
}