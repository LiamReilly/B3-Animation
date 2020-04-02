using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartTracker : MonoBehaviour
{
    
    public Controller bodyCon;

    void Start() {
        
    }

    public float getX() {
        return transform.position.x;
    }

    public float getY() {
        return transform.position.y;
    }

    public float getZ() {
        return transform.position.z;
    }

    public Vector3 getPos() {
        return transform.position;
    }

}
