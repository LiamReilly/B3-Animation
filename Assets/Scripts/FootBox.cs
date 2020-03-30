using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq.Expressions;

public class FootBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Action onTrig;

    public void setOT(Action e) {
        onTrig = e;
    }

    void onCollision(Collision c) {
        print("test");
    }

    void onTriggerEnter(Collider c) {
        onTrig();
        print("Entered collider");
    }
}
