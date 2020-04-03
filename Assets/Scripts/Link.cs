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
    bool jump = false;

    // just change linkspeed to alter off mesh link traverse speed;
    public float linkSpeed;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        origSpeed = agent.speed;
        linking = false;
    }

    void Update()
    {
        if (agent.isOnOffMeshLink)
        {
            if (!jump)
            {
                anim.Play("Jump", 0, 0.25f);
                jump = true;
                //anim.speed = 1.5f;
            }
            StartCoroutine(wait3Seconds());
        }
        if (agent.isOnOffMeshLink && linking == false)
        {
            linking = true;
            
            agent.speed = agent.speed * linkSpeed;
        }
        else if (agent.isOnNavMesh && linking == true)
        {
            linking = false;
            agent.velocity = Vector3.zero;
            agent.speed = origSpeed;
            
        }
    }
    IEnumerator wait3Seconds()
    {
        yield return new WaitForSeconds(1f);
        //anim.SetBool("gonnaJump", false);
        jump = false;
        anim.speed = 1;
    }

}

   
