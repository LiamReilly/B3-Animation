using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWasd : MonoBehaviour
{
    Animator anim;
    Vector2 velocity = Vector2.zero;
    private bool move;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        anim.SetFloat("velocityx", rb.velocity.x);
        anim.SetFloat("velocityy", rb.velocity.y);
        if (velocity.x != 0 || velocity.y != 0)
        {
            move = true;
        }
        else
        {
            move = false;
        }
        //Debug.Log(velocity.x);
        anim.SetBool("Move", move);
    }
}
