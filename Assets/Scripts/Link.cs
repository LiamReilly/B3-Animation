using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;






[RequireComponent(typeof(NavMeshAgent))]
public class Link : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    bool linking;
    float origSpeed;

    // just change linkspeed to alter off mesh link traverse speed;
    public float linkSpeed;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        origSpeed = agent.speed;
        linking = false;
    }

    void FixedUpdate()
    {
        if (agent.isOnOffMeshLink && linking == false)
        {
            linking = true;
            anim.SetTrigger("Jump");
            agent.speed = agent.speed * linkSpeed;
        }
        else if (agent.isOnNavMesh && linking == true)
        {
            linking = false;
            agent.velocity = Vector3.zero;
            agent.speed = origSpeed;
        }
    }
}

   
