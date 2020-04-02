using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
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
    Animator anim;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    private float xMin = -0.5f, xMax = 0.5f;
    private bool walking = true;
    private GameObject Speedstate;

    ObjectSelection Selectionmanager;

    // Start is called before the first frame update
    void Awake()
    {
        agentFab = GetComponent<NavMeshAgent>();
        Selectionmanager = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<ObjectSelection>();
        //rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agentFab.updatePosition = false;
        Speedstate = GameObject.Find("SpeedState");
        

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            walking = true;
            Speedstate.GetComponent<Text>().text = "Walking";
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            walking = false;
            Speedstate.GetComponent<Text>().text = "Running";
        }
        Vector3 worldDeltaPosition = agentFab.nextPosition - transform.position;
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;
        bool shouldMove = velocity.magnitude > 0.5f && agentFab.remainingDistance > agentFab.radius;
        anim.SetBool("Move", shouldMove);
        if (walking)
        {
            anim.SetFloat("velocityx", Mathf.Clamp(velocity.x, xMin, xMax));
            anim.SetFloat("velocityy", Mathf.Clamp(velocity.y, xMin, xMax));
           
        }
        else
        {
            anim.SetFloat("velocityx", velocity.x);
            anim.SetFloat("velocityy", velocity.y);
            
        }
        
        GetComponent<LookAt>().lookAtTargetPosition = agentFab.steeringTarget + transform.forward;
    }

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
    void OnAnimatorMove()
    {
        // Update position to agent position
        transform.position = agentFab.nextPosition;
    }
    public bool walkingTell()
    {
        return walking;
    }
}





