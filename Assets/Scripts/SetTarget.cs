using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetTarget : MonoBehaviour
{
    NavMeshAgent agentFab;
    Rigidbody rb;
    private bool immobile;
    private const float AGENT_AREA = 0.25f;
    private const float THRESHOLD = 0.3f;
    private const float BUMP_STEP = 1.005f;
    private float bumpCount = 1;
    private float currDistSqr = 100;

    ObjectSelection Selectionmanager;

    // Start is called before the first frame update
    void Start()
    {
        agentFab = GetComponent<NavMeshAgent>();
        Selectionmanager = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<ObjectSelection>();
		//rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currDistSqr = (gameObject.transform.position - agentFab.destination).sqrMagnitude;

        //if(!immobile)print(gameObject.name+": " +currDistSqr);
        if (!immobile && currDistSqr < THRESHOLD)
        {
            setImmobile(true);
            bumpCount = 1;
        }

        if(immobile){
            SetDestination(transform.position);
        }
    }

    void OnTriggerStay(Collider other)
    {

        if (!immobile && other.gameObject.CompareTag("Agent") && other.gameObject.GetComponent<SetTarget>().getImmobile())
        {
            float recommendedDistSqr = Selectionmanager.GroupAgents.Count * bumpCount * AGENT_AREA/2 / Mathf.PI;
            bumpCount *= BUMP_STEP;
            currDistSqr = (agentFab.destination - gameObject.transform.position).sqrMagnitude;
            //print("ReccDistSqr: " + recommendedDistSqr.ToString() + ", CurrDistSqr: "+ currDistSqr.ToString());
            if (currDistSqr < recommendedDistSqr)
            {
                setImmobile(true);
                bumpCount = 1;
            }
        }
    }

    public void SetDestination(Vector3 destination)
    {
        //print("Made agent " + gameObject.name +" destination: " + destination.ToString());
        agentFab.SetDestination(destination);
    }

    public bool getImmobile()
    {
        return immobile;
    }

    public void setImmobile(bool immobile)
    {
        this.immobile = immobile;
        agentFab.isStopped = immobile;
        SetDestination(gameObject.transform.position);
        //print("Made agent " + gameObject.name +" immobile" + immobile);
    }
	
	public void pushBack(Vector3 direction)
    {
        //rb.AddForce(direction);
        gameObject.transform.Translate(direction);
    }


}





