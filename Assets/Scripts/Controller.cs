using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody rb;
    private bool move;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    ObjectSelection Selectionmanager;
    Animator anim;
    public float Speed;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        Selectionmanager = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<ObjectSelection>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!cam.isActiveAndEnabled)
        {
            float vert = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
            float horz = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
            //Debug.Log(horz);
            //Debug.Log(vert);
            gameObject.transform.Translate(horz, 0, vert);
            velocity = rb.velocity;
            anim.SetFloat("velocityx", horz * Speed);
            anim.SetFloat("velocityy", vert * Speed);
            if (horz != 0 || vert != 0)
            {
                move = true;
            }
            else
            {
                move = false;
            }
            /*if (horz < 0) anim.speed = 2;
            else anim.speed = 1;*/
            //Debug.Log(velocity.x);
            anim.SetBool("Move", move);
            if (Input.GetKey(KeyCode.Space))
            {
                anim.SetTrigger("Jump");
            }
            if (Input.GetKey(KeyCode.Delete))
            {
                anim.SetTrigger("Die");
            }
        }
        
    }
}
