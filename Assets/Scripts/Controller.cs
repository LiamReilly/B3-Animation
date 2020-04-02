﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody rb;
    public FootBox feet;
    private SetTarget AutoMan;
    
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    Vector3 GroundPoint;
    
    ObjectSelection Selectionmanager;
    Animator anim;

    public float Speed;
    public Camera cam;
    
    bool shouldTurn;
    bool jumping;
    private bool move;
    private float xMax = 0.5f, xMin = -0.5f;
    Vector3 CharSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Selectionmanager = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<ObjectSelection>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //feet = GetComponent<FootBox>();
        feet.setOT(() => jumping = false);
        AutoMan = GameObject.Find("AgentCreator").gameObject.transform.GetChild(0).GetComponent<SetTarget>();

    }


    //changed to add force instead of translate didn't help
    /*private void FixedUpdate()
    {
        if (!cam.isActiveAndEnabled)
        {
            float vert = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
            float horz = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
            //Debug.Log(horz);
            //Debug.Log(vert);
            if (!shouldTurn && !jumping)
            {
                rb.velocity = new Vector3 (horz, 0f, vert);
                Debug.Log(rb.velocity);
            }
            //velocity = rb.velocity+ new Vector3 (0.1f, 0f, 0f);
            
            anim.SetFloat("velocityx", horz);
            anim.SetFloat("velocityy", vert);
            if (horz != 0 || vert != 0)
            {
                move = true;
            }
            else
            {
                move = false;
            }

            if (vert < 0) anim.speed = 2;
            else anim.speed = 1;
            //Debug.Log(velocity.x);

            anim.SetBool("Move", move);
            if (Input.GetKey(KeyCode.Space) && !jumping)
            {
                anim.SetTrigger("Jump");
                jumping = true;
                StartCoroutine(wait3Seconds());
            }
            if (Input.GetKey(KeyCode.Delete))
            {
                anim.SetTrigger("Die");
            }
            if (Input.GetKey(KeyCode.Q))
            {
                shouldTurn = true;
                anim.SetBool("shouldturn", shouldTurn);
                anim.SetFloat("turn", -1f);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                shouldTurn = true;
                anim.SetBool("shouldturn", shouldTurn);
                anim.SetFloat("turn", 1f);
            }
            else
            {
                shouldTurn = false;
                anim.SetBool("shouldturn", shouldTurn);
            }
            
            if (!jumping)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
                Debug.DrawLine(transform.position, Vector2.down, Color.magenta);
                if (hit.collider != null)
                {
                    Debug.Log("hit collider");
                    //float distance = Mathf.Abs(hit.point.y - transform.position.y);
                    //gameObject.transform.Translate(0f, distance, 0f);
                    gameObject.transform.position = hit.point;
                }
            }
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        if (!cam.isActiveAndEnabled)
        {
            float vert = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
            float horz = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
            //Debug.Log(horz);
            //Debug.Log(vert);
            //velocity = rb.velocity;
            //Debug.Log(rb.velocity);
            if (AutoMan.walkingTell())
            {
                //Debug.Log(AutoMan.walkingTell());
                anim.SetFloat("velocityx", Mathf.Clamp(horz * Speed, xMin, xMax));
                anim.SetFloat("velocityy", Mathf.Clamp(vert * Speed, xMin, xMax));
            }
            else
            {
                anim.SetFloat("velocityx", horz * Speed);
                anim.SetFloat("velocityy", vert * Speed);
            }
            
            if (horz != 0 || vert != 0)
            {
                move = true;
            }
            else
            {
                move = false;
            }

            //if (vert <= 1) anim.speed = -1;
            //else anim.speed = 1;
            //Debug.Log(velocity.x);
            
            anim.SetBool("Move", move);
            if (Input.GetKey(KeyCode.Space) && !jumping)
            {
                anim.SetTrigger("Jump");
                jumping = true;
                StartCoroutine(wait3Seconds());
            }
            if (Input.GetKey(KeyCode.Delete))
            {
                anim.SetTrigger("Die");
            }
            if (Input.GetKey(KeyCode.Q)&&!move)
            {
                shouldTurn = true;
                anim.SetBool("shouldturn", shouldTurn);
                anim.SetFloat("turn", -1f);
            } else if (Input.GetKey(KeyCode.E)&&!move)
            {
                shouldTurn = true;
                anim.SetBool("shouldturn", shouldTurn);
                anim.SetFloat("turn", 1f);
            } else
            {
                shouldTurn = false;
                anim.SetBool("shouldturn", shouldTurn);
            } 
            if (!shouldTurn || !jumping)
            {
                if (AutoMan.walkingTell())
                {
                    CharSpeed = new Vector3 (horz * 0.7f, 0, vert * 0.7f);
                    //CharSpeed.Normalize();
                    //Debug.Log(CharSpeed);
                    gameObject.transform.Translate(CharSpeed);
                    
                }
                else
                {
                    CharSpeed = new Vector3(horz, 0f, vert);
                    //CharSpeed.Normalize();
                    gameObject.transform.Translate(CharSpeed);
                    
                }
                
            }
            /*if (!jumping)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
                Debug.DrawLine(transform.position, Vector2.down, Color.magenta);
                if (hit.collider != null)
                {
                    Debug.Log("hit collider");
                    //float distance = Mathf.Abs(hit.point.y - transform.position.y);
                    //gameObject.transform.Translate(0f, distance, 0f);
                    gameObject.transform.position = hit.point;
                }
            }*/
            
        }
        
        

    }
    IEnumerator wait3Seconds()
    {
        yield return new WaitForSeconds(1.5f);
        jumping = false;
    }
}
